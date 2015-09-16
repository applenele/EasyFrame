using QX.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace QX.Entity.Models
{
    public class DB : DbContext
    {
        public DB()
            : base("mssqldb")
        {
        }

        public DbSet<User> Users { set; get; }

        public DbSet<Department> Departments { set; get; }

        public DbSet<Role> Roles { set; get; }

        public DbSet<Permission> Permissions { set; get; }

        public DbSet<UserRole> UserRoles { set; get; }

        public DbSet<UserVipPermission> UserVipPermissions { set; get; }

        public DbSet<RolePermission> RolePermissions { set; get; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
           // base.Configuration.
            base.OnModelCreating(modelBuilder);
        }
    }
}