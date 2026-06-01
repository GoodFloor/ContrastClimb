using Godot;
using ContrastClimb.utils;

namespace ContrastClimb.environment;

public partial class Background : ColorRect
{
    private bool _isWhite = true;

    public override void _Ready()
    {
        base._Ready();
        
        Global.GameManager.Connect(
            GameManager.SignalName.ColorChanged, 
            new Callable(this, MethodName.OnColorChanged)
        );
    }

    private void OnColorChanged()
    {
        if (_isWhite)
        {
            _isWhite = false;
            Color = GColors.Dark;
        }
        else
        {
            _isWhite = true;
            Color = GColors.Light;
        }
    }
}
