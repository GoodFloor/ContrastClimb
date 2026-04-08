using Godot;

public partial class ExitButton : Button
{
    public override void _Pressed()
    {
        base._Pressed();
        GetTree().Quit();
    }
}
