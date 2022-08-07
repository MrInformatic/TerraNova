using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace TerraNova.Common
{
    public class Simulation
    {
        public Dictionary<Guid, SimulationObject> SimulationObjects { get; } = new Dictionary<Guid, SimulationObject>();

        public void Spawn(SimulationObject pSimulationObject)
        {
            pSimulationObject.Spawn(this);
        }

        public void DeSpawn(SimulationObject pSimulationObject)
        {
            pSimulationObject.DeSpawn(this);
        }

        public void DeSpawn(Guid xGuid)
        {
            SimulationObjects[xGuid].DeSpawn(this);
        }
    }
}