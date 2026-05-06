using System;
using ContrastClimb.characters.player;
using Godot;

namespace ContrastClimb.environment.platforms.single_use;

public partial class SingleUsePlatform : ParentPlatform
{
    private Area2D _platformTop;
    private double _delay = 0.5;
    private bool _used;
    public override void _Ready()
    {
        base._Ready();
        
        _platformTop = GetNode<Area2D>("PlatformTop");
        if (!IsActive)
            _platformTop.CollisionMask = 0;
        else
            _platformTop.CollisionMask = CollisionLayer;
    }

    protected override void SwitchColor()
    {
        if (_used) return;
        
        base.SwitchColor();

        if (!IsActive)
            _platformTop.CollisionMask = 0;
        else
            _platformTop.CollisionMask = CollisionLayer;
    }

    public override void PlayerLanded()
    {
        base.PlayerLanded();
        
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
}