using Godot;
using System;

public partial class GameManager : Node
{
    private Node2D _levelRoot;
    private PackedScene _currentLoadedLevel;
    private Node2D _currentInstanceLevel;

    private CanvasLayer _uiRoot;
    
    public override void _Ready()
    {
        GetTree().Paused = true;
        
        Global.GameManager = this;
        Global.Config = new Config();
        Global.Config.LoadConfig();
        
        _levelRoot = GetNode<Node2D>("LevelRoot");
        _uiRoot = GetNode<CanvasLayer>("UIRoot");
        
        PreloadLevel("demo_level");
        InstantiateLoadedLevel();
    }

    public void PauseGame()
    {
        GetTree().Paused = true;
        _uiRoot.Visible = true;
    }

    public void ResumeGame()
    {
        GetTree().Paused = false;
        _uiRoot.Visible = false;
    }

    public void EndLevel(bool success)
    {
        PauseGame();
        
        InstantiateLoadedLevel();
    }

    private void PreloadLevel(string levelName)
    {
        _currentLoadedLevel = ResourceLoader.Load<PackedScene>($"res://levels/{levelName}.tscn");
    }

    private void InstantiateLoadedLevel()
    {
        // Remove previously loaded level before loading a new one
        _currentInstanceLevel?.QueueFree();
        
        _currentInstanceLevel = _currentLoadedLevel.Instantiate<Node2D>();
        _levelRoot.AddChild(_currentInstanceLevel);
    }
}
