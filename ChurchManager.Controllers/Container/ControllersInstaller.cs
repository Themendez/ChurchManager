using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using AutoMapper;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using ChurchManager.Controllers.Controllers;
using ChurchManager.Controllers.Mappings;

namespace ChurchManager.Controllers.Container
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            //Controllers
            container.Register(
                Classes.FromAssemblyContaining<HomeController>()
                       .BasedOn<IController>()
                       .LifestylePerWebRequest()
                );

            //Model Mappings
            Mapper.Initialize(cfg =>
            {
                IEnumerable<Type> mappings = typeof(IModelMapping).Assembly.GetTypes()
                                                                   .Where(
                                                                       t =>
                                                                       typeof(IModelMapping).IsAssignableFrom(t)
                                                                       && t.IsClass && !t.IsAbstract);

                foreach (Type mapping in mappings)
                {
                    IModelMapping instance = (IModelMapping)Activator.CreateInstance(mapping);
                    instance.Map(cfg);
                }
            });

            ControllerFactory controllerFactory = new ControllerFactory(container.Kernel);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);
        }
    }
}
