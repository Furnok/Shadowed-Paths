using System;

[Serializable]
public struct S_StructAchievements
{
    public int id;
    public int progress;
    public bool unlocked;

    public S_StructAchievements(int id, int progress, bool unlocked)
    {
        this.id = id;
        this.progress = progress;
        this.unlocked = unlocked;
    }
}
