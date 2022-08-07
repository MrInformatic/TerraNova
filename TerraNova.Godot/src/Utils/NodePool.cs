using Godot;
using System.Collections.Generic;

namespace TerraNova.Godot.Utils
{
    public class NodePool<T> where T : Node
    {
        private PackedScene Scene;
        private List<T> Nodes;

        public NodePool(PackedScene Scene, int iCapacity)
        {
            Nodes = new List<T>(iCapacity);

            for (var i = 0; i < iCapacity; i++)
            {
                var Node = Scene.Instance<T>();
                Nodes.Add(Node);
            }
        }

        public T CreateInstance()
        {
            var iNodeCount = Nodes.Count;
            if (iNodeCount > 0)
            {
                var pNode = Nodes[iNodeCount - 1];
                Nodes.RemoveAt(iNodeCount - 1);
                return pNode;
            }
            else
            {
                return Scene.Instance<T>();
            }
        }

        public void ReclaimInstance(T instance)
        {
            Nodes.Add(instance);
        }
    }
}