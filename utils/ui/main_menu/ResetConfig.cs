using Godot;

namespace ContrastClimb.utils.ui.main_menu;

public partial class ResetConfig : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        Global.Config.ResetConfig();
        Global.Progress.ResetProgress();
    }
}