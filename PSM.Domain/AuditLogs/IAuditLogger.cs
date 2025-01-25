using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.AuditLogs
{
    public interface IAuditLogger
    {
        Task LogChangeAsync(string entityName, string action, string userId, Dictionary<string, object> changes);
    }
}
