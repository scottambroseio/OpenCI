using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class IdentityRole : IRole<int>
    {
        internal IdentityRole()
        {
        }

        public IdentityRole(string name)
        {
            Name = name;
        }

        public int Id { get; internal set; }
        public string Name { get; set; }
    }
}