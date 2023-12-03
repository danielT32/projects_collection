import cv2
import numpy as np
import time
import math
import pyautogui

#skin color for ycrcb filter
lower_ycrcb = np.array([0, 140, 50], dtype=np.uint8)
upper_ycrcb = np.array([255, 173, 130], dtype=np.uint8)

MIN_DISTANCE_FINGERS = 15
MAX_HAND_RADIUS = 200
MIN_HAND_RADIUS = 40

#hand detection class
class HandDetector:
    def __init__(self, num_frames=5, threshold=0.5):
        self.queue = []
        self.finger_count_queue = []
        self.num_frames = num_frames
        self.threshold = threshold
        self.current_frame = None
        self.current_frame_num = 0
        self.finger_counts_num = 0
        self.last_press = time.time()
        self.win_funcs = dict()

    @staticmethod
    def mask_frames(frame):
        ycrcb = cv2.cvtColor(frame, cv2.COLOR_BGR2YCrCb)
        mask_ycrcb = cv2.inRange(ycrcb, lower_ycrcb, upper_ycrcb)
        mask = mask_ycrcb  # cv2.bitwise_and(mask_ycrcb, 1)
        mask = cv2.dilate(mask, np.ones((3, 3), np.uint8), iterations=1)
        mask = cv2.erode(mask, np.ones((3, 3), np.uint8), iterations=2)

        return mask

    def analizeHand(self, frame):
        mask = self.mask_frames(frame)
        cv2.imshow("mask", mask)
        return mask

    @staticmethod
    def skeletonize(mask):
        skel = np.zeros(mask.shape, dtype=np.uint8)
        cv2.ximgproc.thinning(mask, skel)
        return skel

    @staticmethod
    def black_pixel_ratio(masked_frame, circle):
        (x, y), radius = circle
        circular_mask = np.zeros_like(masked_frame)
        cv2.circle(circular_mask, (x, y), int(radius), (255, 255, 255), -1)
        masked_pixels = cv2.bitwise_and(masked_frame, circular_mask)
        non_black_pixels = cv2.countNonZero(masked_pixels)
        total_pixels = (np.pi * radius ** 2)
        return non_black_pixels / total_pixels

    def smooth_mask(self, mask, num_frames=3):
        self.queue.append(mask)

        if len(self.queue) < num_frames:
            return mask
        if len(self.queue) > num_frames:
            self.queue.pop(0)

        mask_smooth = sum(self.queue)
        int_mask = cv2.convertScaleAbs(mask_smooth)
        color_mask = cv2.cvtColor(int_mask, cv2.COLOR_GRAY2BGR)
        smooth_mask = cv2.GaussianBlur(color_mask, (11, 11), 0)

        return cv2.cvtColor(smooth_mask, cv2.COLOR_BGR2GRAY)

    @staticmethod
    def detect_fingers(hand_contour, hull, frame):
        defects = cv2.convexityDefects(hand_contour, hull)
        finger_points = []

        for i in range(defects.shape[0]):
            s, e, f, d = defects[i, 0]
            start = tuple(hand_contour[s][0])
            end = tuple(hand_contour[e][0])
            far = tuple(hand_contour[f][0])

            a = math.sqrt((end[0] - start[0]) ** 2 + (end[1] - start[1]) ** 2)
            b = math.sqrt((far[0] - start[0]) ** 2 + (far[1] - start[1]) ** 2)
            c = math.sqrt((end[0] - far[0]) ** 2 + (end[1] - far[1]) ** 2)
            angle = (math.acos((b ** 2 + c ** 2 - a ** 2) / (2 * b * c)) * 180) / 3.14

            if angle <= 90:
                finger_points.append(start)
                finger_points.append(end)

        finger_points = removeCloseAndDuplicatePoints(removeDuplicates(finger_points))
        for finger in finger_points:
            cv2.circle(frame, finger, 5, [255, 255, 255], -1)

        return finger_points

    def fingers_count(self, hand_contour, hull, frame):
        count_defect_counts = 0
        max_defects_percent = 30
        hold_still = False
        finger_points = self.detect_fingers(hand_contour, hull, frame)
        finger_counted = len(finger_points)
        self.finger_count_queue.append((finger_counted, time.time()))

        if self.finger_count_queue[-1][1] - self.finger_count_queue[0][1] >= 1:
            self.finger_count_queue.pop(0)
        finger_counted = round(sum([pair[0] for pair in self.finger_count_queue]) / len(self.finger_count_queue))
        for value, _ in self.finger_count_queue:
            if abs(value - finger_counted) >= 1:
                count_defect_counts += abs(value - finger_counted)
        if (count_defect_counts / len(self.finger_count_queue)) * 100 > max_defects_percent:
            hold_still = True

        return finger_counted, hold_still

    def useWindowsFunc(self, finger_counted):
        if time.time() - self.last_press > 1:
            pyautogui.press(self.win_funcs[finger_counted])
            self.last_press = time.time()


