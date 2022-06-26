using Godot;

namespace TerraNova.Gui
{
    public interface IGui<T>
    {
        void Open(T pData);

        void Close();
    }
}