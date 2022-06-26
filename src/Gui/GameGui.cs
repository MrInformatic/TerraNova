using System;
using System.Collections.Generic;
using TerraNova.Utils;
using Godot;

namespace TerraNova.Gui
{
    public class GameGui : Control
    {
        private Dictionary<Type, object> pGuis = new Dictionary<Type, object>();

        public override void _Ready()
        {
            if (this.TryGetParrentOfType<GameState>(out var pGameState))
            {
                pGameState.GameGui = this;
            }
        }

        public void RegisterGui<T>(T pGui)
        {
            pGuis.Add(pGui.GetType(), pGui);
        }

        public T GetGui<T>()
        {
            return (T)pGuis[typeof(T)];
        }

        public T Open<T, D>(D pData) where T : IGui<D>
        {
            var pGui = GetGui<T>();
            pGui.Open(pData);
            return pGui;
        }

        public T Close<T, D>() where T : IGui<D>
        {
            var pGui = GetGui<T>();
            pGui.Close();
            return pGui;
        }
    }
}