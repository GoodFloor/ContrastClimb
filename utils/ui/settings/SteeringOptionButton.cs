using System;
using ContrastClimb.utils.enums;
using Godot;

namespace ContrastClimb.utils.ui.settings;

public partial class SteeringOptionButton : OptionButton
{

    public override void _Ready()
    {
        ((GameManager)GetTree().CurrentScene).ConfigLoaded += OnConfigLoaded;
    }

    private void OnConfigLoaded()
    {
        Selected = (int)Global.Config.Steering;
    }
    
    private void OnItemSelected(int index)
    {
        Global.Config.Steering = (EMovementType)index;
        Global.GameManager.Player.ReloadMovementConfig();
    }
}