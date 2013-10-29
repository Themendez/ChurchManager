using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.Windsor;
using Castle.Windsor.Installer;

namespace ChurchManager
{
    public class ContainerConfig
    {
        private const string AssembliesKey = "IoCAssemblies";
        public static void Init(IWindsorContainer container)
        {
            string assembliesNames = ConfigurationManager.AppSettings[AssembliesKey];

            Assembly[] assemblies = assembliesNames.Split(',')
                .Select(Assembly.Load)
                .ToArray();
            foreach (Assembly assembly in assemblies)
            {
                container.Install(FromAssembly.Instance(assembly));
            }
            
        }
    }
}