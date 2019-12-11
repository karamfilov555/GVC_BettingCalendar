﻿using BC.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;

namespace BC.Web.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDomainServices(
        this IServiceCollection services)
        {
            Assembly
                .GetAssembly(typeof(IService))
                .GetTypes()
                .Where(t => t.IsClass && t.GetInterfaces().Any(i => i.Name == $"I{t.Name}"))
                .Select(t => new
                {
                    Interface = t.GetInterface($"I{t.Name}"),
                    Implementation = t
                })
                .ToList()
                .ForEach(s => services.AddScoped(s.Interface, s.Implementation));

            return services;
        }
    }

}