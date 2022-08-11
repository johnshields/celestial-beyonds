public static class Bools
{
    public static bool muteActive, keyboardSelected, playstationSelected, xboxSelected;
    public static bool pbUpgraded, cdUpgraded, aUpgraded, isWebGL;
    public static bool cursorRequired;

    public static void LoadUpgrades()
    {
        if (PlayerMemory.cannonUpgrade == 1)
        {
            pbUpgraded = true;
            cdUpgraded = false;
        }
        
        if (PlayerMemory.cannonUpgrade == 2)
        {
            pbUpgraded = false;
            cdUpgraded = true;
        }

        if (PlayerMemory.armorUpgrade == 1)
        {
            aUpgraded = true;
        }
    }
    
    public static void IsWebGL()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
        isWebGL = true;
#else
        isWebGL = false;
#endif
    }
}
