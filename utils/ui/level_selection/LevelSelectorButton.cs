using Godot;

namespace ContrastClimb.utils.ui.level_selection;

public partial class LevelSelectorButton : Button
{
    private Sprite2D _scoreLabel;
    private int _levelId;
    private int _levelScore;
    private static Texture2D[]  _scoreTexture;
    
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
            _scoreLabel.Texture = _scoreTexture[_levelScore];
        }
    }

    public override void _Ready()
    {
        base._Ready();
        
        _scoreLabel = GetNode<Sprite2D>("Score");
        
        _scoreTexture = [GD.Load<Texture2D>("res://utils/ui/result_stars/sprites/star0.png"), 
            GD.Load<Texture2D>("res://utils/ui/result_stars/sprites/star1.png"), 
            GD.Load<Texture2D>("res://utils/ui/result_stars/sprites/star2.png"), 
            GD.Load<Texture2D>("res://utils/ui/result_stars/sprites/star3.png")];
        
    }

    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.PlayLevel(_levelId);
    }
    
    
}