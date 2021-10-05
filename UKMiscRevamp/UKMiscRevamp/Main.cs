using BepInEx;
using BepInEx.Configuration;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKMiscRevamp
{
    [BepInPlugin("UK.MISC", "UKMisc", "3.0.0")]
    public class Plugin : BaseUnityPlugin
    {
         public EnemyIdentifier[] Enemies;
        public void Start()
        {
        }
        public void Update()
        {

            if(SceneManager.GetActiveScene().name != "Intro")
            {
                Invoke("findEnemies", .2f);
                if (Input.GetKeyDown(KeyCode.K)) 
                {
                    MonoSingleton<NewMovement>.Instance.gameObject.GetComponent<MiscUI>().Active 
                        = !MonoSingleton<NewMovement>.Instance.gameObject.GetComponent<MiscUI>().Active;
                } 

                if (MonoSingleton<NewMovement>.Instance.GetComponent<SpeedMod>().isActiveAndEnabled == false)
                {
                    MonoSingleton<NewMovement>.Instance.walkSpeed = 750f;
                    MonoSingleton<NewMovement>.Instance.jumpPower = 90;
                    MonoSingleton<NewMovement>.Instance.wallJumpPower = 150;
                }
            }
        }
        void findEnemies()
        {
            Enemies = FindObjectsOfType<EnemyIdentifier>();
        }
    }
}
