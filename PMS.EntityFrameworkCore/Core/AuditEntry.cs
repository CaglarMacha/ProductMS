using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.EntityFrameworkCore.Core
{
    public class AuditEntry
    {
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
            TemporaryProperties = new List<PropertyEntry>();
            OldValues = new Dictionary<string, object>();
            NewValues = new Dictionary<string, object>();
        }

        public EntityEntry Entry { get; }
        public string TableName { get; set; }
        public string Action { get; set; }
        public string PrimaryKey { get; set; }
        public Dictionary<string, object> OldValues { get; set; }
        public Dictionary<string, object> NewValues { get; set; }
        public List<PropertyEntry> TemporaryProperties { get; }

        public bool HasTemporaryProperties => TemporaryProperties.Any();
    }
}
