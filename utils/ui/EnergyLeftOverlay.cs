using System;
using Godot;

namespace ContrastClimb.utils.ui;

public partial class EnergyLeftOverlay : CanvasLayer
{
    private Label _counter;
    private int _value;

    public int Value
    {
        get => Convert.ToInt32(_counter.Text);
        set
        {
            _counter.Text = value.ToString(); 
            _value = value;
        }
    }

    public override void _Ready()
    {
        base._Ready();

        _counter = GetNode<Label>("Display/EnergyLeftLabel");
    }

    public override void _Input(InputEvent @event)
    {
        base._Input(@event);
        
        if (@event.IsActionPressed("switch_color") && _value > 0)
        {
            _value--;
            _counter.Text = _value.ToString();
        }
    }
}