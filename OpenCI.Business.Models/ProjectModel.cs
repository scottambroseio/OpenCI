using System;

namespace OpenCI.Business.Models
{
    public class ProjectModel
    {
        public Guid Guid { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime CreationTime { get; set; }

        public DateTime ModificationTime { get; set; }
    }
}