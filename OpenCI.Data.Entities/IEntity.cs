using System;

namespace OpenCI.Data.Entities
{
    public interface IEntity
    {
        Guid Guid { get; set; }

        DateTime CreationTime { get; set; }

        DateTime ModificationTime { get; set; }
    }
}
