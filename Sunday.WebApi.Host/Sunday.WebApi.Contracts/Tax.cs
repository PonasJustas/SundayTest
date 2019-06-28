using System;

namespace Sunday.WebApi.Contracts
{
    public class Tax
    {
        private DateTime _startPeriod;

        public Guid Uuid { get; set; }
        public Guid MunicipalityUuid { get; set; }
        public TaxSchedule Schedule { get; set; }

        public DateTime StartPeriod
        {
            get => _startPeriod;
            set => _startPeriod = value.Date;
        }

        public double Value { get; set; }
    }
}