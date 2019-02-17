using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Reflection;

namespace Upope.ServiceBase.Extensions
{
    //public static class ApplyConfigurationsExtension
    //{
    //    public static void ApplyConfigurationsFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
    //    {
    //        var configurations = assembly.DefinedTypes.Where(t =>
    //             t.ImplementedInterfaces.Any(i =>
    //                i.IsGenericType &&
    //                i.Name.Equals(typeof(IEntityTypeConfiguration<>).Name,
    //                       StringComparison.InvariantCultureIgnoreCase)
    //              ) &&
    //              t.IsClass &&
    //              !t.IsAbstract &&
    //              !t.IsNested)
    //              .ToList();

    //        foreach (var configuration in configurations)
    //        {
    //            var entityType = configuration.BaseType.GenericTypeArguments.SingleOrDefault(t => t.IsClass);

    //            var applyConfigMethod = typeof(ModelBuilder).GetMethod("ApplyConfiguration");

    //            var applyConfigGenericMethod = applyConfigMethod.MakeGenericMethod(entityType);

    //            applyConfigGenericMethod.Invoke(modelBuilder,
    //                    new object[] { Activator.CreateInstance(configuration) });
    //        }
    //    }​​​​​​
    //}
}
