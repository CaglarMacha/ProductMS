using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSM.Domain.AuditLogs
{
    public interface IAuditLogRepository
    {
        Task SaveAsync(AuditLog auditLog);
    }
}
