using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PSM.Domain;
using PSM.Domain.AuditLogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PMS.Infrastructure.Middlewares
{
    public class AuditLogMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<AuditLogMiddleware> _logger;
        private readonly IAuditLogRepository _auditLogRepository;
        private readonly IServiceScopeFactory serviceScopeFactory;

        public AuditLogMiddleware(RequestDelegate next, ILogger<AuditLogMiddleware> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _logger = logger;
            //_auditLogRepository = auditLogRepository;
            this.serviceScopeFactory = serviceScopeFactory;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var startTime = DateTime.UtcNow;

            // İstek bilgilerini loglayın (RequestPath, HTTP method, vb.)
            var auditLog = new AuditLog
            {
                RequestPath = context.Request.Path,
                HttpMethod = context.Request.Method,
                Timestamp = startTime
            };

            // İsteğin cevabını işleyin
            var originalBodyStream = context.Response.Body;
            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                try
                {
                    await _next(context); // İstek işlemini gerçekleştirin

                    // Yanıt sonrası loglama (StatusCode ve yanıtın uzunluğu)
                    auditLog.StatusCode = context.Response.StatusCode;
                }
                catch (Exception ex)
                {
                    // Hata durumunda loglama
                    _logger.LogError(ex, "Hata oluştu.");
                    auditLog.StatusCode = 500; // Hata durumunda HTTP 500 döndürülür
                }
                finally
                {
                    // Durum kodunu ve logları kaydedin
                    auditLog.Duration = (DateTime.UtcNow - startTime).Milliseconds;

                    // Log'u veritabanına kaydedin (Opsiyonel)
                    using (var scope = serviceScopeFactory.CreateScope())
                    {
                        var auditLogRepository = scope.ServiceProvider.GetRequiredService<IAuditLogRepository>();
                        await auditLogRepository.SaveAsync(auditLog);
                    }

                    // Yanıtı geri yazın
                    await memoryStream.CopyToAsync(originalBodyStream);
                }
            }
        }


    }
}
