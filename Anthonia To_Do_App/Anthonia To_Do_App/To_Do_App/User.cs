using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using static To_Do_App.Program;

namespace To_Do_App
{
    public class User
    {
       
        public string Name { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public User(string name, string password, string email)
        {
            Name = name;
            Password = password;
            Email = email;
        }
    }
}
