using System;
using Castle.Windsor;
using ChurchManager.Persistence;
using NHibernate.Tool.hbm2ddl;
using log4net;

namespace ChurchManager.SchemaGenerator
{
    class Program
    {
        private static ILog _log = LogManager.GetLogger(typeof(Program));
        private static IWindsorContainer _container;

        private static NHibernate.Cfg.Configuration _configuration;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                return;
            }

            string option = args[0];

            switch (option)
            {
                case "-update":
                    UpdateSchema();
                    break;
                case "-drop-create":
                    CreateSchema();
                    break;
                default:
                    Console.WriteLine("Valid parameter values are: -update|-drop-create");
                    break;
            }
            Console.ReadKey();
        }

        private static void InitializeFramework()
        {
            _container = new WindsorContainer();
            PersistenceManager.Init(_container);
            _configuration = PersistenceManager.Configuration;
        }

        private static void CreateSchema()
        {
            _log.Debug("Recreating DB...");
            InitializeFramework();

            SchemaExport export = new SchemaExport(_configuration);
            export.SetOutputFile("script.sql");
            export.Drop(true, true);
            export.Create(true, true);
            _log.Debug("Schema created...");
        }

        private static void UpdateSchema()
        {
            _log.Debug("Updating DB...");
            InitializeFramework();

            SchemaUpdate update = new SchemaUpdate(_configuration);
            update.Execute(false, true);
            _log.Debug("Schema updated...");
        }
    }
}
