using System;

namespace MiniCRM.API.Models
{
    public class User
    {
        public int id { get; set; }
        public string username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
    }
}