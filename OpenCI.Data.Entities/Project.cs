using System;

namespace OpenCI.Data.Entities
{
    public class Project : IEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int Id { get; set; }
        public Guid Guid { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
    }
}