using System.Collections.Generic;
using System.Linq;
using Godot;

namespace TerraNova.Utils
{
    public static class NodeSearch
    {
        public static IEnumerable<Node> GetParrents(this Node pNode)
        {
            for (var pParrent = pNode; pParrent != null; pParrent = pParrent.GetParent())
            {
                yield return pParrent;
            }
        }

        public static IEnumerable<T> GetParrentsOfType<T>(this Node pNode) where T : Node
        {
            return pNode.GetParrents().OfType<T>();
        }

        public static bool TryGetParrentOfType<T>(this Node pNode, out T pValue) where T : Node
        {
            foreach (var pParrent in pNode.GetParrentsOfType<T>())
            {
                pValue = pParrent;
                return true;
            }

            pValue = default;
            return false;
        }
    }
}