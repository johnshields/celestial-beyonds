using System;
using UnityEngine;

namespace Main.Scripts.Combat
{
    public class CombatManager  : MonoBehaviour
    {
        public static int playerHealth = 30;
        public static int enemyHealth = 5;
        public int[] cheatHealth;
        public GameObject[] enemy;

        private void Awake()
        {
            cheatHealth[0] = 5;
            cheatHealth[1] = 5;
            playerHealth = 30;
            enemyHealth = 3;
        }

        private void Update()
        {
            // For testing.
            if (cheatHealth[0] == 0)
                playerHealth = 0;
            else if (cheatHealth[1] == 0)
                enemyHealth = 0;

            if (enemyHealth  <= 0)
            {
                enemyHealth = 5;
                print("Enemy Terminated!");
                Destroy(enemy[0]);
            }
            else if (playerHealth <= 0)
                print("Player Defeated!");
        }
    }
}