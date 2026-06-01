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
        ((GameManager)GetTree().CurrentScene).Connect(GameManager.SignalName.ColorChanged, new Callable(this, MethodName.OnColorChanged));
    }

    private void OnColorChanged()
    {
        _value--;
        _counter.Text = _value.ToString();
    }

    public void AddEnergy(int value = 1)
    {
        Value += value;
    }
}