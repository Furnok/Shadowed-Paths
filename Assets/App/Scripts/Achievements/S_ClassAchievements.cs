using System;
using UnityEngine;
using UnityEngine.Localization;

[Serializable]
public class S_ClassAchievements
{
    public int id = 0;
    public Sprite image = null;
    public LocalizedString title = new LocalizedString();
    public LocalizedString description = new LocalizedString();
    public int progress = 0;
    public int objective = 0;
    public bool unlocked = false;

    public S_ClassAchievements Clone()
    {
        return new S_ClassAchievements
        {
            id = this.id,
            image = this.image,
            title = this.title,
            description = this.description,
            progress = this.progress,
            objective = this.objective,
            unlocked = this.unlocked
        };
    }

    public S_StructAchievements ToSaveData()
    {
        return new S_StructAchievements(id, progress, unlocked);
    }
}