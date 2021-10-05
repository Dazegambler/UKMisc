using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UKMiscRevamp.Modifiers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKMiscRevamp
{
    class MiscUI : MonoBehaviour
    {
        static AssetBundle UIBundle = LoadAssetBundle(Properties.Resource.ukmisc);
        GUISkin skin = UIBundle.LoadAsset<GUISkin>("UIMisc");

        Rect
            SpeedWindow = new Rect(Screen.width / 1.275f,Screen.width/1920, 400,100),
            window = new Rect(60/Screen.width,400/Screen.height,200,210);

        void Awake()
        {
        }

        public void OnGUI()
        {
            GUI.skin = skin;
            if(SceneManager.GetActiveScene().name != "Intro"
                && SceneManager.GetActiveScene().name != "Main Menu")
            {
                GUI.Window(875,window, UI,"UKMisc 3.0.0");

                if (MonoSingleton<SandMode>.Instance.isActiveAndEnabled == true)
                {
                    Text(35, "Sand Mode ON", new Rect(8/Screen.width,10, 1000,1000),"white");
                }

                if(MonoSingleton<TestModifier>.Instance.isActiveAndEnabled == true)
                {
                    Text(35, "Test Mode ON", new Rect(8/Screen.width,130, 1000, 1000), "white");
                }

                if (MonoSingleton<VampHook>.Instance.isActiveAndEnabled == true)
                {
                    if (MonoSingleton<AssistController>.Instance.cheatsEnabled != true
                        && MonoSingleton<AssistController>.Instance.majorEnabled != true)
                    {
                        Invoke("Warning",.15f);
                    }
                    else
                    {
                        Text(35, "Vampire Mode ON", new Rect(8/Screen.width,160, 1000, 1000), "white");
                    }
                }

                if (MonoSingleton<SpeedMod>.Instance.isActiveAndEnabled == true)
                {
                    if (MonoSingleton<AssistController>.Instance.cheatsEnabled != true
                        && MonoSingleton<AssistController>.Instance.majorEnabled != true)
                    {
                        Invoke("Warning",.15f);
                        MonoSingleton<SpeedMod>.Instance.enabled = false;
                    }
                    else
                    {
                        GUI.Window(1337,SpeedWindow, UI, "");
                    }
                }

                if (MonoSingleton<FrictionMod>.Instance.isActiveAndEnabled == true)
                {
                    Text(35, "Frictionless Mode ON", new Rect(8, 70/Screen.height, 1000, 1000), "white");
                }
            }
        }
        public void UI(int WindowID)
        {
            switch (WindowID)
            {
                case 875:
                    //if (GUI.Button(new Rect(5, 40+(30*0), window.width - 10, 30), "")) MonoSingleton<T>.Instance.enabled = !MonoSingleton<T>.Instance.enabled;
                    if (GUI.Button(new Rect(5, 20, window.width - 10, 30), "Frictionless")) MonoSingleton<FrictionMod>.Instance.enabled = !MonoSingleton<FrictionMod>.Instance.enabled;
                    if (GUI.Button(new Rect(5, 20+(35*1), window.width - 10, 30), "SAND mode")) MonoSingleton<SandMode>.Instance.enabled = !MonoSingleton<SandMode>.Instance.enabled;
                    if (GUI.Button(new Rect(5, 20+(35*2), window.width - 10, 30), "SPEED mode")) MonoSingleton<SpeedMod>.Instance.enabled = !MonoSingleton<SpeedMod>.Instance.enabled;
                    if (GUI.Button(new Rect(5, 20+(35*3), window.width - 10, 30), "Vampire Hook")) MonoSingleton<VampHook>.Instance.enabled = !MonoSingleton<VampHook>.Instance.enabled;
                    if (GUI.Button(new Rect(5, 20+(35*4), window.width - 10, 30), "Test Mode")) MonoSingleton<TestModifier>.Instance.enabled = !MonoSingleton<TestModifier>.Instance.enabled;
                    break;
                case 1337:
                    SpeedMenu(SpeedWindow);
                    break;
            }
        }
        float
            _spd = 750,
            _jump = 90;
        void SpeedMenu(Rect pos)
        {
            _spd = GUI.HorizontalSlider(new Rect(5, 40, pos.width - 120, 25), _spd, -100, 100);
            _jump = GUI.HorizontalSlider(new Rect(5, 75, pos.width - 120, 25), _jump, -100, 100);
            GUI.Label(new Rect(0, -10, 400, 50), $"<size=25><i><color=yellow>SPEED</color></i>:{MonoSingleton<SpeedMod>.Instance.spd}/{MonoSingleton<SpeedMod>.Instance.jump}</size>");
            if(GUI.Button(new Rect(pos.width / 2 + 125, 0, 75, 25), "Back"))
            {
                MonoSingleton<SpeedMod>.Instance.enabled = false;
            }
            if(GUI.Button(new Rect(pos.width / 2 + 40, 0, 75, 25), "Reset"))
            {
                MonoSingleton<SpeedMod>.Instance.spd = 750;
                MonoSingleton<SpeedMod>.Instance.jump = 90;
            }
            if(GUI.Button(new Rect(pos.width/2+125, 35, 100, 25), $"<size=13>Speed+- {(int)_spd}</size>"))
            {
                MonoSingleton<SpeedMod>.Instance.spd += (int)_spd;
            }
            if(GUI.Button(new Rect(pos.width/2+125, 70, 100, 25), $"<size=13>Jump+- {(int)_jump}</size>"))
            {
                MonoSingleton<SpeedMod>.Instance.jump += (int)_jump;
            }
        }
        private void Text(int fntsize, string txt, Rect Pos, string col)
        {
            GUI.Label(new Rect(Pos.x + 1f, Pos.y - 1f, Pos.width, Pos.height), $"<size={fntsize}><color=black>{txt}</color></size>");
            GUI.Label(Pos, $"<size={fntsize}><color={col}>{txt}</color></size>");
        }
        private void Warning()
        {
            Rect warning = new Rect(Screen.width / 4, Screen.height / 2, 1000,0);
            warning.height = Mathf.MoveTowards(warning.height,100,1f);
            Text(40, "<b>MUST HAVE MAJOR ASSISTS/CHEATS ACTIVATED</b>",warning, "red");
            warning.height = Mathf.MoveTowards(warning.height,0,1f);
        }
        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
