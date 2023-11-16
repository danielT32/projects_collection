using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Model
{
    /// <summary>
    /// Removes all non-alphanumeric characters from the input string.
    /// </summary>
    /// <param name="input">The string to remove non-alphanumeric characters from.</param>
    /// <returns>A new string that contains only alphanumeric characters.</returns>
    
    public class User : BaseEntity
    {
        //properties
        private int iD; //מספר מזהה של המשתמש
        private string email; //כתובת דוא"ל
        private string uname; //שם משתמש
        private string fname; //שם פרטי
        private string lname; //שם משפחה
        private string description; //ביוגרפיה או תיאור של המשתמש
        private DateTime entrDate; //תאריך יצירת משתמש
        private string password; //סיסמת המשתמש
        //getters and setters
        public string Description { get => description; set => description = value; }
        public DateTime EntrDate { get => entrDate; set => entrDate = value; }
        public string Password { get => password; set => password = value; }
        public int ID { get => iD; set => iD = value; }
        public string Uname { get => uname; set => uname = RemoveNonAlphanumeric(value); }
        public string Fname { get => fname; set => fname = RemoveNonAlphanumeric(value); }
        public string Lname { get => lname; set => lname = RemoveNonAlphanumeric(value); }
        public string Email { get => email; set => email = value; }
        private static string RemoveNonAlphanumeric(string input)
        {
            // Define a regular expression that matches all non-alphanumeric characters
            Regex regex = new Regex("[^a-zA-Z0-9]");

            // Use the regular expression to remove all non-alphanumeric characters from the input string
            string result = regex.Replace(input, "");

            return result;
        }
    }

    public class UserList : List<User>
    {
        public UserList() { }
        public UserList(IEnumerable<User> list) : base(list) { }
        public UserList(IEnumerable<BaseEntity> list) : base(list.Cast<User>().ToList()) { }
    }
}
