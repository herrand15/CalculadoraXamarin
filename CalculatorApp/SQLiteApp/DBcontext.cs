using Microsoft.EntityFrameworkCore;
using System;
using CalculatorApp.Classes;

namespace SQLiteApp
{
    public class DBcontext : DbContext
    {

        public DbSet<Operation> ops { get; set; }
        private readonly string DBpath;

        public DBcontext(string DBPath)
        {
            DBpath = DBPath;
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename= {DBpath}");
        }

    }
}
