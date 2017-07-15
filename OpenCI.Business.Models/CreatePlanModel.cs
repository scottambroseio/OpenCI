using System;

namespace OpenCI.Business.Models
{
    public class CreatePlanModel
    {
        public Guid ProjectGuid { get; set; }

        public bool Enabled { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
