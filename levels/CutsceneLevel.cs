using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.levels;

public partial class CutsceneLevel : ParentLevel
{
    public override void _Ready()
    {
        GetNode<Button>("Button").Pressed += () =>
        {
            Global.GameManager.EndLevel(true);
        };
    }
}