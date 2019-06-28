using System;

namespace Sunday.Repositories.Entities
{
    public interface IEntityId
    {
        Guid Uuid { get; set; }
    }
}
