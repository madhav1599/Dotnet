﻿using Microsoft.EntityFrameworkCore;
using MVCCRUD.Models;

namespace MVCCRUD.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):base(options) { }
        public DbSet<Employee> Employees { get; set; }
    }
}
