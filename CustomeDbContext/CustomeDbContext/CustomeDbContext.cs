using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure.Interception;

namespace CustomeDbContext
{
    public class CustomeDbContext : DbContext
    {
        private readonly QueryInterceptor qi = new QueryInterceptor();

        public CustomeDbContext(string connection, bool readuncommitted = false) : base(connection)
        {
            if (ConfigurationManager.AppSettings["DbContext:QueryInterceptor"] != null && ConfigurationManager.AppSettings["DbContext:QueryInterceptor"] == "on")
                DbInterception.Add(qi);
            else
                DbInterception.Remove(qi);

            if (readuncommitted)
                base.Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED");

            base.Database.CommandTimeout = 600;
        }
    }
}
