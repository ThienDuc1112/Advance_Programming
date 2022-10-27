using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment
{
    internal class User
    {
        protected int id;
        protected string userName;
        protected string password;

        public User(int id, string userName, string password)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
        }
        public User() { }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }

        public string UserName
        {
            get { return userName; }
            set
            {
                if(value == " ")
                {
                    throw new ArgumentException("The name can't be null");
                }
                if (!CheckWord(value))
                {
                    throw new ArgumentException("The name must be alphabet charecters");
                }
                userName = value;
            }
        }

        public string Password
        {
            get { return password; }
            set
            {
                if(value.Length < 5 || value.Length > 12)
                {
                    throw new ArgumentException("The number of characters must be between 5 and 12");
                }
                password = value;
            }
        }

        public void Register()
        {
            Console.WriteLine("Enter a user name:");
            string name = Console.ReadLine();
            Console.WriteLine("Enter a password");
            string password = Console.ReadLine();
            this.UserName = name;
            this.Password = password;
        }

        public virtual void ViewSystem()
        {
            Console.WriteLine("The symtem here");
        }

        public virtual void ViewProfile()
        {
            Console.WriteLine("Here, users can see their profile");
        }

        public bool CheckWord(string str)
        {
            str = str.Trim();
            str = str.ToLower();
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= 'a' && str[i] <= 'z')
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsNumber(string str)
        {
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] >= '0' && str[i] <= '9')
                {
                    return true;
                }
            }
            return false;
        }
        public bool IsValidEmail(string source)
        {
            return new EmailAddressAttribute().IsValid(source);
        }
    }
}
