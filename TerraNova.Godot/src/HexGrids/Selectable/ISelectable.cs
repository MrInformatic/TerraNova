namespace TerraNova.Godot.HexGrids.Selectables
{
    public interface ISelectable : IView
    {
        Selector Selector { get; set; }
    }
}