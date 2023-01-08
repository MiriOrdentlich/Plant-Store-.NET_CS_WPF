using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public struct Users
    {
       // public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Name { get; set; }
        public string? Adress { get; set; }
        public string? Phone { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public override string ToString() => $@"
UserName    =   {UserName},
Name        =   {Name},
Adress      =   {Adress},
Phone       =   {Phone},
Email       =   {Email},
Password    =   {Password}
";

    }
}
