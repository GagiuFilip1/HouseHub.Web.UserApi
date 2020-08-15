using Microsoft.EntityFrameworkCore;

namespace HouseHub.Web.UserApi.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            SetModelsRelations(modelBuilder);
            SetConvertors(modelBuilder);
            SetIndexes(modelBuilder);
        }

        private static void SetModelsRelations(ModelBuilder modelBuilder)
        {
    
        }

        private static void SetConvertors(ModelBuilder modelBuilder)
        {
           
        }

        private static void SetIndexes(ModelBuilder modelBuilder)
        {
         
        }
    }
}