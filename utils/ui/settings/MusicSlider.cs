using Godot;

namespace ContrastClimb.utils.ui.settings;

public partial class MusicSlider : HSlider
{
    private AudioStreamPlayer2D _musicPlayer;
    private const float MaxVolume = 32f;

    public override void _Ready()
    {
        base._Ready();

        ((GameManager)GetTree().CurrentScene).ConfigLoaded += OnConfigLoaded;
    }

    private void OnConfigLoaded()
    {
        Value = 100.0 - Global.Config.Music;
        
        // If _musicPlayer wasn't assigned yet - get its reference
        _musicPlayer ??= Global.GameManager.GetNode<AudioStreamPlayer2D>("MusicPlayer");

        if (Global.Config.Music < 100f)
        {
            _musicPlayer.VolumeDb = MaxVolume * Global.Config.Music / -100f;
        }
        else
        {
            _musicPlayer.VolumeDb = -80f;
        }
    }

    public override void _ValueChanged(double newValue)
    {
        base._ValueChanged(newValue);
        
        var convertedValue = 100f - (float)newValue;

        // If _musicPlayer wasn't assigned yet - get its reference
        _musicPlayer ??= Global.GameManager.GetNode<AudioStreamPlayer2D>("MusicPlayer");

        if (convertedValue < 100f)
        {
            _musicPlayer.VolumeDb = MaxVolume * convertedValue / -100f;
        }
        else
        {
            _musicPlayer.VolumeDb = -80f;
        }
        Global.Config.Music = convertedValue;
    }
}