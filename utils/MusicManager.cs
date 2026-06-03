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
    }

    public void Play(int id)
    {
        if (id > 5)
            id = 5;
        id++;

        var newStream = GD.Load<AudioStreamMP3>($"res://music/The_Abyss_0{id}.mp3");

        _player.Stream = newStream;
        _player.Play();
    }
    
}