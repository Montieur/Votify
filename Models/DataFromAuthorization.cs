using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Votify.Models
{
    public class DataFromAuthorization
    {
        public string Token { get; set; }
        public User User { get; set; }

        public DataFromAuthorization(string Token, User User)
        {
            this.Token = Token;
            this.User = User;
        }
    }
}
