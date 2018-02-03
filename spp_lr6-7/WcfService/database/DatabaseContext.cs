using System.Data.Entity;

namespace WcfService.database
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("DefaultConnection")
        {

        }
        public DbSet<Message> Messages { get; set; }
    }
}
