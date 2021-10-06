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

        VampHook
            vamp;
        SpeedMod
            speed;
        SandMode
            sand;
        FrictionMod
            friction;
        TestModifier
            test;

        string
            vamptxt,
            sandtxt,
            frictiontxt,
            testtxt;

        AssistController
            Ac;

        void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            vamp = VampHook.Instance;
            speed = SpeedMod.Instance;
            sand = SandMode.Instance;
            friction = FrictionMod.Instance;
            test = TestModifier.Instance;
            Ac = AssistController.Instance;
        }

        void Update()
        {
        }

        public void OnGUI()
        {
            GUI.skin = skin;
            if(SceneManager.GetActiveScene().name != "Intro"
                && SceneManager.GetActiveScene().name != "Main Menu")
            {
                GUI.Window(875,window, UI,"UKMisc 3.0.0");

                if (sand.Active == true)
                {
                    sandtxt = "<color=lime>[</color><color=yellow>SAND</color> mode<color=lime>]</color>";
                    //Text(35, "<size=10>Sand Mode ON</size>", new Rect(8/Screen.width,0, 1000,1000),"white");
                }
                else
                {
                    sandtxt = "<color=red>[</color><color=yellow>SAND</color> mode<color=red>]</color>";

                }

                if (test.Active == true)
                {
                    testtxt = "<color=lime>[</color>Test Mode<color=lime>]</color>";
                    //Text(35, "<size=10>Test Mode ON</size>", new Rect(8/Screen.width,30, 1000, 1000), "white");
                }
                else
                {
                    testtxt = "<color=red>[</color>Test Mode<color=red>]</color>";
                }

                if (vamp.Active == true)
                {
                    if (Ac.cheatsEnabled != true && Ac.majorEnabled != true)
                    {
                        Invoke("Warning",.15f);
                        vamp.Active = false;
                    }
                    else
                    {
                        vamptxt = "<color=lime>[</color><color=maroon>Vampire</color> Hook<color=lime>]</color>";
                        //Text(35, "<>Vampire Mode ON", new Rect(8/Screen.width,60, 1000, 1000), "white");
                    }
                }
                else
                {
                    vamptxt = "<color=red>[</color><color=maroon>Vampire</color> Hook<color=red>]</color>";
                }

                if (speed.Active == true)
                {
                    if (Ac.cheatsEnabled != true && Ac.majorEnabled != true)
                    {
                        Invoke("Warning",.15f);
                        speed.Active = false;
                    }
                    else
                    {
                        GUI.Window(1337,SpeedWindow, UI, "");
                    }
                }

                if (friction.Active == true)
                {
                    frictiontxt = "<color=lime>[</color>Frictionless<color=lime>]</color>";
                    //Text(35, "Frictionless Mode ON", new Rect(8/Screen.width, 90, 1000, 1000), "white");
                }
                else
                {
                   frictiontxt = "<color=red>[</color>Frictionless<color=red>]</color>";
                }
            }
        }
        public void UI(int WindowID)
        {
            switch (WindowID)
            {
                case 875:
                    //if (GUI.Button(new Rect(5, 40+(30*0), window.width - 10, 30), "")) MonoSingleton<T>.Instance.enabled = !MonoSingleton<T>.Instance.enabled;
                    if (GUI.Button(new Rect(5, 20, window.width - 10, 30), frictiontxt)) friction.Active = !friction.Active;
                    if (GUI.Button(new Rect(5, 20+(35*1), window.width - 10, 30), sandtxt)) sand.Active = !sand.Active;
                    if (GUI.Button(new Rect(5, 20+(35*2), window.width - 10, 30), "<color=red><i>SPEED</i></color> mode")) speed.Active = !speed.Active;
                    if (GUI.Button(new Rect(5, 20+(35*3), window.width - 10, 30), vamptxt)) vamp.Active = !vamp.Active;
                    if (GUI.Button(new Rect(5, 20+(35*4), window.width - 10, 30), testtxt)) test.Active = !test.Active;
                    break;
                case 1337:
                    SpeedMenu(SpeedWindow);
                    break;
            }
        }
        float
            _spd = 0,
            _jump = 0;
        void SpeedMenu(Rect pos)
        {
            _spd = GUI.HorizontalSlider(new Rect(5, 40, pos.width - 120, 25), _spd, -100, 100);
            _jump = GUI.HorizontalSlider(new Rect(5, 75, pos.width - 120, 25), _jump, -100, 100);
            GUI.Label(new Rect(0, -10, 400, 50), $"<size=25><i><color=yellow>SPEED</color></i>:{speed.spd}/{speed.jump}</size>");
            if(GUI.Button(new Rect(pos.width / 2 + 125, 0, 75, 25), "Back"))
            {
                speed.Active = false;
            }
            if(GUI.Button(new Rect(pos.width / 2 + 40, 0, 75, 25), "Reset"))
            {
                speed.spd = 750;
                speed.jump = 90;
            }
            if(GUI.Button(new Rect(pos.width/2+100, 35, 100, 25), $"<size=13>Speed+- {(int)_spd}</size>"))
            {
                speed.spd += (int)_spd;
            }
            if(GUI.Button(new Rect(pos.width/2+100, 70, 100, 25), $"<size=13>Jump+- {(int)_jump}</size>"))
            {
                speed.jump += (int)_jump;
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
