using System.Collections;
using Main.Scripts.Captain;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMemory : MonoBehaviour
{
    public static int peridots, cannonUpgrade, armorUpgrade;
    public static string sceneToLoad;
    public bool playerInScene;
    private GameObject _player;
    private string _activeScene;

    private void Awake()
    {
        if(playerInScene)
            _player = GameObject.FindGameObjectWithTag("Player");
        _activeScene = SceneManager.GetActiveScene().name;
        sceneToLoad = PlayerPrefs.GetString("sceneToLoad");
        peridots = PlayerPrefs.GetInt("peridots");
        cannonUpgrade = PlayerPrefs.GetInt("cannonUpgrade");
        armorUpgrade = PlayerPrefs.GetInt("armorUpgrade");

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
    }

    public static void ResetMemory()
    {
        sceneToLoad = "002_Opening";
        peridots = 0;
        cannonUpgrade = 0;
        armorUpgrade = 0;
        print("Player Memory Reset.");
    }

    private void WhichScene()
    {
        switch (_activeScene)
        {
            case "003_CelestialWaltz":
                sceneToLoad = "003_CelestialWaltz";
                break;
            case "004_Intro_TRAPPIST-1":
                sceneToLoad = "004_Intro_TRAPPIST-1";
                break;
            case "004_TRAPPIST-1":
                sceneToLoad = "004_TRAPPIST-1";
                break;
            case "005_LunarPulse":
                sceneToLoad = "005_LunarPulse";
                break;
            case "006_Intro_PCB":
                sceneToLoad = "006_Intro_PCB";
                break;
            case "006_ProximaCentauriB":
                sceneToLoad = "006_ProximaCentauriB";
                break;
            case "007_Man_of_Celestial_Man_of_Faith":
                sceneToLoad = "007_Man_of_Celestial_Man_of_Faith";
                break;
            case "008_IntroKepler-186f":
                sceneToLoad = "008_IntroKepler-186f";
                break;
            case "008_Kepler-186f":
                sceneToLoad = "008_Kepler-186f";
                break;
            case "009_Intro_Endgame":
                sceneToLoad = "009_Intro_Endgame";
                break;
            case "009_Endgame":
                sceneToLoad = "009_Endgame";
                break;
            case "010_TheGoldenRecord":
                sceneToLoad = "010_TheGoldenRecord";
                break;
            case "011_Earth":
                sceneToLoad = "011_Earth";
                break;
        }

        print("sceneToLoad: " + sceneToLoad);
        PlayerPrefs.Save();
    }

    private void LoadUpgrade()
    {
        if (playerInScene)
        {
            if (cannonUpgrade == 1)
            {
                Bools.pbUpgraded = true;
                _player.GetComponent<CaptainAnimAndSound>().pbUpgrade = true;
            }

            if (cannonUpgrade == 2)
            {
                Bools.cdUpgraded = true;
                _player.GetComponent<CaptainAnimAndSound>().cdUpgrade = true;
            }

            if (armorUpgrade == 1)
            {
                Bools.aUpgraded = true;
                _player.GetComponent<CaptainAnimAndSound>().aUpgrade = true;
                StartCoroutine(AddHealth());
            }   
        }
    }

    private IEnumerator AddHealth()
    {
        yield return new WaitForSeconds(1.5f);
        _player.GetComponent<CaptainHealth>().maxHealth = 200;
        yield return new WaitForSeconds(1f);
        _player.GetComponent<CaptainHealth>().currentHealth = 200;
    }
}