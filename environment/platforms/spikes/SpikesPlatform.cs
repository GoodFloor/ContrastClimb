using System;
using ContrastClimb.utils;
using Godot;
using Godot.Collections;

namespace ContrastClimb.environment.platforms.spikes;

public partial class SpikesPlatform : ParentPlatform
{
    public override void _Ready()
    {
        base._Ready();
    }

    public override void PlayerLanded()
    {
        base.PlayerLanded();
        
        Global.GameManager.EndLevel(false);
    }
}