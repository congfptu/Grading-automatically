using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Group1_Project.DAO
{
    class Account
    {
        private int id;
        private string username;
        private string password;

        public Account()
        {
        }

        public Account(int id, string username, string password)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }

        public int Id { get => id; set => id = value; }
        public string Username { get => username; set => username = value; }
        public string Password { get => password; set => password = value; }
    }
}
