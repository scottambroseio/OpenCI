using System;

namespace OpenCI.Data.Entities
{
    public class Plan : IEntity
    {
        public int Id { get; set; }
        public int PlanId { get; set; }
        public Guid Guid { get; set; }
        public Guid ProjectGuid { get; set; }
        public DateTime CreationTime { get; set; }
        public DateTime ModificationTime { get; set; }
        public bool Enabled { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
