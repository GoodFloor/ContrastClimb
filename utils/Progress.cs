using Godot;

namespace ContrastClimb.utils;

public partial class Progress : GodotObject
{
    public const int LevelCount = 10;
    
    private const string ProgressPath = "user://progress.cfg";
    private const string Password = "f31oijzvc";
    private const int CurrentVersion = 1;
    private ConfigFile _progressFile = new ConfigFile();

    public int LatestLevelId
    {
        get => (int)_progressFile.GetValue("info", "latestLevelId", 0);
        set
        {
            _progressFile.SetValue("info", "latestLevelId", value);
            _progressFile.SaveEncryptedPass(ProgressPath, Password);
        }
    }

    public void LoadProgress()
    {
        _progressFile.LoadEncryptedPass(ProgressPath, Password);
        // _progressFile.Load(ProgressPath);
        
        if ((int)_progressFile.GetValue("info", "version", 0) < CurrentVersion)
            GenerateFile();
        
    }

    private void GenerateFile()
    {
        _progressFile.SetValue("info", "version", CurrentVersion);
        _progressFile.SetValue("info", "latestLevelId", 0);

        _progressFile.SetValue("level_0", "state", "unlocked");
        _progressFile.SetValue("level_0", "score", 0);
        
        for (var i = 1; i < 10; i++)
        {
            _progressFile.SetValue($"level_{i}", "state", "locked");
            _progressFile.SetValue($"level_{i}", "score", 0);
        }
        
        _progressFile.SaveEncryptedPass(ProgressPath, Password);
        // _progressFile.Save(ProgressPath);
    }
    
    public bool IsLevelUnlocked(string levelName)
    {
        return _progressFile.GetValue(levelName, "state").ToString() == "unlocked";
    }

    public void UnlockLevel(int levelId)
    {
        _progressFile.SetValue($"level_{levelId}", "state", "unlocked");
        _progressFile.SaveEncryptedPass(ProgressPath, Password);
    }
    
    public int GetLevelScore(string levelName)
    {
        return (int)_progressFile.GetValue(levelName, "score");
    }
    
    public void SetLevelScore(string levelName, int score)
    {
        _progressFile.SetValue(levelName, "score", score);
        _progressFile.SaveEncryptedPass(ProgressPath, Password);
    }

    public void ResetProgress()
    {
        GenerateFile();
    }
}