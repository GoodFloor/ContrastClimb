using Godot;

namespace ContrastClimb.utils;

public partial class MusicManager : Node
{
    private AudioStreamPlayer _player;

    public float Volume
    {
        get => _player.VolumeDb;
        set => _player.VolumeDb = value;
    }
    
    public override void _Ready()
    {
        base._Ready();

        Global.MusicManager = this;
        _player = GetNode<AudioStreamPlayer>("MusicPlayer");
        ((GameManager)GetTree().CurrentScene).ConfigLoaded += OnConfigLoaded;
    }
    
    private void OnConfigLoaded()
    {
        Volume = Global.Config.Music;
        
        // TODO: Load appropriate playback
    }
    
}