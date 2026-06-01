using ContrastClimb.characters.player;
using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.environment.collectibles;

public partial class EnergyOrb : Area2D
{
    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player)
            return;
        
        Global.GameManager.EnergyLeftOverlay.AddEnergy();
        QueueFree();
    }
}