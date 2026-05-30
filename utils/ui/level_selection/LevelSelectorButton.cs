using Godot;

namespace ContrastClimb.utils.ui.level_selection;

public partial class LevelSelectorButton : Button
{
    private Sprite2D _scoreLabel;
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
            _scoreLabel.Texture = Global.ScoreTexture[_levelScore];
        }
    }

    public override void _Ready()
    {
        base._Ready();
        
        _scoreLabel = GetNode<Sprite2D>("Score");
        
        
        
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.PlayLevel(_levelId);
    }
    
    
}