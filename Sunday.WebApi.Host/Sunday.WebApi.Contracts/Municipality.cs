using System;
using System.Collections.Generic;

namespace Sunday.WebApi.Contracts
{
    public class Municipality   
    {
        public Guid Uuid { get; set; }
        public string Name { get; set; }
        public List<Tax> Taxes { get; set; }
    }
}
