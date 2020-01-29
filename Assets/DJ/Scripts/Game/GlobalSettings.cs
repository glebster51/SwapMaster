public static class GlobalSettings
{
    public static LevelSettings levelSettings;
    public static int money;
    public static int startHealth;
    public static int lastLevel;
    public static int lastLevelScore;

    static GlobalSettings()
    {
        LoadProfile();
    }

    private static void LoadProfile()
    {
        lastLevelScore = 0;
        lastLevel = -1;
        money = 0;
        startHealth = 3;
    }
}
