using PMS.EntityFrameworkCore.Core;
using PSM.Domain.AuditLogging;
using PSM.Domain.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infrastructure.Middlewares
{
    public class AuditLogger: IAuditLogger
    {
        private readonly PMSDbContext dbContext;


        public AuditLogger(PMSDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task LogChangeAsync(string entityName, string action, string userId, Dictionary<string, object> changes)
        {
            foreach (var change in changes)
            {
                var log = new AuditLogging
                {
                    TableName = entityName,
                    Action = action,
                    ChangedBy = userId,
                    ColumnName = change.Key,
                    NewValue = change.Value?.ToString(),
                    ChangeTime = DateTime.UtcNow
                };

                dbContext.AuditLoggings.Add(log);
            }

            await dbContext.SaveChangesAsync();
        }
    }
}
