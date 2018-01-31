using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using Microsoft.AspNet.Identity;
using NHibernate;
using System.Configuration;
using BEEL.Models.Identity;
using NHibernate.Tool.hbm2ddl;

namespace BEEL.Models
{
    public class DBContext
    {
        private readonly ISessionFactory sessionFactory;

        public DBContext()
        {
            //var connectionString = ConfigurationManager.ConnectionStrings["default"].ConnectionString;
            sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008.ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='c:\users\baglan.ibragimov\documents\visual studio 2015\Projects\BEEL\BEEL\App_Data\BeelineDB.mdf';Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework"))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<DBContext>())
                .BuildSessionFactory();
        }
        public ISession MakeSession()
        {
            return sessionFactory.OpenSession();
        }

        public IUserStore<User, int> Users
        {
            get { return new IdentityStore(MakeSession()); }
        }

        public static ISession OpenSession()
        {
            ISessionFactory sessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                  .ConnectionString(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename='c:\users\baglan.ibragimov\documents\visual studio 2015\Projects\BEEL\BEEL\App_Data\BeelineDB.mdf';Integrated Security=True;MultipleActiveResultSets=True;Application Name=EntityFramework")
                              .ShowSql()
                )
               .Mappings(m =>
                          m.FluentMappings
                              .AddFromAssemblyOf<DBContext>())
                .ExposeConfiguration(cfg => new SchemaExport(cfg).Create(false, false))
                .BuildSessionFactory();
            return sessionFactory.OpenSession();
        }
    }
}