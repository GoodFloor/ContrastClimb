using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.cutscenes;

public partial class Cutscene : Control
{
    private VideoStreamPlayer _player;
    private Button _skipButton;
    public override void _Ready()
    {
        base._Ready();
        
        _player = GetNode<VideoStreamPlayer>("Player");
        _skipButton = GetNode<Button>("SkipButton");
        
        _player.Finished += Global.GameManager.EndCutscene;
        _skipButton.Pressed += Global.GameManager.EndCutscene;
    }

    public void SetSource(string path)
    {
        var videoStream = new VideoStreamTheora();
        videoStream.File = path;

        _player.Stream = videoStream;
        _player.Play();
    }
}