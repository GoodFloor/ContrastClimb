using Godot;

namespace ContrastClimb.utils.ui.main_menu;

public partial class ExitButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        GetTree().Quit();
    }
}