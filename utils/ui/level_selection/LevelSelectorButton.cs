using Godot;

namespace ContrastClimb.utils.ui.level_selection;

public partial class LevelSelectorButton : Button
{
    private Label _scoreLabel;
    private int _levelId;
    private int _levelScore;
    
    [Export]
    public int LevelId
    {
        get => _levelId;
        set
        {
            Text = $"Level {value}";
            _levelId = value;
        }
    }

    public int LevelScore
    {
        get => _levelScore;
        set
        { 
            _levelScore = value;
            _scoreLabel.Text = $"{_levelScore} / 3";
        }
    }

    public override void _Ready()
    {
        base._Ready();
        
        _scoreLabel = GetNode<Label>("Score");
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.PlayLevel($"level_{_levelId}");
    }
    
    
}