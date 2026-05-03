using System.Collections.Generic;
using Godot;

namespace ContrastClimb.utils.ui.level_selection;

public partial class LevelSelection : Control
{
    private GridContainer _levelGrid;
    private PackedScene _levelSelectorButton;
    private List<LevelSelectorButton> _levelSelectorButtons;

    public override void _Ready()
    {
        base._Ready();
        
        _levelGrid = GetNode<GridContainer>("LevelGrid");
        _levelSelectorButton = ResourceLoader.Load<PackedScene>("res://utils/ui/level_selection/level_selector_button.tscn");
        _levelSelectorButtons = [];
    }

    public void GenerateLevelsList()
    {
        for (var i = 0; i < Progress.LevelCount; i++)
        {
            var newButton = _levelSelectorButton.Instantiate<LevelSelectorButton>();
            _levelGrid.AddChild(newButton);
            
            newButton.LevelId = i;
            newButton.LevelScore = Global.Progress.GetLevelScore($"level_{i}");

            if (!Global.Progress.IsLevelUnlocked($"level_{i}"))
                newButton.Disabled = true;
            
            _levelSelectorButtons.Add(newButton);
        }
    }

    public void UnlockLevel(int levelId)
    {
        _levelSelectorButtons[levelId].Disabled = false;
    }

    public void ChangeScore(int levelId, int newScore)
    {
        _levelSelectorButtons[levelId].LevelScore = newScore;
    }
}