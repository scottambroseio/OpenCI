using System;

namespace OpenCI.Data.Entities
{
    public interface IEntity
    {
        int Id { get; set; }

        Guid Guid { get; set; }

        DateTime CreationTime { get; set; }

        DateTime ModificationTime { get; set; }
    }
}