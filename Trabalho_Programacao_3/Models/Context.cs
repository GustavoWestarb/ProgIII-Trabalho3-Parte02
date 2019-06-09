using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Core.Objects;

namespace Trabalho_Programacao_3.Models
{
    public class Context : DbContext
    {
        public DbSet<UserModel> Users { get; set; }
        public DbSet<PadModel> Pads { get; set; }

        //public Context() : base("Context") { }

        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<UserModel>().MapToStoredProcedures();
        //}

        //public virtual ObjectResult<UserModel> LoginByEmailPassword(string email, string password)
        //{
        //    var usernameParameter = email != null ?
        //        new ObjectParameter("username", email) :
        //        new ObjectParameter("username", typeof(string));

        //    var passwordParameter = password != null ?
        //        new ObjectParameter("password", password) :
        //        new ObjectParameter("password", typeof(string));

        //    return ((IObjectContextAdapter)this).ObjectContext.ExecuteFunction<UserModel>("LoginByEmailPassword", usernameParameter, passwordParameter);
        //}
    }
}