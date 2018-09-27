using Autofac;
using Autofac.Core;
using AutoMapper;
using ManagementService.Data;
using ManagementService.Model.DbSets.Roles;
using ManagementService.Model.DbSets.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using URF.Core.Abstractions;
using URF.Core.Abstractions.Services;
using URF.Core.Abstractions.Trackable;
using URF.Core.EF;
using URF.Core.Services;

namespace ManagementService.Service
{
    public  class DiRegister : Autofac.Module
    {
        

        public static ContainerBuilder _container { get; set; }

        protected override void Load(ContainerBuilder builder)
        {
            var Service = Assembly.GetExecutingAssembly();
            builder.RegisterAssemblyTypes(Service)
                   .AsImplementedInterfaces();
    //        builder.RegisterAssemblyTypes(Assembly.Load("DataServices"))
    //.Where(t => t.Name.EndsWith("Repository"))
    //.AsImplementedInterfaces();

            builder.RegisterGeneric(typeof(Service<>))
                .As(typeof(IService<>))
                .InstancePerLifetimeScope();

            builder.RegisterGeneric(typeof(RepositoryX<>))
                .As(typeof(IRepositoryX<>))
                .InstancePerLifetimeScope();


            builder.RegisterType(typeof(UnitOfWork))
                .As(typeof(IUnitOfWork))
                .InstancePerLifetimeScope();

            builder.RegisterType(typeof(UnitOfWorkDatabaseContext))
                .As(typeof(IUnitOfWorkDatabaseContext))
                .InstancePerLifetimeScope();


            builder.RegisterType(typeof(DatabaseContext))
                .As(typeof(IDatabaseContext))
                    .InstancePerLifetimeScope();


            var dbContextParameter = new ResolvedParameter((pi, ctx) => pi.ParameterType == typeof(IdentityDbContext),
                                                        (pi, ctx) => ctx.Resolve<DatabaseContext>());

            builder.RegisterType<CustomUserStore>()
                //RegisterType<UserStore<User, Role, DatabaseContext, Guid,UserClaim,UsersInRole,UserLogin,UserToken,RoleClaim>>()
                .As<IUserStore<User>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();

            builder.RegisterType<CustomRoleStore>()
                //RegisterType<UserStore<User, Role, DatabaseContext, Guid,UserClaim,UsersInRole,UserLogin,UserToken,RoleClaim>>()
                .As<IRoleStore<Role>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();

            builder.RegisterType<UserManager<User>>()
                .As<UserManager<User>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();

            builder.RegisterType<SignInManager<User>>()
                .As<SignInManager<User>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();

            //builder.RegisterType<RoleStore<Role, DatabaseContext,Guid>>()
            //    .As<IRoleStore<Role>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();

            builder.RegisterType<RoleManager<User>>()
                .As<RoleManager<User>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();

            builder.RegisterType<DbContextOptions<DatabaseContext>>()
                .As<DbContextOptions<DatabaseContext>>().WithParameter(dbContextParameter).InstancePerLifetimeScope();
            

            /*Automapper Di Config*/

            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new Mapping());
            });

            IMapper mapper = mappingConfig.CreateMapper();

            builder.RegisterInstance(mapper).As<IMapper>().SingleInstance();


            builder.RegisterModule(new ManagementService.Data.DiRegister());
            _container = builder;
        }
    }
}
