using System;
using ContrastClimb.characters.player;
using ContrastClimb.levels;
using ContrastClimb.utils.ui;
using ContrastClimb.utils.ui.level_selection;
using Godot;

namespace ContrastClimb.utils;

public partial class GameManager : Node
{
    [Signal]
    public delegate void ColorChangedEventHandler();
    [Signal]
    public delegate void ConfigLoadedEventHandler();
    
    private Node2D _levelRoot;
    private PackedScene _currentLoadedLevel;
    private ParentLevel _currentInstanceLevel;
    public Player Player;

    private Node _cutsceneRoot;
    private PackedScene _cutsceneTemplate;
    private cutscenes.Cutscene _cutsceneInstance;

    private CanvasLayer _uiRoot;
    private Control _mainMenu;
    private LevelSelection _levelSelection;
    private Control _settingsScreen;
    private Control _winScreen;
    private Control _failScreen;
    private Sprite2D _winScreenScore;
    public EnergyLeftOverlay EnergyLeftOverlay;
    
    private int _currentLevelId;
    
    public override void _Ready()
    {
        Global.GameManager = this;
        
        Global.Config = new Config();
        Global.Config.LoadConfig();
        
        Global.Progress = new Progress();
        Global.Progress.LoadProgress();
        EmitSignal(SignalName.ConfigLoaded);
        
        _levelRoot = GetNode<Node2D>("LevelRoot");
        _cutsceneRoot = GetNode<Node>("CutsceneRoot");
        _uiRoot = GetNode<CanvasLayer>("UIRoot");
        EnergyLeftOverlay = _levelRoot.GetNode<EnergyLeftOverlay>("EnergyLeftOverlay");
        
        _mainMenu = _uiRoot.GetNode<Control>("MainMenu");
        _levelSelection = _uiRoot.GetNode<LevelSelection>("LevelSelection");
        _settingsScreen = _uiRoot.GetNode<Control>("SettingsScreen");
        _winScreen = _uiRoot.GetNode<Control>("WinScreen");
        _failScreen = _uiRoot.GetNode<Control>("FailScreen");
        _winScreenScore = _winScreen.GetNode<Sprite2D>("Score");
        
        _cutsceneTemplate = ResourceLoader.Load<PackedScene>("res://cutscenes/cutscene.tscn");

        Global.ScoreTexture = [GD.Load<Texture2D>("res://utils/ui/stars_0.png"), 
            GD.Load<Texture2D>("res://utils/ui/stars_1.png"), 
            GD.Load<Texture2D>("res://utils/ui/stars_2.png"), 
            GD.Load<Texture2D>("res://utils/ui/stars_3.png")];
        
        PauseGame();
        
        _levelSelection.GenerateLevelsList();

        _currentLevelId = Global.Progress.LatestLevelId;
        LoadNewLevel($"level_{_currentLevelId}");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("switch_color") && EnergyLeftOverlay.Value > 0)
            EmitSignalColorChanged();
    }

    public void PauseGame()
    {
        GetTree().Paused = true;
        _uiRoot.Visible = true;
        _mainMenu.Visible = true;
        _levelSelection.Visible = false;
        _settingsScreen.Visible = false;
        _winScreen.Visible = false;
        _failScreen.Visible = false;
        EnergyLeftOverlay.Visible = false;
    }

    public void ResumeGame()
    {
        _uiRoot.Visible = false;
        EnergyLeftOverlay.Visible = true;
        GetTree().Paused = false;
    }

    public void PlayLevel(int levelId)
    {
        _currentLevelId = levelId;
        
        LoadNewLevel($"level_{_currentLevelId}");
        ResumeGame();
    }

    public void EndLevel(bool success)
    {
        PauseGame();

        _mainMenu.Visible = false;
        if (success)
        {
            _winScreen.Visible = true;

            var score = 1;
            if (EnergyLeftOverlay.Value >= _currentInstanceLevel.EnergyLeftPerfect)
                score = 3;
            else if (EnergyLeftOverlay.Value >= _currentInstanceLevel.EnergyLeftOk)
                score = 2;
            
            _winScreenScore.Texture = Global.ScoreTexture[score];

            if (score > Global.Progress.GetLevelScore(_currentLevelId))
            {
                Global.Progress.SetLevelScore(_currentLevelId, score);
                _levelSelection.ChangeScore(_currentLevelId, score);
            }

            // If there is a next level - unlock it and load it
            if (_currentLevelId >= Progress.LevelCount - 1) return;
            _currentLevelId++;
            Global.Progress.UnlockLevel(_currentLevelId);
            _levelSelection.UnlockLevel(_currentLevelId);
            Global.Progress.LatestLevelId = _currentLevelId;
            _currentLoadedLevel = ResourceLoader.Load<PackedScene>($"res://levels/level_{_currentLevelId}.tscn");
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

    private void InstantiateLoadedLevel()
    {
        // Remove previously loaded level before loading a new one
        _currentInstanceLevel?.QueueFree();

        _currentInstanceLevel = _currentLoadedLevel.Instantiate<ParentLevel>();
        Player = _currentInstanceLevel.GetNode<Player>("Player");
        EnergyLeftOverlay.Value = _currentInstanceLevel.StartingEnergy;

        if (_currentInstanceLevel.Cutscene == null)
            _levelRoot.AddChild(_currentInstanceLevel);
        else
            LoadCutscene(_currentInstanceLevel.Cutscene);
    }

    private void LoadNewLevel(string levelName)
    {
        _currentLoadedLevel = ResourceLoader.Load<PackedScene>($"res://levels/{levelName}.tscn");
        
        InstantiateLoadedLevel();
    }

    private void LoadCutscene(string path)
    {
        _cutsceneInstance?.QueueFree();
        _cutsceneInstance = _cutsceneTemplate.Instantiate<cutscenes.Cutscene>();
        
        _cutsceneRoot.AddChild(_cutsceneInstance);
        _cutsceneInstance.SetSource(path);
    }
    
    public void EndCutscene()
    {
        _cutsceneInstance?.QueueFree();
        _cutsceneInstance = null;
        _levelRoot.AddChild(_currentInstanceLevel);
    }
    
    private void OpenSettings()
    {
        PauseGame();
        
        _mainMenu.Visible = false;
        _settingsScreen.Visible = true;
    }

    public void RegenerateLevelsList()
    {
        _levelSelection.ClearLevelsList();
        _levelSelection.GenerateLevelsList();
        
        _currentLevelId = Global.Progress.LatestLevelId;
        LoadNewLevel($"level_{_currentLevelId}");
    }
}
