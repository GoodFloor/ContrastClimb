namespace ContrastClimb.utils;

public enum ECollisionMask
{
    BlackDownwards = 1,
    WhiteDownwards = 2,
    BlackUpwards = 4,
    WhiteUpwards = 8,
    AlwaysSolid = 16,
    Black = BlackDownwards + BlackUpwards,
    White = WhiteDownwards + WhiteUpwards,
    Downwards = BlackDownwards + WhiteDownwards,
    Upwards = BlackUpwards + WhiteUpwards,
    BlackAndWhite = Black + White,
    BlackAndSolid = Black + AlwaysSolid,
    WhiteAndSolid = White + AlwaysSolid,
    DownwardsAndSolid = Downwards + AlwaysSolid,
    UpwardsAndSolid = Upwards + AlwaysSolid,
    BlackWhiteSolid = Black + White + AlwaysSolid,
}