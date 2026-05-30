using ContrastClimb.utils;
using Godot;

namespace ContrastClimb.environment.platforms.spikes;

public partial class PlatformSprite : ParentPlatformSprite
{
    public override void HidePlatform()
    {
        Visible = false;
    }

    public override void RevealPlatform()
    {
        Visible = true;
    }

    public override void MakeLight()
    {
        GetNode<Sprite2D>("Sprite").Modulate = GColors.Light;
    }

    public override void MakeDark()
    {
        GetNode<Sprite2D>("Sprite").Modulate = GColors.Dark;
    }
}
