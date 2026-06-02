using ContrastClimb.characters.player;
using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.environment.collectibles;

public partial class EnergyOrb : Area2D
{
    private bool _currentlyWhite;
    
    private void OnBodyEntered(Node2D body)
    {
        if (body is not Player)
            return;
        
        Global.GameManager.EnergyLeftOverlay.AddEnergy();
        QueueFree();
    }

    public override void _Ready()
    {
        base._Ready();

        Global.GameManager.Connect(
            GameManager.SignalName.ColorChanged,
            new Callable(this, MethodName.OnColorChanged)
        );

        Modulate = GColors.Dark;
        _currentlyWhite = false;
        
        GetNode<AnimatedSprite2D>("AnimatedSprite2D").Play("idle");
    }

    private void OnColorChanged()
    {
        _currentlyWhite = !_currentlyWhite;

        Modulate = _currentlyWhite ? GColors.Light : GColors.Dark;
    }
}