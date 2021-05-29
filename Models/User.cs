using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votify.Models
{
    public class User
    {
        private int UserId;
        public string UserLogin { get; set; }
        public string UserEmail { get; set; }
        public string UserName { get; set; }

        public User(int Id,string Login, string Email, string Name)
        {
            this.UserId = Id;
            this.UserLogin = Login;
            this.UserEmail = Email;
            this.UserName = Name;
        }


    }
}
