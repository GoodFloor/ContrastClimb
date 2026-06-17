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
        _dangerIndicator = GetNode<Control>("DangerIndicator");
        ((GameManager)GetTree().CurrentScene).Connect(GameManager.SignalName.ColorChanged, new Callable(this, MethodName.OnColorChanged));
        
        _dangerIndicator.Visible = false;
    }

    private void OnColorChanged()
    {
        _value--;
        _counter.Text = _value.ToString();

        if (_value == 0)
        {
            _dangerIndicator.Visible = true;
        }
    }

    public void AddEnergy(int value = 1)
    {
        Value += value;
        
        if (_value > 0)
        {
            _dangerIndicator.Visible = false;
        }
    }
}