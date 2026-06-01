using Godot;

namespace ContrastClimb.utils.ui.main_menu;

public partial class ResetProgressButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        
        Global.Progress.ResetProgress();
        Global.GameManager.RegenerateLevelsList();
    }
}