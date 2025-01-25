using PSM.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.AuditLogging
{
    public class AuditLogging : AuditedAggregateRoot<Guid>
    {
        public string? TableName { get; set; }
        public string? Action { get; set; } 
        public string? PrimaryKey { get; set; }
        public string? ColumnName { get; set; }
        public string? OldValue { get; set; }
        public string? NewValue { get; set; }
        public DateTime ChangeTime { get; set; }
        public string? ChangedBy { get; set; }
    }
}
