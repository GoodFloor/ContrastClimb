using Godot;
using System;

public partial class GameManager : Node
{
    private Node2D _levelRoot;
    private PackedScene _currentLoadedLevel;
    private Node2D _currentInstanceLevel;

    private CanvasLayer _uiRoot;
    private Control _mainMenu;
    private Control _levelSelection;
    private Control _winScreen;
    private Control _failScreen;
    
    
    public override void _Ready()
    {
        Global.GameManager = this;
        Global.Config = new Config();
        Global.Config.LoadConfig();
        
        _levelRoot = GetNode<Node2D>("LevelRoot");
        _uiRoot = GetNode<CanvasLayer>("UIRoot");
        _mainMenu = _uiRoot.GetNode<Control>("MainMenu");
        _levelSelection = _uiRoot.GetNode<Control>("LevelSelection");
        _winScreen = _uiRoot.GetNode<Control>("WinScreen");
        _failScreen = _uiRoot.GetNode<Control>("FailScreen");
        
        PauseGame();
        
        PreloadLevel("level_0");
        InstantiateLoadedLevel();
    }

    public void PauseGame()
    {
        GetTree().Paused = true;
        _uiRoot.Visible = true;
        _mainMenu.Visible = true;
        _levelSelection.Visible = false;
        _winScreen.Visible = false;
        _failScreen.Visible = false;
    }

    public void ResumeGame()
    {
        GetTree().Paused = false;
        _uiRoot.Visible = false;
    }

    public void PlayLevel(string levelName)
    {
        PreloadLevel(levelName);
        InstantiateLoadedLevel();
        ResumeGame();
    }

    public void EndLevel(bool success)
    {
        PauseGame();

        _mainMenu.Visible = false;
        if (success)
        {
            _winScreen.Visible = true;
        }
        else
        {
            _failScreen.Visible = true;
        }
    }

    public void RestartLevel()
    {
        PauseGame();
        InstantiateLoadedLevel();
    }

    public void OpenLevelSelection()
    {
        PauseGame();
        _mainMenu.Visible = false;
        _levelSelection.Visible = true;
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
