//using Microsoft.AspNetCore.Http;
//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.DependencyInjection;
//using Microsoft.Extensions.Logging;
//using PMS.EntityFrameworkCore.Core;
//using PSM.Domain.AuditLogs;
//using PSM.Domain.Entities;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using static System.Formats.Asn1.AsnWriter;

//namespace PMS.Infrastructure.Middlewares
//{
//    public class AuditLoggingMiddleware
//    {
//        private readonly ILogger<AuditLogMiddleware> _logger;
//        private readonly IAuditLogRepository _auditLogRepository;
//        private readonly IServiceScopeFactory serviceScopeFactory;
//        private readonly RequestDelegate _next;

//        public AuditLoggingMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
//        {
//            _next = next;
//            this.serviceScopeFactory = serviceScopeFactory;
//        }

//        public async Task InvokeAsync(HttpContext context)
//        {
//            using (var scope = serviceScopeFactory.CreateScope())
//            {
//                var auditLogger = scope.ServiceProvider.GetRequiredService<IAuditLogger>();
//                var dbContext = scope.ServiceProvider.GetRequiredService<PMSDbContext>();

//                var request = context.Request;
//                var userId = context.User?.Identity?.Name ?? "Anonymous";

//                dbContext.ChangeTracker.DetectChanges();

//                await _next(context);

//                var changedEntities = dbContext.ChangeTracker.Entries()
//                    .Where(e => e.State == EntityState.Modified || e.State == EntityState.Added || e.State == EntityState.Deleted);
//                var entityName = "changedEntities.FirstOrDefault().Entity?.GetType()?.Name";
//                foreach (var entity in changedEntities)
//                {
//                    var changeType = entity.State switch
//                    {
//                        EntityState.Added => "Create",
//                        EntityState.Modified => "Update",
//                        EntityState.Deleted => "Delete",
//                        _ => "Unknown"
//                    };

//                    var changeDetails = new Dictionary<string, object>();
//                    if (entity.State == EntityState.Modified)
//                    {
//                        foreach (var property in entity.OriginalValues.Properties)
//                        {
//                            var original = entity.OriginalValues[property];
//                            var current = entity.CurrentValues[property];

//                            if (!object.Equals(original, current))
//                            {
//                                changeDetails[property.Name] = new { Original = original, Current = current };
//                            }
//                        }
//                    }
//                    else if (entity.State == EntityState.Added)
//                    {
//                        foreach (var property in entity.CurrentValues.Properties)
//                        {
//                            changeDetails[property.Name] = entity.CurrentValues[property];
//                        }
//                    }
//                    else if (entity.State == EntityState.Deleted)
//                    {
//                        foreach (var property in entity.OriginalValues.Properties)
//                        {
//                            changeDetails[property.Name] = entity.OriginalValues[property];
//                        }
//                    }

//                    await auditLogger.LogChangeAsync(entityName, changeType, userId, changeDetails);
//                }
//            }
//        }
//    }

//}
