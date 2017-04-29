using build17_demo.Models;
using Microsoft.EntityFrameworkCore;

namespace build17_demo
{
    public class DataContext : DbContext
    {
        string _connectionString;
        public DataContext(string connectionString)
        {
            this._connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this._connectionString);
        }

        public DbSet<Comment> Comments { get; set; }
    }
}