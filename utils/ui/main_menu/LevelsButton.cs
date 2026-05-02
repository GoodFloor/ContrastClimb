using Godot;

namespace ContrastClimb.utils.ui.main_menu;

public partial class LevelsButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.OpenLevelSelection();
    }
}