using UnityEngine;
using UnityEngine.UI;

namespace Main.Scripts.Combat
{
    public class CombatManager : MonoBehaviour
    {
        public static int enemyHealth = 5;
        public GameObject[] enemy;

        private void Awake()
        {
            enemyHealth = 3;
        }

        private void Update()
        {
            if (enemyHealth <= 0)
            {
                enemyHealth = 5;
                print("Enemy Terminated!");
                Destroy(enemy[0]);
            }
        }
    }
}