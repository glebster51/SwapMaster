public static class GlobalSettings
{
    public static LevelSettings levelSettings;
    public static int money;
    public static int startHealth;

    static GlobalSettings()
    {
        LoadProfile();
    }

    private static void LoadProfile()
    {
        //Load Save Here!
        money = 0;
        startHealth = 3;
    }
}
