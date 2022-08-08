public static class Bools
{
    public static bool muteActive, keyboardSelected, playstationSelected, xboxSelected;
    public static bool pbUpgraded, cdUpgraded, aUpgraded;

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
}
