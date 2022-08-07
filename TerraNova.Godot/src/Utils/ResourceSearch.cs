using Godot;
using System.Collections.Generic;
using System.Linq;

namespace TerraNova.Godot.Utils
{
    public static class ResourceSearch
    {
        public static IEnumerable<Resource> GetAllResources(string sPath)
        {
            var pDirectory = new Directory();
            pDirectory.Open(sPath);
            pDirectory.ListDirBegin(skipNavigational: true);
            string sChild = null;

            while (!string.IsNullOrWhiteSpace(sChild = pDirectory.GetNext()))
            {
                var pChildPath = System.IO.Path.Combine(pDirectory.GetCurrentDir(), sChild);

                if (pDirectory.CurrentIsDir())
                {
                    foreach (var pResource in GetAllResources(pChildPath))
                    {
                        yield return pResource;
                    }
                }
                else
                {
                    yield return ResourceLoader.Load(pChildPath);
                }
            }
        }

        public static IEnumerable<T> GetAllResourcesOfType<T>(string sPath) where T : Resource
        {
            return GetAllResources(sPath).OfType<T>();
        }
    }
}