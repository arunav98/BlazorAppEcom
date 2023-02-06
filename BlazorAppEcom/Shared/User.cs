using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAppEcom.Shared
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }=String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime date { get; set; } = DateTime.Now;
        public Addresses Addresses { get; set; }
    }
}
