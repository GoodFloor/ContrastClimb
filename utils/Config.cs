using Godot;
using System;

public partial class Config : GodotObject
{
    private string _configPath = "res://config.cfg";
    private ConfigFile _configFile = new ConfigFile();
    
    public void LoadConfig()
    {
        GD.Print("LoadConfig");
        if (_configFile.Load(_configPath) != Error.Ok)
            GenerateDefaultConfig();
    }


    private void GenerateDefaultConfig()
    {
        _configFile.SetValue("gameplay", "steering", "drag");
        _configFile.Save(_configPath);
    }
}
