using baseProject.Infra.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace baseProject.Infra.Context
{
    public class ContextManagement
    {
        public static void MigrationInitialization(WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.Migrate();
        }
    }
}
