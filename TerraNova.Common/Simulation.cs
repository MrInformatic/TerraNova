using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TerraNova.Common
{
    public class Simulation
    {
        public static readonly Simulation Instance = new Simulation();

        public Dictionary<Guid, SimulationObject> SimulationObjects { get; } = new Dictionary<Guid, SimulationObject>();

        private Simulation() { }

        public static void Spawn(SimulationObject pSimulationObject)
        {
            pSimulationObject.Spawn();
        }

        public static void DeSpawn(SimulationObject pSimulationObject)
        {
            pSimulationObject.DeSpawn();
        }

        public static void DeSpawn(Guid xGuid)
        {
            Instance.SimulationObjects[xGuid].DeSpawn();
        }
    }
}