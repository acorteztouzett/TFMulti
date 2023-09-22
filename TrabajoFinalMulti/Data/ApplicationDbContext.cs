using Microsoft.EntityFrameworkCore;

namespace TrabajoFinalMulti.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }
        
    }
}
