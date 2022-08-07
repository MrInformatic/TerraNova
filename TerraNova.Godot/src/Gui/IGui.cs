using Godot;

namespace TerraNova.Godot.Gui
{
    public interface IGui<T>
    {
        void Open(T pData);

        void Close();
    }
}