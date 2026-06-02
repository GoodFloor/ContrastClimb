using Godot;

namespace ContrastClimb.utils.ui.settings;

public partial class MusicSlider : HSlider
{
    private const float MaxVolume = 32f;

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
        
        var convertedValue = 100f - (float)newValue;

        if (newValue > 0f)
            Global.MusicManager.Volume = MaxVolume * convertedValue / -100f;
        else
            Global.MusicManager.Volume = -80f;
        
        Global.Config.Music = convertedValue;
    }
}