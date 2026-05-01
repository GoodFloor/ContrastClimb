using Godot;
using System;

public partial class AreaStartingCamera : Area2D
{
    private void OnBodyEntered(Node2D node)
    {
        if (node is Player)
            GetParent<DemoLevel>().StartCamera();
    }
}
