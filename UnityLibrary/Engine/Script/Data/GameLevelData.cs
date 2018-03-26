using System;

public enum GameDifficult
{
    Normal, Hard, Expert, Master, Torment1, Torment2, Torment3, Torment4, Torment5, Torment6, Torment7, Torment8, Torment9, Torment10, Torment11, Torment12, Torment13, GreaterRift
}

public static class GameLevelData
{
    public static void ApplyMonster(Monster monster, GameDifficult difficult, int greaterRiftLevel)
    {
        float hpRatio = 1f;
        float damageRatio = 1f;
        switch (difficult)
        {
            //case GameDifficult.Normal:
            //    break;
            case GameDifficult.Hard:
                hpRatio = 2f;
                damageRatio = 1.3f;
                monster.ApplyLevel(2f, 1.3f);
                break;
            case GameDifficult.Expert:
                hpRatio = 3.2f;
                damageRatio = 1.89f;
                break;
            case GameDifficult.Master:
                hpRatio = 5.12f;
                damageRatio = 2.73f;
                break;
            case GameDifficult.Torment1:
                hpRatio = 8.19f;
                damageRatio = 3.96f;
                break;
            case GameDifficult.Torment2:
                hpRatio = 13.11f;
                damageRatio = 5.75f;
                break;
            case GameDifficult.Torment3:
                hpRatio = 20.97f;
                damageRatio = 8.33f;
                break;
            case GameDifficult.Torment4:
                hpRatio = 33.55f;
                damageRatio = 12.08f;
                break;
            case GameDifficult.Torment5:
                hpRatio = 53.96f;
                damageRatio = 17.52f;
                break;
            case GameDifficult.Torment6:
                hpRatio = 85.90f;
                damageRatio = 25.40f;
                break;
            case GameDifficult.Torment7:
                hpRatio = 189.85f;
                damageRatio = 36.04f;
                break;
            case GameDifficult.Torment8:
                hpRatio = 416.25f;
                damageRatio = 50.97f;
                break;
            case GameDifficult.Torment9:
                hpRatio = 912.60f;
                damageRatio = 72.08f;
                break;
            case GameDifficult.Torment10:
                hpRatio = 2000.82f;
                damageRatio = 101.94f;
                break;
            case GameDifficult.Torment11:
                hpRatio = 4387.00f;
                damageRatio = 144.16f;
                break;
            case GameDifficult.Torment12:
                hpRatio = 9618.00f;
                damageRatio = 203.87f;
                break;
            case GameDifficult.Torment13:
                hpRatio = 21090.00f;
                damageRatio = 288.31f;
                break;
            case GameDifficult.GreaterRift:
                for (int i = 0; i < greaterRiftLevel; ++i)
                {
                    hpRatio *= 1.17f;
                    damageRatio *= (float)Math.Pow(2, 0.1);
                }
                break;
            default:
                break;
        }
        monster.ApplyLevel(hpRatio, damageRatio);
    }
}
