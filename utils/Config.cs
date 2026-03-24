using Godot;
using System;

public partial class Config : GodotObject
{
    private string _configPath = "res://config.cfg";
    private ConfigFile _configFile = new ConfigFile();
    public string Steering;
    
    public void LoadConfig()
    {
        GD.Print("LoadConfig");
        if (_configFile.Load(_configPath) != Error.Ok)
            GenerateDefaultConfig();

        Steering = (string)_configFile.GetValue("gameplay", "steering");
    }


    private void GenerateDefaultConfig()
    {
        // Default steering on mobile device is with tilting
        _configFile.SetValue("gameplay", "steering", "tilt");
        
        // If the device doesn't have accelerometer, then it defaults to dragging
        if (Input.GetGravity() == Vector3.Zero)
            _configFile.SetValue("gameplay", "steering", "drag");
        
        _configFile.Save(_configPath);
    }
}
