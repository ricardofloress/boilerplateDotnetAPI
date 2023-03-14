using baseProject.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace baseProject.Infra.Context
{
    public class ContextManagement
    {
        public static void MigrationInitialization(IApplicationBuilder builder)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                serviceScope.ServiceProvider.GetService<ApplicationDbContext>().Database.Migrate();
            }
        }
    }
}
