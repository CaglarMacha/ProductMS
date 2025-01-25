using PSM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain
{
    public class AuditLog: AuditedAggregateRoot<Guid>
    {
        public string RequestPath { get; set; } = string.Empty;
        public string HttpMethod { get; set; } = string.Empty;
        public string RequestBody { get; set; } = string.Empty;
        public string ResponseBody { get; set; } = string.Empty;
        public int StatusCode { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
        public long Duration { get; set; } // Milliseconds
    }
}
