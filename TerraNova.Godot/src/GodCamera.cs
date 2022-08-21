using Godot;

namespace TerraNova.Godot
{
    public class GodCamera : Camera
    {
        public float Speed { get; set; } = 25f;
        public RayCast Raycast { get; private set; } = new RayCast();

        public override void _Process(float fDelta)
        {
            var xDirection = new Vector3();

            if (Input.IsActionPressed("up"))
            {
                xDirection.z -= 1;
            }

            if (Input.IsActionPressed("down"))
            {
                xDirection.z += 1;
            }

            if (Input.IsActionPressed("left"))
            {
                xDirection.x -= 1;
            }

            if (Input.IsActionPressed("right"))
            {
                xDirection.x += 1;
            }

            this.Translation += xDirection * Speed * fDelta;
        }
    }
}