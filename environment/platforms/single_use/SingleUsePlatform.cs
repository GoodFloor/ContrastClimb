using System;
using Godot;
using Godot.Collections;

namespace ContrastClimb.environment.platforms.single_use;

public partial class SingleUsePlatform : ParentPlatform
{
    private double _delay = 0.5;
    private bool _used;

    public override void _Ready()
    {
        base._Ready();

        if (IsWhite)
            CollisionLayer += 256;
        else
            CollisionLayer += 512;
    }

    public override void PlayerLanded()
    {
        base.PlayerLanded();
        
        if (_used) return;
        _used = true;
        
        DestroyNeighbours(GetNode<Area2D>("ScannerLeft").GetOverlappingBodies());
        DestroyNeighbours(GetNode<Area2D>("ScannerRight").GetOverlappingBodies());

        WaitAndDestroy();
    }

    private async void WaitAndDestroy()
    {
        try
        {
            await ToSignal(GetTree().CreateTimer(_delay, false), SceneTreeTimer.SignalName.Timeout);
            QueueFree();
        }
        catch (Exception e)
        {
            GD.PrintErr(e);
        }
    }

    private void DestroyNeighbours(Array<Node2D> neighbours)
    {
        foreach (var neighbour in neighbours)
        {
            if (neighbour is not SingleUsePlatform platform) return;
            platform.PlayerLanded();
        }
    }
    
}