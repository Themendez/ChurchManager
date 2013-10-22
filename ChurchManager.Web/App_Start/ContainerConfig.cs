using System.IO;
using System.Linq;
using System.Reflection;
using Castle.MicroKernel.Registration;
using Castle.Windsor;
using ChurchManager.Core.Persistence;
using ChurchManager.Core.Service;
using ChurchManager.Persistence.Repository;

namespace ChurchManager
{
    public class ContainerConfig
    {
        public static void Init(IWindsorContainer container)
        {
            //NHibernate stuff
            Persistence.PersistenceManager.Init(container);

            Assembly[] assemblies = GetAllReferencedAssemblies();
            //Repositories
            container.Register(
                Component.For(typeof(IRepository<>))
                         .ImplementedBy(typeof(RepositoryBase<>))
                         .LifestyleTransient()
                );
            foreach (var assembly in assemblies)
            {
                container.Register(
                    Classes.FromAssembly(assembly)
                        .BasedOn(typeof(IRepository<>))
                        .WithService.DefaultInterfaces()
                        .Configure(c => c.LifestylePerWebRequest())
                    );
            }

            //Services
            foreach (Assembly assembly in assemblies)
            {
                container.Register(
                    Classes.FromAssembly(assembly)
                        .BasedOn<IApplicationService>()
                        .WithService.AllInterfaces()
                        .Configure(c => c.LifestylePerWebRequest())
                    );
            }
        }

        private static Assembly[] GetAllReferencedAssemblies()
        {
            string executingAssembly = Assembly.GetExecutingAssembly().Location;
            string location = Path.GetDirectoryName(executingAssembly);
            string[] assemblies = Directory.GetFiles(location, "*.dll");
            Assembly[] references = assemblies.Select(Assembly.LoadFrom).ToArray();
            return references;
        }
    }
}