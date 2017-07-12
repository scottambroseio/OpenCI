namespace OpenCI.Business.Models
{
    public class ProjectModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"Project {{ Id: {Id}, Name: {Name} }}";
        }
    }
}
