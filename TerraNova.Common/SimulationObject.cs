using System;

namespace TerraNova.Common
{
    public class SimulationObject
    {
        public Guid Guid { get; private set; } = Guid.Empty;
        public bool IsValid => Guid != Guid.Empty;

        public void Spawn()
        {
            if (Guid == Guid.Empty && CanSpawn)
            {
                Guid xGuid = Guid.Empty;
                do
                {
                    xGuid = Guid.NewGuid();
                }
                while (Simulation.Instance.SimulationObjects.ContainsKey(xGuid));

                Guid = xGuid;
                Simulation.Instance.SimulationObjects[xGuid] = this;
                OnSpawn();
            }
        }

        public void DeSpawn()
        {
            if (Guid != Guid.Empty)
            {
                OnDeSpawn();
                Simulation.Instance.SimulationObjects.Remove(Guid);
                Guid = Guid.Empty;
            }
        }

        protected virtual bool CanSpawn { get { return true; } }
        protected virtual void OnSpawn() { }
        protected virtual void OnDeSpawn() { }

        public SimulationObject GetSimulationObject(Guid xGuid)
        {
            return Simulation.Instance.SimulationObjects.TryGetValue(xGuid, out var pSimualtionObject) ? pSimualtionObject : null;
        }

        public T GetSimulationObject<T>(Guid xGuid) where T : SimulationObject
        {
            return Simulation.Instance.SimulationObjects.TryGetValue(xGuid, out var pSimualtionObject) ? pSimualtionObject as T : null;
        }

        public bool ValidGuid(Guid xGuid)
        {
            return Simulation.Instance.SimulationObjects.ContainsKey(xGuid);
        }
    }
}