def removeDuplicates(finger_points):
    return [t for t in (set(tuple(i) for i in finger_points))]


def removeCloseAndDuplicatePoints(finger_points):
    error_list = []
    for i in finger_points:
        for j in finger_points:
            if i == j:
                continue

            distance = math.sqrt((i[0] - j[0]) ** 2 + (i[1] - j[1]) ** 2)
            if distance < MIN_DISTANCE_FINGERS:
                error_list.append(i)
                error_list.append(j)

    error_list = removeDuplicates(error_list)
    error_list = error_list[::2]
    for points in error_list:
        finger_points.remove(points)
    return finger_points


def crop_frame(frame, point1, point2):
    x1, y1 = point1
    x2, y2 = point2
    return frame[min(y1, y2):max(y1, y2), min(x1, x2):max(x1, x2)]


def changeBrightness(frame):
    alpha = 2  # Contrast control
    beta = 50  # Brightness control

    # call convertScaleAbs function
    return cv2.convertScaleAbs(frame, alpha=alpha, beta=beta)


def drawText(frame, text):
    text_on_frame = frame
    font = cv2.FONT_HERSHEY_SIMPLEX
    bottomLeftCornerOfText = (10, 500)
    fontScale = 1
    fontColor = (255, 255, 255)
    thickness = 1
    lineType = 2
    cv2.putText(text_on_frame, text, bottomLeftCornerOfText, font, fontScale, fontColor, thickness, lineType)
    return text_on_frame


def readTxtFunc():
    file = open("hand_dict.txt", 'r')
    commands = file.read().split('\n')
    win_funcs = dict()

    for i in range(1, len(commands) + 1):
        win_funcs[i] = commands[i-1]

    return win_funcs

def main():
    cap = cv2.VideoCapture("onlyHand.mp4")
    detector = HandDetector()
    prev_counter = []
    pointsRec = []
    prev_hold_still = True

    def select_roi(event, x, y, flags, param):
        nonlocal pointsRec
        if event == cv2.EVENT_LBUTTONDOWN:
            pointsRec.append((x, y))
        elif event == cv2.EVENT_LBUTTONUP:
            if len(pointsRec) == 1:
                pointsRec.append((x, y))

    cv2.namedWindow("Frame")
    cv2.setMouseCallback("Frame", select_roi)

    detector.win_funcs = readTxtFunc()
    while True:
        ret, frame = cap.read()
        if len(pointsRec) != 2:
            mask = frame
        else:
            try:
                mask = crop_frame(frame, pointsRec[0], pointsRec[1])
            except Exception:
                continue
        try:
            mask = detector.analizeHand(mask)
        except:
            cap = cv2.VideoCapture("onlyHand.mp4")
            continue


        cv2.rectangle(frame, (0, 400), (350, 100), (0, 0, 255), 3)

        contours, _ = cv2.findContours(mask, cv2.RETR_TREE, cv2.CHAIN_APPROX_NONE)
        expected_hand_size = (50, 100)

        if len(contours) > 0:
            for countur in contours:
                ((x, y), radius) = cv2.minEnclosingCircle(countur)
                hull_without_points = cv2.convexHull(countur, returnPoints=False)
                hull = cv2.convexHull(countur)

                # Check countur for hand
                hand_circle = ((int(x), int(y)), int(radius))

                if MIN_HAND_RADIUS < radius < MAX_HAND_RADIUS and 0.7 > detector.black_pixel_ratio(mask, hand_circle) > 0.3:
                    cv2.circle(frame, (int(x), int(y)), 7, (0, 255, 0), -1)  # center of hand
                    cv2.circle(frame, (int(x), int(y)), int(radius), (0, 255, 255), 2)  # around the hand
                    cv2.drawContours(frame, [hull], 0, (0, 0, 255), 2)

                    # Start measuring speed from second frame
                    if len(prev_counter) != 0:
                        if 0 < x < 350 and 100 < y < 400:
                            finger_counted, hold_still = detector.fingers_count(countur, hull_without_points, frame)
                            if not hold_still and finger_counted != 0:
                                frame = drawText(frame, str(finger_counted))
                                detector.useWindowsFunc(finger_counted)

                            else:
                                frame = drawText(frame, "Hold still!!!")
                            prev_hold_still = hold_still

                    prev_counter = (x, y)

        # Show the frame

        cv2.imshow("Mask", mask)
        cv2.imshow('Frame', frame)
        # Check if the user pressed 'q' to quit
        if cv2.waitKey(1) == ord('q') or cv2.waitKey(1) == ord('/'):
            break

    cap.release()
    cv2.destroyAllWindows()


if __name__ == "__main__":
    main()
