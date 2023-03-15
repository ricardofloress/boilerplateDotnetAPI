using baseProject.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace baseProject.Infra.Context
{
    public class ContextManagement
    {
        public static void MigrationInitialization(WebApplication app)
        {
            app.Services.GetService<ApplicationDbContext>().Database.Migrate();
        }
    }
}
