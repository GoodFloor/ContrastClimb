using Godot;
using ContrastClimb.utils.enums;

namespace ContrastClimb.utils;

public partial class Config : GodotObject
{
    private const string ConfigPath = "res://config.cfg";
    private ConfigFile _configFile = new ConfigFile();
    public EMovementType Steering
    {
        get => (EMovementType)(int)_configFile.GetValue("gameplay", "steering");
        set
        {
            _configFile.SetValue("gameplay", "steering", (int)value);
            _configFile.Save(ConfigPath);
        } 
    }
    
    /// <summary>
    /// Loads config from file and makes sure that all the expected options are present. 
    /// </summary>
    public void LoadConfig()
    {
        _configFile.Load(ConfigPath);
        
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
        
            // If the device is not mobile, i.e. it doesn't have accelerometer, then it defaults to dragging
            if (OS.GetName() != "Android" && OS.GetName() != "iOS")
            {
                _configFile.SetValue("gameplay", "steering", (int)EMovementType.Drag);
            }
                
        }
        
        
        _configFile.Save(ConfigPath);
    }

    public void ResetConfig()
    {
        _configFile.EraseSection("gameplay");
        GenerateDefaultConfig();
    }
}
