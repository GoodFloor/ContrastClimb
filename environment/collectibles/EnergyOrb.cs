using ContrastClimb.characters.player;
using Godot;

namespace ContrastClimb.environment.collectibles;

public partial class EnergyOrb : Area2D
{
    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player)
            return;
        
        GD.Print("Energy Orb collected");
        QueueFree();
    }
}