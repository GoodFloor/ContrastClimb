using System;
using Godot;

namespace ContrastClimb.utils.ui;

public partial class EnergyLeftOverlay : CanvasLayer
{
    private Label _counter;
    private int _value;
    private Control _dangerIndicator;

    public int Value
    {
        get => _value;
        set
        {
            _counter.Text = value.ToString(); 
            _value = value;
            _dangerIndicator.Visible = value == 0;
        }
    }

    public override void _Ready()
    {
        base._Ready();

        _counter = GetNode<Label>("Display/EnergyLeftLabel");
        _dangerIndicator = GetNode<Control>("DangerIndicator");
        ((GameManager)GetTree().CurrentScene).Connect(GameManager.SignalName.ColorChanged, new Callable(this, MethodName.OnColorChanged));
        
        _dangerIndicator.Visible = false;
    }

    private void OnColorChanged()
    {
        Value--;
    }

    public void AddEnergy(int value = 1)
    {
        Value += value;
    }
}