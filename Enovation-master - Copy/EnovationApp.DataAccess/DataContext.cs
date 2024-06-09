using EnovationAssignment.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnovationApp.DataAccess.Models
{
    public class DataContext:DbContext
    {
        public DbSet<Animal> Animals { get; set; }
        public DbSet<User> Users { get; set; }
        //public DataContext()
        //{

        //}
        public DataContext(DbContextOptions options):base(options)
        {

        }
        
    }
}
