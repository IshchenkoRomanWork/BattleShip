using Autofac;
using Autofac.Integration.Mvc;
using BattleShipMVC.Filters;
using BattleShipMVC.Interfaces;
using BattleShipMVC.Models;
using Microsoft.AspNet.Identity;
using ORM.Interfaces;
using ORM.Services;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace BattleShipMVC.App_Start
{
    public class IoC
    {
        public static void SetDependencies()
        {
            var builder = new ContainerBuilder();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());

            builder.RegisterGeneric(typeof(ORM<>)).As(typeof(IORM<>)).WithParameter("connectionString", ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            builder.RegisterGeneric(typeof(BattleShipRepository<>)).As(typeof(IBattleShipRepository<>));

            builder.RegisterType<BattleShipUserStore>().As<IUserStore<BattleShipUserIdentity, int>>();
            builder.RegisterType<ExceptionFilter>().SingleInstance();
            builder.RegisterType<Logger>().As<ILogger>();

            builder.RegisterFilterProvider();

            IContainer container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}