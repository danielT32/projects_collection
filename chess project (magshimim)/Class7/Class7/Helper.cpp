#include "Helper.h"


void locationToArr(int* arr, std::string location)
{
	int row = location[0] - 97;
	int line = location[1] -'1';
	arr[1] = line;
	arr[0] = row;
}

int locationToIndex(std::string location)
{
	int row = location[0] - 97;
	int line = location[1] - '1';
	return row + line * 8;
}

int* indexToArr(int indx)
{
	int* arr = new int[2];
	int backupIndx = indx, line = 0, row = 0;
	while (backupIndx > 7)
	{
		row++;
		backupIndx -= 8;
	}
	line = backupIndx;
	arr[1] = line;
	arr[0] = row;
	return arr;
}

std::string arrToString(int i, int j)
{
	char row = "abcdefgh"[j];
	std::string line = std::to_string(i + 1);
	return row + line;
}
