using Microsoft.EntityFrameworkCore;
using A2.Models;

namespace A2.Data
{
    public class A2DbContext : DbContext
    {
        public A2DbContext(DbContextOptions<A2DbContext> options) : base(options) { }
        public DbSet<Event> Events { get; set; }
        public DbSet<Organizor> Organizors { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

    }
}