using Microsoft.EntityFrameworkCore;
using PMS.EntityFrameworkCore.Core;
using PSM.Domain;
using PSM.Domain.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.EntityFrameworkCore
{
    public class AuditLogRepository : IAuditLogRepository
    {
        private readonly PMSDbContext context;

        public AuditLogRepository(PMSDbContext context)
        {
            this.context = context;
        }
        public async Task SaveAsync(AuditLog auditLog)
        {
            await context.AuditLogs.AddAsync(auditLog);
            await context.SaveChangesAsync();
        }
    }
}
