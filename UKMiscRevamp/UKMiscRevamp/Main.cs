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
        Modifiers.SpeedMod
            spd;

        MiscUI
            UI;
        public void Start()
        {
        }
        void OnSceneLoaded(Scene Current,LoadSceneMode mode)
        {
            if(Current.name != "Intro")
            {
                //if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<T>(out var a) == false) MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<T>().enabled = false;
                if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<MiscUI>(out var g) == false) 
                {
                    UI = MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<MiscUI>();
                }
                if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<Modifiers.TestModifier>(out var f) == false) MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.TestModifier>();
                if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<Modifiers.FrictionMod>(out var e) == false) MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.FrictionMod>();
                if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<Modifiers.VampHook>(out var d) == false) MonoSingleton<NewMovement>.Instance.gameObject.AddComponent <Modifiers.VampHook>();
                if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent <Modifiers.SandMode>(out var c) == false) MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.SandMode>();
                if (MonoSingleton<NewMovement>.Instance.gameObject.TryGetComponent<Modifiers.SpeedMod>(out var b) == false)
                {
                    spd = MonoSingleton<NewMovement>.Instance.gameObject.AddComponent<Modifiers.SpeedMod>();
                }
            }
        }
        public void Update()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
            if(SceneManager.GetActiveScene().name != "Intro")
            {
                Invoke("findEnemies", .2f);
                if (Input.GetKeyDown(KeyCode.T)) 
                {
                    UI.enabled = !UI.enabled;
                } 

                if (spd.Active == false)
                {
                    MonoSingleton<NewMovement>.Instance.walkSpeed = 750f;
                    MonoSingleton<NewMovement>.Instance.jumpPower = 90;
                    MonoSingleton<NewMovement>.Instance.wallJumpPower = 150;
                }
            }
        }
    }
}
