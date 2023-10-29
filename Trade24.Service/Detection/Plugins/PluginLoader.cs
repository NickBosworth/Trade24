using System.Reflection;

namespace Trade24.Service.Detection.Plugins
{
    public static class PluginLoader
    {
        public static List<IDetector> LoadPlugins()
        {
            var plugins = new List<IDetector>();

            // Get the current assembly (can be adjusted for external assemblies)
            var assembly = Assembly.GetExecutingAssembly();

            foreach (var type in assembly.GetTypes())
            {
                if (type.IsInterface || type.IsAbstract)
                {
                    continue; // Skip interfaces and abstract classes
                }
                else
                {
                    if (type.GetInterface(typeof(IDetector).FullName) != null)
                    {
                        var plugin = Activator.CreateInstance(type) as IDetector;
                        plugins.Add(plugin);
                    }
                }
            }

            return plugins;
        }

    }
}
