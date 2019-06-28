using System;

namespace Sunday.Repositories.Entities
{
    public class Tax : IEntityId
    {
        public Guid Uuid { get; set; }
        public Guid MunicipalityUuid { get; set; }
        public virtual Municipality Municipality { get; set; }
        public int Schedule { get; set; }
        public DateTime StartPeriod { get; set; }
        public double Value { get; set; }

        
    }
}
