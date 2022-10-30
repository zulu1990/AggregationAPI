using AggregationAPI.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace AggretationApp.Repository
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Record> Records { get; set; }
    }
}
