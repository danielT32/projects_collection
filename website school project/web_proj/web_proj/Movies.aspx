<%@ Page Title="" Language="C#" MasterPageFile="~/masterPage.Master" AutoEventWireup="true" CodeBehind="Movies.aspx.cs" Inherits="web_proj.Movies" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        td{width:150px}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <a name="top">
        <h1 class="koteret">סרטים</h1>
    </a>

    <table style="margin: 0px auto;">
        <tr style="border:2px">
            <td>
                <a href="#actMovie">פעולה-אקשן</a><br />
                3
            </td>
            <td>
                <a href="#comedyMovie">קומדיה</a><br />
                1
            </td>
            <td>
                <a href="#dramaMovie">דרמה</a><br />
                1
            </td>
            <td>
                <a href="#crimeMovie">פשע</a><br />
                1
            </td>
            <td>
                <a href="#animaMovie">אנימציה</a><br />
                2
            </td>
        </tr>
    </table>

    <a name ="actMovie">
        <div class="kategories"><u>פעולה-אקשן</u></div>
    </a>
    <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">Enola holms (אנולה הולמס)</div> <br />
                
                אנגליה, 1884. הנערה האמיצה אנולה הולמס, גדלה עם אימה ללא אביה שמת ואחיה הגדולים שיצאו מהבית בשלב מוקדם בחייה.
                היא מחפשת את אימה הנעדרת ונעזרת ביכולות הבילוש שלה, כדי להערים על אחיה הגדולים שרלוק ומיינקרופט ולעזור ללורד בן גילה שנמצא במנוסה כשחייו בסכנה <br /><br />
                דירוג משתמשי גוגל: 93% אהבו את הסרט הזה
                <br /><br /><br /><br /><br /><br /><br />
                <a href="#top">חזרה לתחילת הדף</a>

            </td>
            <td style="text-align:right"><img style="height:400px" src="photos/sratim/Enola_Holmes_2020.jpg" /></td>
        </tr>
    </table><br />

    <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">End game</div> <br />
                לאחר שתאנוס הצליח לאסוף את ששת אבני האינסוף, הוא כפה את רצונו המעוות על כלל החיים ובאופן רנדומלי מחק מחצית מאוכלוסיית היקום,
                ובכללם גם מספר לא מבוטל של נוקמים. לאחר החורבן העצום, הנוקמים הנותרים עומדים בפתחו של האתגר הגדול ביותר שניצבו בפניו:
                מציאת הכוחות הפנימיים כדי 'לקום מהספסל' ולמצוא דרך להביס את תאנוס אחת ולתמיד <br /><br />
                דירוג משתמשי גוגל: 95% אהבו את הסרט הזה<br /><br />
                <a href="#top">חזרה לתחילת הדף</a>



            </td>
            <td style="text-align:right"><img style="height:250px" src="photos/sratim/endgame.jpg" /></td>
        </tr>
    </table><br /><br /><br />

    <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">מהיר ועצבני</div> <br />
                מהיר ועצבני היא סדרת סרטי פעולה העוסקת במרוצי רחוב מחתרתיים, לרוב תוך שילוב מספר רב של כלי רכב.
                הקשר העלילתי בין הסדרות אינו רציף, אם כי קיים קו משותף המתבסס על דמויות הסדרה.<br /><br />
                דירוג משתמשי גוגל עבור סרט 9: 96% אהבו את הסרט הזה<br /><br />
                <a href="#top">חזרה לתחילת הדף</a>



            </td>
            <td style="text-align:right"><img style="height:210px" src="photos/sratim/fast_and_furios.jpg" /></td>
        </tr>
    </table><br />
    <a name ="comedyMovie">
        <div class="kategories"><u>קומדיה</u></div>
    </a>
    <p>
    <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">ג'וני אינגליש 3.0</div> <br />
                כאשר מתקפת סייבר חושפת את זהותם של כל הסוכנים החשאיים הבריטים, הסוכן הפורש ג'וני אינגליש,
                אשר עזב את השירות החשאי וכיום מלמד בבית ספר יסודי, נקרא לחזור לדגל ולהציל שוב את העולם בשירות הוד מעלתה. במסגרת משימתו עליו לעצור את ההאקר המסתורי האחראי למתקפת הסייבר.
                עם הבנה לוקה בטכנולוגיה המודרנית ושיטות פעולה מיושנות, ג'וני אינגליש מתגלה כסוכן האנלוגי המושלם לאיום הדיגיטלי החדש <br /><br />
                דירוג משתמשי גוגל: 93% אהבו את הסרט הזה
                <br /><br /><br /><br /><br />
                <a href="#top">חזרה לתחילת הדף</a>


            </td>
            <td style="text-align:right"><img style="height:400px" src="photos/sratim/johnnyEnglish.jpg" /></td>
        </tr>
    </table><br />



    </p>
    <a name="dramaMovie">
        <div class="kategories"><u>דרמה-פעולה</u></div>
    </a>
    <p>
        <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">1917</div> <br />
                הסרט מספר את סיפורם של שני חיילים בריטים צעירים באביב 1917, זמן קצר לאחר הנסיגה הגרמנית לקו הינדנבורג, עליהם הוטל להעביר הודעה ליחידה שכנה העומדת לצאת לקרב אבוד מראש. <br /><br />
                דירוג משתמשי גוגל: 92% אהבו את הסרט הזה
                <br /><br />
                <a href="#top">חזרה לתחילת הדף</a>


            </td>
            <td style="text-align:right"><img style="height:250px" src="photos/sratim/1917.jpg" /></td>
        </tr>
    </table><br />

    </p>
    <a name ="crimeMovie">
        <div class="kategories"><u>פשע</u></div>
    </a>
    <p>
        <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">knives out</div> <br />
                כאשר הסופר הידוע הארלן תרומביי נמצא מת באחוזתו מיד לאחר יום הולדתו ה-85, הבלש המסוגנן בנואה בלאנק מגויס לחקירה באופן מסתורי.
                ממשפחתו הלא-מתפקדת של הארלן ועד הצוות המסור שלו, על בלאנק לנווט ברשת של הסחות דעת ושקרים על מנת לחשוף את האמת מאחורי מותו בטרם עת של הארלן <br /><br />
                דירוג משתמשי גוגל: 93% אהבו את הסרט הזה
                <br /><br /><br /><br /><br /><br /><br />
                <a href="#top">חזרה לתחילת הדף</a>


            </td>
            <td style="text-align:right"><img style="height:400px" src="photos/sratim/Knives_Out_poster.jpeg" /></td>
        </tr>
    </table><br />

    </p>
    <a name="animaMovie">
        <div class="kategories"><u>אנימציה</u></div>
    </a>
    <p>
        <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">spiderman into the spider verse</div> <br />
                מיילס מוראלס, מתבגר מברוקלין, נעקץ על ידי עכביש רדיואקטיבי וזוכה לקורס מזורז בטוויית קורים מחבריו החדשים שחיים בממד מקביל. <br /><br />
                דירוג משתמשי גוגל: 92% אהבו את הסרט הזה
                <br /><br /><br /><br /><br /><br /><br /><br />
                <a href="#top">חזרה לתחילת הדף</a>


            </td>
            <td style="text-align:right">
                <img style="height:400px" src="photos/sratim/spiderman%20into%20the%20spider%20verse.jpg" /></td>
        </tr>
    </table><br />
    <table style="margin: 0px auto; width: 80%">
        <tr>
            <td style="text-align:right; width:80%">
                <div class="heads">Toy Story 4</div> <br />
                וודי תמיד היה בטוח במקומו בעולם והאמין שתפקידו המרכזי בחיים הללו הוא לשמור על הילד שהוא בבעלותו, בין אם זה אנדי או בוני.
                אך כאשר בוני מצרפת צעצוע בר מזל חדש לחבורה בשם "פורקי", וודי יוצא למסע הרפתקני יחד עם חברים חדשים וישנים, דרך מסע זה יבין וודי עד כמה גדול עולמנו בעבור צעצוע <br /><br />
                דירוג משתמשי גוגל: 88% אהבו את הסרט הזה
                <br /><br /><br /><br /><br /><br />
                <a href="#top">חזרה לתחילת הדף</a>

            </td>
            <td style="text-align:right">
                <img style="height:400px" src="photos/sratim/Toy_Story_4_poster.jpg" /></td>
        </tr>
    </table><br />


    </p>


</asp:Content>
