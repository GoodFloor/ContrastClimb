using Godot;

namespace ContrastClimb.utils.ui.settings;

public partial class MusicSlider : HSlider
{
    private const float MaxVolume = 16f;
    private const float MinVolume = -16f;

    public override void _Ready()
    {
        base._Ready();

        ((GameManager)GetTree().CurrentScene).ConfigLoaded += OnConfigLoaded;
    }

    private void OnConfigLoaded()
    {
        Value = 100.0 - Global.Config.Music;
    }

    public override void _ValueChanged(double newValue)
    {
        base._ValueChanged(newValue);
        
        const float span = MaxVolume - MinVolume;
        var convertedValue = (float)newValue * span / 100f + MinVolume;

        if (newValue > 0f)
            Global.MusicManager.Volume = convertedValue;
        else
            Global.MusicManager.Volume = -80f;
        
        Global.Config.Music = convertedValue;
    }
}