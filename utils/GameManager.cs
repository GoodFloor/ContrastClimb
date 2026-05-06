using ContrastClimb.levels;
using ContrastClimb.utils.ui.level_selection;
using Godot;

namespace ContrastClimb.utils;

public partial class GameManager : Node
{
    private Node2D _levelRoot;
    private PackedScene _currentLoadedLevel;
    private ParentLevel _currentInstanceLevel;

    private Node _cutsceneRoot;
    private PackedScene _cutsceneTemplate;
    private cutscenes.Cutscene _cutsceneInstance;

    private CanvasLayer _uiRoot;
    private Control _mainMenu;
    private LevelSelection _levelSelection;
    private Control _winScreen;
    private Control _failScreen;
    
    private int _currentLevelId;

    private int _colorChangesUsed;

    private bool _cutsceneScheduled;
    
    
    public override void _Ready()
    {
        Global.GameManager = this;
        
        Global.Config = new Config();
        Global.Config.LoadConfig();
        
        Global.Progress = new Progress();
        Global.Progress.LoadProgress();
        
        _levelRoot = GetNode<Node2D>("LevelRoot");
        _cutsceneRoot = GetNode<Node>("CutsceneRoot");
        _uiRoot = GetNode<CanvasLayer>("UIRoot");
        _mainMenu = _uiRoot.GetNode<Control>("MainMenu");
        _levelSelection = _uiRoot.GetNode<LevelSelection>("LevelSelection");
        _winScreen = _uiRoot.GetNode<Control>("WinScreen");
        _failScreen = _uiRoot.GetNode<Control>("FailScreen");
        
        _cutsceneTemplate = ResourceLoader.Load<PackedScene>("res://cutscenes/cutscene.tscn");
        
        PauseGame();
        
        _levelSelection.GenerateLevelsList();

        _currentLevelId = Global.Progress.LatestLevelId;
        LoadNewLevel($"level_{_currentLevelId}");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);

        if (@event.IsActionPressed("switch_color"))
            _colorChangesUsed++;
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
        _uiRoot.Visible = false;
        GetTree().Paused = false;
        if (_cutsceneScheduled)
        {
            
        }
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

            int score;
            if (_colorChangesUsed < _currentInstanceLevel.ScoreTier3)
                score = 3;
            else if (_colorChangesUsed < _currentInstanceLevel.ScoreTier2)
                score = 2;
            else if (_colorChangesUsed < _currentInstanceLevel.ScoreTier1)
                score = 1;
            else
                score = 0;

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
            LoadNewLevel($"level_{_currentLevelId}");
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
        
        // Reset the score
        _colorChangesUsed = 0;

        _currentInstanceLevel = _currentLoadedLevel.Instantiate<ParentLevel>();

        if (_currentInstanceLevel.Cutscene == null)
        {
            _levelRoot.AddChild(_currentInstanceLevel);
        }
        else
        {
            _cutsceneScheduled = true;
            LoadCutscene(_currentInstanceLevel.Cutscene);
        }
        
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
        _cutsceneScheduled = false;
        _cutsceneInstance?.QueueFree();
        _levelRoot.AddChild(_currentInstanceLevel);
    }
}
