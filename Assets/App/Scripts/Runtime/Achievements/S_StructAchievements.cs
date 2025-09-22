using System;

[Serializable]
public struct S_StructAchievements
{
    public int id;
    public bool unlocked;

    public S_StructAchievements(int id, bool unlocked)
    {
        this.id = id;
        this.unlocked = unlocked;
    }
}
