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
        void OnSceneLoaded(Scene Current,LoadSceneMode mode)
        {
            if(Current.name != "Intro")
            {
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.SpeedMod>().enabled = false;
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.SandMode>().enabled = false;
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.VampHook>().enabled = false;
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.SpeedMod>().enabled = false;
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.TestModifier>().enabled = false;
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.FrictionMod>().enabled = false;
                MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<MiscUI>().enabled = false;
            }
        }
        public void Update()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            if(SceneManager.GetActiveScene().name != "Intro")
            {
                Invoke("findEnemies", .2f);
                if (Input.GetKeyDown(KeyCode.K)) 
                {
                    MonoSingleton<NewMovement>.Instance.gameObject.GetComponent<MiscUI>().Active 
                        = !MonoSingleton<NewMovement>.Instance.gameObject.GetComponent<MiscUI>().Active;
                } 

                if (MonoSingleton<Modifiers.SpeedMod>.Instance.isActiveAndEnabled == false)
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
