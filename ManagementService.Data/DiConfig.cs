using Autofac;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using URF.Core.Abstractions;
using URF.Core.EF;

namespace ManagementService.Data
{
    public class DiRegister : Autofac.Module
    {


        public ContainerBuilder _container { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var Service = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(Service)
                   .AsImplementedInterfaces();


            builder.RegisterType(typeof(DatabaseContext))
                .As(typeof(DatabaseContext))
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(DatabaseContext))
             .As(typeof(Microsoft.EntityFrameworkCore.DbContext))
             .InstancePerLifetimeScope();
            

            _container = builder;
        }
    }
}
