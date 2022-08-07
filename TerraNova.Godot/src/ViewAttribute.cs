using System;

namespace TerraNova.Godot
{
    public class ViewAttribute : Attribute
    {
        public Type[] ViewedTypes { get; }

        public ViewAttribute(params Type[] pViewedTypes)
        {
            ViewedTypes = pViewedTypes;
        }
    }
}