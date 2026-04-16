using Godot;
using System;
using ContrastClimb.utils;

public partial class OneWayPlatformSprite : PlatformSprite
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
        GetNode<ColorRect>("ColorRect").Color = GColors.Light;
    }

    public override void MakeDark()
    {
        GetNode<ColorRect>("ColorRect").Color = GColors.Dark;
    }
}
