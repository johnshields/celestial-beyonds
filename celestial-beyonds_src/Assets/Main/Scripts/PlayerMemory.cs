using System.Collections;
using Main.Scripts.Captain;
using Main.Scripts.Enemies.Aristaues;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMemory : MonoBehaviour
{
    public static int peridots, cannonUpgrade, armorUpgrade;
    public static string sceneToLoad;
    public bool playerInScene, armourUpgradeInScene, resetMemory;
    private static bool completed;
    private bool _added;
    private GameObject _player;
    private string _activeScene;

    private void Awake()
    {
        _activeScene = SceneManager.GetActiveScene().name;
        sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
        peridots = PlayerPrefs.GetInt("peridots");
        cannonUpgrade = PlayerPrefs.GetInt("cannonUpgrade");
        armorUpgrade = PlayerPrefs.GetInt("armorUpgrade");

        if (playerInScene)
            _player = GameObject.FindGameObjectWithTag("Player");

        completed = sceneToLoad == "011_Earth";

        print("Current scene:" + _activeScene);

        WhichScene();
        LoadUpgrade();
    }


    private void Update()
    {
        PlayerPrefs.SetString("sceneToLoad", sceneToLoad);
        PlayerPrefs.SetInt("peridots", peridots);
        PlayerPrefs.SetInt("cannonUpgrade", cannonUpgrade);
        PlayerPrefs.SetInt("armorUpgrade", armorUpgrade);

        if (resetMemory)
            ResetMemory();
        
        PlayerPrefs.Save();
    }

    public static void ResetMemory()
    {
        completed = false;
        sceneToLoad = string.Empty;
        peridots = 0;
        cannonUpgrade = 0;
        armorUpgrade = 0;
        Lemons.cheatActivated = false;
        Rambo.cheatActivated = false;
        print("Player Memory Reset.");
    }

    private void WhichScene()
    {
        if (!completed)
        {
            sceneToLoad = _activeScene switch
            {
                "003_CelestialWaltz" => "003_CelestialWaltz",
                "004_Intro_TRAPPIST-1" => "004_Intro_TRAPPIST-1",
                "004_TRAPPIST-1" => "004_TRAPPIST-1",
                "005_LunarPulse" => "005_LunarPulse",
                "006_Intro_PCB" => "006_Intro_PCB",
                "006_ProximaCentauriB" => "006_ProximaCentauriB",
                "007_Man_of_Celestial_Man_of_Faith" => "007_Man_of_Celestial_Man_of_Faith",
                "008_IntroKepler-186f" => "008_IntroKepler-186f",
                "008_Kepler-186f" => "008_Kepler-186f",
                "009_Intro_Endgame" => "009_Intro_Endgame",
                "009_Endgame" => "009_Endgame",
                "010_TheGoldenRecord" => "010_TheGoldenRecord",
                "011_Earth" => "011_Earth",
                _ => sceneToLoad
            };
        }
        else
            sceneToLoad = "011_Earth";

        print("sceneToLoad: " + sceneToLoad);
    }

    private void LoadUpgrade()
    {
        if (playerInScene)
        {
            if (cannonUpgrade == 1)
            {
                Bools.pbUpgraded = true;
                _player.GetComponent<CaptainAnimAndSound>().pbUpgrade = true;
                Bools.cdUpgraded = false;
                _player.GetComponent<CaptainAnimAndSound>().cdUpgrade = false;
            }
            else if (cannonUpgrade == 2)
            {
                Bools.pbUpgraded = false;
                _player.GetComponent<CaptainAnimAndSound>().pbUpgrade = false;
                Bools.cdUpgraded = true;
                _player.GetComponent<CaptainAnimAndSound>().cdUpgrade = true;
            }

            if (armorUpgrade == 1 && armourUpgradeInScene)
            {
                Bools.aUpgraded = true;
                _player.GetComponent<CaptainAnimAndSound>().aUpgrade = true;
                if (!_added)
                    StartCoroutine(AddHealth());
            }
        }
    }

    private IEnumerator AddHealth()
    {
        _added = true;
        yield return new WaitForSeconds(1f);
        _player.GetComponent<CaptainHealth>().maxHealth = 200;
        yield return new WaitForSeconds(.5f);
        _player.GetComponent<CaptainHealth>().currentHealth = 200;
    }
}