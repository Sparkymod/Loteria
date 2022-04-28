using System.Reflection;

namespace Loteria.Data.Extensions
{
    public static class BuildExtensions
    {
        public static void AddAllServicesAvailable(this IServiceCollection services, string @namespace = "Loteria.Data.Services")
        {
            List<Type> serviceClassList = Assembly.GetExecutingAssembly().GetTypes().Where(t => !t.IsAbstract && t.IsClass && t.Namespace == @namespace && t.Name.EndsWith("Services")).ToList();
            foreach (Type service in serviceClassList)
            {
                services.AddTransient(service);
            }
        }
    }
}
