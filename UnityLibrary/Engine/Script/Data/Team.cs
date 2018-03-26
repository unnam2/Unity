[System.Flags]
public enum Team
{
    None = 0,
    Friendly = 1 << 0,
    Enemy = 1 << 1,
    Natural = 1 << 2
}

public static class TeamManager
{
    public static bool Include(this Team my, Team other)
    {
        if ((my & other) == 0) //하나도 겹치는게 없으면
            return false;
        return true;
    }
}
