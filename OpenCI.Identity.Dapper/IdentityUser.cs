using Microsoft.AspNet.Identity;
using System;

namespace OpenCI.Identity.Dapper
{
    public class IdentityUser : IUser<int>
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
