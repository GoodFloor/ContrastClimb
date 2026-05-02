using Godot;

namespace ContrastClimb.utils.ui.level_selection;

public partial class LevelSelectorButton : Button
{
    [Export]
    public string LevelName;

    public override void _Pressed()
    {
        base._Pressed();
        
        Global.GameManager.PlayLevel(LevelName);
    }
}