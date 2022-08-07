using System;

namespace TerraNova.Common
{
    public class SimulationObject
    {
        public Guid Guid { get; private set; } = Guid.Empty;

        public void Spawn(Simulation pSimulation)
        {
            if (Guid == Guid.Empty && CanSpawn)
            {
                Guid xGuid = Guid.Empty;
                do
                {
                    xGuid = Guid.NewGuid();
                }
                while (pSimulation.SimulationObjects.ContainsKey(xGuid));

                Guid = xGuid;
                pSimulation.SimulationObjects[xGuid] = this;
                OnSpawn();
            }
        }

        public void DeSpawn(Simulation pSimulation)
        {
            if (Guid != Guid.Empty)
            {
                OnDeSpawn();
                pSimulation.SimulationObjects.Remove(Guid);
                Guid = Guid.Empty;
            }
        }

        protected virtual bool CanSpawn { get { return true; } }
        protected virtual void OnSpawn() { }
        protected virtual void OnDeSpawn() { }
    }
}