using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookAPI
{
    public class DataContext: DbContext
    {
        public DbSet<Car> Cars { get; set; }
        public DbSet<Book> Books { get; set; }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer("Server=localhost;Database=CarBookDB;User Id=sa;Password = Password1!; ");
        //}
 public DataContext(DbContextOptions<DataContext> options)
        : base(options)
    { }

        
    }
}
