using Godot;
using System;

public partial class GameManager : Node
{
    private Node2D _levelRoot;
    private PackedScene _currentLoadedLevel;
    private Node2D _currentInstanceLevel;
    
    public override void _Ready()
    {
        Global.GameManager = this;
        Global.Config = new Config();
        Global.Config.LoadConfig();
        
        _levelRoot = GetNode<Node2D>("LevelRoot");
        
        
        _currentLoadedLevel = ResourceLoader.Load<PackedScene>("res://levels/demo_level.tscn");
        _currentInstanceLevel = _currentLoadedLevel.Instantiate<Node2D>();
        _levelRoot.AddChild(_currentInstanceLevel);
    }
}
