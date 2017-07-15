using Microsoft.AspNet.Identity;

namespace OpenCI.Identity.Dapper
{
    public class IdentityRole : IRole<int>
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
