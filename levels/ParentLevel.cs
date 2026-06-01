using Godot;
using ContrastClimb.characters.player;
using ContrastClimb.utils;

namespace ContrastClimb.levels;

public partial class ParentLevel : Node2D
{
    [Export] public int StartingEnergy;
    [Export] public int EnergyLeftOk;
    [Export] public int EnergyLeftPerfect;
    [Export(PropertyHint.File, "*.ogv")] public string Cutscene;
    
    private CameraPath _cameraPath;
    private Area2D _cameraStartArea;

    public override void _Ready()
    {
        base._Ready();
        _cameraPath = GetNode<CameraPath>("CameraPath");
        _cameraStartArea = GetNode<Area2D>("AreaStartingCamera");

        _cameraStartArea.BodyEntered += CameraStartArea;
        
        OnReady();
    }

    public override void _PhysicsProcess(double delta)
    {
        base._PhysicsProcess(delta);
        var df = (float)delta;
        
        OnPhysicsProcess(df);
    }

    private void CameraStartArea(Node2D body)
    {
        if (body is not Player)
            return;
        
        _cameraPath.StartCamera();
        _cameraStartArea.QueueFree();
    }

    protected virtual void OnReady()
    {
        // Can do something in particular levels
    }
    
    protected virtual void OnPhysicsProcess(float delta)
    {
        // Can do something in particular levels
    }
}