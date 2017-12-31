using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Alphaleonis.Win32.Filesystem;
using System.Reflection;
using System.Collections;
using System.Globalization;
using Gastro;
using System.Data.Entity.Core.Objects;
using GastroDLL;

namespace Gastro
{
    public class Database
    {
        string connstring;
        string server;
        string database;
        char sepdec = '.';
        public enum SetType { flow, profiling, result, logset, other}

        int roundPrecision = 4;

        /// <summary>
        /// Database Constructor
        /// used to init the connection string to the database
        /// </summary>
        /// <param name="server"></param>
        /// <param name="database"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="l"></param>
        public Database(string server, string database, string user, string pass)
        {
            this.server = server ?? "localhost";
            this.database = database ?? "erp";
            user = user ?? "root";
            pass = pass ?? "raboon430";
            bool SSPI = string.IsNullOrWhiteSpace(user) && string.IsNullOrWhiteSpace(pass);
            this.connstring = string.Format("Data Source={0};Initial Catalog={1};{2};MultipleActiveResultSets=True;App=TFR_KPIdb", this.server, this.database,
                SSPI ? "Integrated Security=SSPI" : string.Format("uid=\"{0}\";pwd=\"{1}\"", user, pass));
            sepdec = Utility.GetRegionDecimalSeperator();
        }
       
        public bool CheckDatabase(bool initialize, Gastro db)
        {
                if (!db.Database.Exists())
                {
                    if (initialize)
                    {
                        if (!db.Database.CreateIfNotExists())
                            throw new System.Exception("Database Model creation failed");
                    }
                    else
                        throw new System.Exception("Database Model not exists");
                }
                if (!db.Database.CompatibleWithModel(false))
                {
                    throw new System.Exception("Database Model is incompatible");
                }
                return true; 
        }

        public Gastro GetContext(bool detect = false)
        {
            Gastro context = new Gastro(connstring);
            context.Configuration.ProxyCreationEnabled = false;
            context.Configuration.AutoDetectChangesEnabled = detect;
            return context;
        }

        

    }   

}
 