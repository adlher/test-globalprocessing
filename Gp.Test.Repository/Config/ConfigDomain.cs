using Gp.Test.Entity;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace Gp.Test.Repository.Config
{
    public class ConfigDomain
    {
        public static void ConfigurationDomain(IServiceProvider serviceProvider, string jsonData)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings();
            List<Persona>? endPointItems = JsonConvert.DeserializeObject<List<Persona>>(jsonData, settings);

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                TestContext? context = serviceScope.ServiceProvider.GetService<TestContext>();

                if (context != default && endPointItems != default)
                {
                    context.AddRange(endPointItems);
                    context.SaveChanges();
                }
            }
        }
    }
}
