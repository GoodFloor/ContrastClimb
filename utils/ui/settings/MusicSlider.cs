using Godot;

namespace ContrastClimb.utils.ui.settings;

public partial class MusicSlider : HSlider
{
    private AudioStreamPlayer _musicPlayer;
    private const float MaxVolume = 32f;

    public override void _Ready()
    {
        base._Ready();

        ((GameManager)GetTree().CurrentScene).ConfigLoaded += OnConfigLoaded;
    }

    private void OnConfigLoaded()
    {
        Value = 100.0 - Global.Config.Music;
        
        SetVolume(Global.Config.Music);
    }

    private void SetVolume(float value)
    {
        // If _musicPlayer wasn't assigned yet - get its reference
        _musicPlayer ??= Global.GameManager.GetNode<AudioStreamPlayer>("MusicPlayer");

        if (value < 100f)
            _musicPlayer.VolumeDb = MaxVolume * value / -100f;
        else
            _musicPlayer.VolumeDb = -80f;
    }

    public override void _ValueChanged(double newValue)
    {
        base._ValueChanged(newValue);
        
        var convertedValue = 100f - (float)newValue;

        SetVolume(convertedValue);
        
        Global.Config.Music = convertedValue;
    }
}