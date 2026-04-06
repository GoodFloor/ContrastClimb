using Godot;
using System;
using ContrastClimb.utils;

public partial class Config : GodotObject
{
    private string _configPath = "res://config.cfg";
    private ConfigFile _configFile = new ConfigFile();
    public EMovementType Steering
    {
        get => (EMovementType)(int)_configFile.GetValue("gameplay", "steering");
        set
        {
            _configFile.SetValue("gameplay", "steering", (int)value);
            _configFile.Save(_configPath);
        } 
    }
    
    /// <summary>
    /// Loads config from file and makes sure that all the expected options are present. 
    /// </summary>
    public void LoadConfig()
    {
        _configFile.Load(_configPath);
        
        GenerateDefaultConfig();
    }
    
    /// <summary>
    /// Checks if all the expected config options are present, if a definition is not present, then it sets a default value for it.
    /// </summary>
    private void GenerateDefaultConfig()
    {
        if (!_configFile.HasSectionKey("gameplay", "steering"))
        {
            // Default steering on mobile device is with tilting
            _configFile.SetValue("gameplay", "steering", (int)EMovementType.Tilt);
        
            // If the device doesn't have accelerometer, then it defaults to dragging
            if (Input.GetGravity() == Vector3.Zero)
                _configFile.SetValue("gameplay", "steering", (int)EMovementType.Drag);
        }
        
        
        _configFile.Save(_configPath);
    }

    public void ResetConfig()
    {
        _configFile.EraseSection("gameplay");
        GenerateDefaultConfig();
    }
}
