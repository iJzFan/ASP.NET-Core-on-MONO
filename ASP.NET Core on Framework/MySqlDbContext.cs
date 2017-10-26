using ASP.NET_Core_on_Framework.Models;
using MySql.Data.Entity;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity;

using System.Linq;
using System.Threading.Tasks;

namespace ASP.NET_Core_on_Framework
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class MySqlDbContext:DbContext
    {
        public MySqlDbContext() : base()
        { }
        public MySqlDbContext(DbConnection connectionString, bool contextOwnsConnection) : base(connectionString, contextOwnsConnection)
        { }
        //public MySqlDbContext(string conn) : base(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=demo;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=True;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
        //{
        //}

        public DbSet<Student> Students { get; set; }
        public DbSet<Teacher> Teachers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
