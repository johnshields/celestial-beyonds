using System.Collections;
using TMPro;
using UnityEngine;

public class MiniMenu : MonoBehaviour
{
    public int enemiesNum,
        plantsNum,
        artifactsNum,
        totalEnemiesTrap,
        totalEnemiesPCB,
        totalEnemiesKep,
        totalPlants,
        totalArtifacts,
        totalPeridotsTrap,
        totalPeridotsPCB,
        totalPeridotsKep;

    public TextMeshProUGUI plantNumTxt, enemyNumTxt, artifactsNumTxt, peridotsNumTxt;
    public GameObject allEnemiesPanel, allArtifactsPanel, allPeridotsPanel, upgradeOption, upgradePanel;
    public AudioClip[] achievementSFX;
    public bool trappist, pcb, kepler;
    private AudioSource _audio;
    private bool _allEnemies, allArtifacts, _allPeridots;

    private void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    private void OnGUI()
    {
        if (plantsNum != 0 || enemiesNum != 0 || artifactsNum != 0 || artifactsNum != 0 || Peridots.peridotsCollectedInLvl != 0)
        {
            plantNumTxt.text = plantsNum + " / " + totalPlants;
            artifactsNumTxt.text = artifactsNum + " / " + totalArtifacts;

            if (trappist)
            {
                enemyNumTxt.text = enemiesNum + " / " + totalEnemiesTrap;
                peridotsNumTxt.text = Peridots.peridotsCollectedInLvl + " / " + totalPeridotsTrap;   
            }
            
            if (pcb)
            {
                enemyNumTxt.text = enemiesNum + " / " + totalEnemiesPCB;
                peridotsNumTxt.text = Peridots.peridotsCollectedInLvl + " / " + totalPeridotsPCB;   
            }
            
            if (trappist)
            {
                enemyNumTxt.text = enemiesNum + " / " + totalEnemiesKep;
                peridotsNumTxt.text = Peridots.peridotsCollectedInLvl + " / " + totalPeridotsKep;   
            }
        }
    }

    private void Update()
    {
        if (enemiesNum == 10 && !_allEnemies && trappist)
        {
            _allEnemies = true;
            print("Upgrade unlocked!");
            StartCoroutine(CloseActivePanel(0));
        }
        if (enemiesNum == 12 && !_allEnemies && pcb)
        {
            _allEnemies = true;
            print("Upgrade unlocked!");
            StartCoroutine(CloseActivePanel(0));
        }
        if (enemiesNum == 14 && !_allEnemies && kepler)
        {
            _allEnemies = true;
            print("Upgrade unlocked!");
            StartCoroutine(CloseActivePanel(0));
        }
        if (artifactsNum == 10 && !allArtifacts)
        {
            allArtifacts = true;
            StartCoroutine(CloseActivePanel(1));
        }
        if (Peridots.peridotsCollectedInLvl == 67 && !_allPeridots && trappist)
        {
            _allPeridots = true;
            StartCoroutine(CloseActivePanel(2));
        }
        if (Peridots.peridotsCollectedInLvl == 80 && !_allPeridots && pcb)
        {
            _allPeridots = true;
            StartCoroutine(CloseActivePanel(2));
        }
        if (Peridots.peridotsCollectedInLvl == 100 && !_allPeridots && kepler)
        {
            _allPeridots = true;
            StartCoroutine(CloseActivePanel(2));
        }
    }

    private void UpgradePanel()
    {
        if (PlayerMemory.cannonUpgrade != 1 || PlayerMemory.cannonUpgrade != 2 || PlayerMemory.armorUpgrade != 1)
        {
            upgradePanel.SetActive(true);
            upgradeOption.SetActive(true);
            _audio.PlayOneShot(achievementSFX[3]);
        }
    }

    private IEnumerator CloseActivePanel(int whichPanel)
    {
        yield return new WaitForSeconds(3);
        switch (whichPanel)
        {
            case 0:
                allEnemiesPanel.SetActive(true);
                _audio.PlayOneShot(achievementSFX[0]);
                break;
            case 1:
                allArtifactsPanel.SetActive(true);
                _audio.PlayOneShot(achievementSFX[1]);
                break;
            case 2:
                allPeridotsPanel.SetActive(true);
                _audio.PlayOneShot(achievementSFX[2]);
                break;
        }

        yield return new WaitForSeconds(3);
        switch (whichPanel)
        {
            case 0:
                allEnemiesPanel.SetActive(false);
                yield return new WaitForSeconds(3);
                UpgradePanel();
                yield return new WaitForSeconds(6);
                upgradePanel.SetActive(false);
                break;
            case 1:
                allArtifactsPanel.SetActive(false);
                break;
            case 2:
                allPeridotsPanel.SetActive(false);
                break;
        }
    }
}