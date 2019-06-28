using System;
using System.Collections.Generic;

namespace Sunday.Repositories.Entities
{
    public class Municipality : IEntityId
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Tax> Taxes { get; set; }
    }
}
