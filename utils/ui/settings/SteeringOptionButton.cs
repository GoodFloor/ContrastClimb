using System;
using ContrastClimb.utils.enums;
using Godot;

namespace ContrastClimb.utils.ui.settings;

public partial class SteeringOptionButton : OptionButton
{

    public override void _Ready()
    {
        WaitAndLoadConfig();
    }

    private async void WaitAndLoadConfig()
    {
        try
        {
            await ToSignal(GetTree().CreateTimer(1f, true), SceneTreeTimer.SignalName.Timeout);
            Selected = (int)Global.Config.Steering;
        }
        catch (Exception e)
        {
            GD.PrintErr(e);
        }
    }
    
    private void OnItemSelected(int index)
    {
        Global.Config.Steering = (EMovementType)index;
        Global.GameManager.Player.ReloadMovementConfig();
    }
}