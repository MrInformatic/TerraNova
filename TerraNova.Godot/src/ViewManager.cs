using System.Collections.Generic;
using System;
using Godot;
using TerraNova.Common;
using TerraNova.Godot.Utils;
using System.Reflection;

namespace TerraNova.Godot
{
    public class ViewManager : Node
    {
        public Simulation Simulation { get; } = new Simulation();
        public Dictionary<Type, PackedScene> pViewPrefabs { get; } = new Dictionary<Type, PackedScene>();
        public Dictionary<Guid, IView> pViews { get; } = new Dictionary<Guid, IView>();
        public SimulationObject SelectedObject { get; set; }

        public override void _Ready()
        {
            foreach (var pViewPrefab in ResourceSearch.GetAllResourcesOfType<PackedScene>("res://scenes/views/"))
            {
                var pNode = pViewPrefab.Instance();
                var pType = pNode.GetType();
                var pViewAttribute = pType.GetCustomAttribute<ViewAttribute>();
                if (pViewAttribute != null)
                {
                    foreach (var pViewedType in pViewAttribute.ViewedTypes)
                    {
                        if (!pViewPrefabs.ContainsKey(pViewedType))
                        {
                            pViewPrefabs[pViewedType] = pViewPrefab;
                        }
                        else
                        {
                            GD.PrintErr($"{pViewPrefabs[pViewedType].ResourcePath} and {pViewPrefab.ResourcePath} both handle {pViewedType.Name}");
                        }
                    }
                }
            }

            if (this.TryGetParrentOfType<GameRoot>(out var pGameRoot))
            {
                pGameRoot.ViewManager = this;
            }
        }

        public override void _Process(float delta)
        {
            foreach (var pSimulationObject in Simulation.SimulationObjects.Values)
            {
                if (!pViews.ContainsKey(pSimulationObject.Guid))
                {
                    var pType = pSimulationObject.GetType();
                    if (pViewPrefabs.TryGetValue(pType, out var pViewPrefab))
                    {
                        var pView = pViewPrefab.Instance<IView>();
                        pView.SimulationObject = pSimulationObject;
                        pView.ViewManager = this;
                        pViews[pSimulationObject.Guid] = pView;
                        AddChild((Node)pView);
                    }
                    else
                    {
                        GD.PrintErr($"Could not handle {pType.Name}: No view prefab found!");
                    }
                }
            }
        }
    }
}