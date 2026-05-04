using Godot;
using System;

namespace ContrastClimb.environment.platforms;

public partial class ParentPlatformSprite : Node2D
{
    public virtual void HidePlatform()
    {
        throw new NotImplementedException();
    }

    public virtual void RevealPlatform()
    {
        throw new NotImplementedException();
    }

    public virtual void MakeLight()
    {
        throw new NotImplementedException();
    }

    public virtual void MakeDark()
    {
        throw new NotImplementedException();
    }
}
