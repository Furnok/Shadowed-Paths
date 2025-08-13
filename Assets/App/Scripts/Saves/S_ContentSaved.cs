using System;
using System.Collections.Generic;

[Serializable]
public class S_ContentSaved
{
    public string dateSaved = "";
    public string currentLevel = "";

    public string playerName = "";

    public List<S_StructAchievements> listAchievements = new();
}
