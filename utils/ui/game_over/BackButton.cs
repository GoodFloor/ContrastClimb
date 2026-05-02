using Godot;

namespace ContrastClimb.utils.ui.game_over;

public partial class BackButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.RestartLevel();
    }
}