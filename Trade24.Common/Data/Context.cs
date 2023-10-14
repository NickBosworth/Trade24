using Microsoft.EntityFrameworkCore;

namespace Trade24.Common.Data
{


    public class Context : DbContext
    {
        public DbSet<StockTicker> StockTickers { get; set; }
        public DbSet<DailyStockData> DailyStockData { get; set; }

        public DbSet<RunRecord> RunRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Specify the SQLite database connection string
            string connectionString = "Data Source=data.db"; // Assuming the database file is named "data.db"

            optionsBuilder.UseSqlite(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Define any additional configurations here, if needed
            // For example, setting unique constraints for Symbol and Date in DailyStockData
            modelBuilder.Entity<DailyStockData>()
                .HasIndex(ds => new { ds.Symbol, ds.Date })
                .IsUnique();
        }
    }

}
