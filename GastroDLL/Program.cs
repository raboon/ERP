using System;

using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gastro
{
    class Program
    {
        public static string server = "localhost\\SQLEXPRESS", database = "erp", user = "sa", pass = "raboon430";
        static bool dologging = true;
        static bool initDB = true;
        static void Main(string[] args)
        {
            // init logging
            Logging l = new Logging(dologging, Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "_" + DateTime.Now.ToString("yyyyMMdd") + ".txt");
            l.WriteLine(@"ERPDBCore v1.00 - ERP Database Deployment Tool (c) 2016, ***StarLab Ltd.***");

            // check database
            try
            {
                Database db = new Database(server, database, user, pass);
                //ContextFactory contextFactory = new ContextFactory("sqlConn");
                //ERPDBCore.ERPContext context = db.GetContext(); //Get first context
                db.CheckDatabase(true, db.GetContext(true));
                //if (!contextFactory.CheckDatabase(initDB, contextFactory.Create()))
                   //l.WriteLine(@" database error; message was already printed.");
            }
            catch (Exception e)
            {
                l.WriteException(e, "Database Init");

            }
            Console.ReadKey();
        }
    }
}
