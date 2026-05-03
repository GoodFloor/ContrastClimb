using Godot;

namespace ContrastClimb.utils;

public partial class Global : Node
{
    public static GameManager GameManager { get; set; }
    public static Config Config { get; set; }
    public static Progress Progress { get; set; }
}
