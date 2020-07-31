using System;
using Petroteks.Core.Entities;
using Petroteks.MvcUi.ExtensionMethods;

namespace Petroteks.MvcUi.Services
{
    public class DbServiceProvider
    {
        private readonly IServiceProvider serviceProvider;

        public DbServiceProvider(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
        public static dynamic Execute(Type type, DbServiceProvider dbServiceProvider) =>
                typeof(DbServiceProvider)
                .GetMethod("ExecuteServiceProviderGeneric")
                .MakeGenericMethod(new Type[] { type })
                .Invoke(dbServiceProvider, null);

        public dynamic ExecuteServiceProviderGeneric<T>() where T : EntityBase, new() =>
            serviceProvider.GetDbService<T>();
    }
}
