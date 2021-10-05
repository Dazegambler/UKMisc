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

        public bool
            Active;

        string[] 
            Main = { "UK Misc 2.0.0", "Back(Wont do anything here)", "Modifiers", "Tools" };
        int[] 
            MainVals = { 0, 1, 2, 3 };

        string[] 
            Modifiers = { "MODIFIERS", "Back", "Toggle <color=orange>Sand</color> Mode", "Toggle <color=maroon>Vampire</color> Hook", "Toggle <color=red>Test</color> Mode", "<size=13>Toggle <color=yellow><i>SPEED</i></color> Multiplier</size>", "<size=13>Toggle <color=cyan>Frictionless</color> Mode</size>" };
        bool[] 
            _Modifiers = { false, false, false, false, false };

        string[] 
            Tools = { "TOOLS", "Back", "Hud Modifiers" };
        int[] 
            _Tools = { 0, 1, 4 };

        string[] 
            Huds = { "HUD MODIFIERS", "Back", "<color=cyan>Nail</color> Counter", "HP Counter" };
        int[] 
            _Huds = { 0, 1, 5, 6 };

        int id;string Title;

        Rect
            ConfigWindow = new Rect(60/Screen.width,400/Screen.height, 300, 350),
            wind,
            SpeedWindow = new Rect(Screen.width / 1.275f,Screen.width/1920, 400, 100),
            window = new Rect(60/Screen.width,400/Screen.height,200,75);

        void Awake()
        {
        }

        public void OnGUI()
        {
            GUI.skin = skin;
            if(SceneManager.GetActiveScene().name != "Intro"
                && SceneManager.GetActiveScene().name != "Main Menu")
            {
                GUI.Window(id, wind, UI,Title);

                if (MonoSingleton<SandMode>.Instance.isActiveAndEnabled == true)
                {
                    Text(35, "Sand Mode ON", new Rect(8/Screen.width,10/Screen.height, 1000,1000),"white");
                }

                if(gameObject.GetComponent<TestModifier>().isActiveAndEnabled == true)
                {
                    Text(35, "Test Mode ON", new Rect(8/Screen.width,130/Screen.height, 1000, 1000), "white");
                }

                if (gameObject.GetComponent<VampHook>().isActiveAndEnabled == true)
                {
                    if (MonoSingleton<AssistController>.Instance.cheatsEnabled != true
                        && MonoSingleton<AssistController>.Instance.majorEnabled != true)
                    {
                        Invoke("Warning",.15f);
                    }
                    else
                    {
                        Text(35, "Vampire Mode ON", new Rect(8/Screen.width,160/Screen.height, 1000, 1000), "white");
                    }
                }

                if (gameObject.GetComponent<SpeedMod>().isActiveAndEnabled == true)
                {
                    if (MonoSingleton<AssistController>.Instance.cheatsEnabled != true
                        && MonoSingleton<AssistController>.Instance.majorEnabled != true)
                    {
                        Invoke("Warning",.15f);
                        gameObject.GetComponent<SpeedMod>().enabled = false;
                    }
                    else
                    {
                    }
                }

                if (gameObject.GetComponent<FrictionMod>().isActiveAndEnabled == true)
                {
                    Text(35, "Frictionless Mode ON", new Rect(8, 70/Screen.height, 1000, 1000), "white");
                }
            }
        }
        public void UI(int WindowID)
        {
            switch (WindowID)
            {
                case 0:
                    Menu(Main, MainVals, window);
                    break;
                case 2:
                    MenuBool(Modifiers, _Modifiers, window);
                    break;
                case 3:
                    Menu(Tools, _Tools, window);
                    break;
                case 4:
                    Menu(Huds, _Huds, window);
                    break;
                case 7:
                    SpeedMenu(SpeedWindow);
                    break;
            }
        }
        private void Menu(string[] strings, int[] vals, Rect Pos)
        {
            Title = strings[0];
            if (id != 0)
            {
                switch (GUI.Button(new Rect(0, 25, Pos.width, 35), strings[1]))
                {
                    case true:
                        id = 0;
                        break;
                }
            }
            else GUI.Label(new Rect(0, 25, Pos.width, 35), "<size=14>Welcome to <i><b>UKMisc</b></i> 2.0.0</size>");
            for (int i = 2; i < strings.Length; i++)
            {
                switch (GUI.Button(new Rect(0, (40 * i) - 15, Pos.width, 35), strings[i]))
                {
                    case true:
                        id = vals[i];
                        break;
                }
                Pos = new Rect(Pos.x, Pos.y, Pos.width, 40 * strings.Length);
            }
            wind = Pos;
        }
        private void MenuBool(string[] strings, bool[] vals, Rect Pos)
        {
            Title = strings[0];
            switch (GUI.Button(new Rect(0, 25, Pos.width, 35), strings[1]))
            {
                case true:
                    id = 0;
                    break;
            }
            for (int i = 2; i < strings.Length; i++)
            {
                switch (GUI.Button(new Rect(0, (40 * i) - 15, Pos.width, 35), strings[i]))
                {
                    case true:
                        vals[i - 2] = !vals[i - 2];
                        break;
                }
                Pos = new Rect(Pos.x, Pos.y, Pos.width, 40 * strings.Length);
            }
            wind = Pos;
        }
        float
            _spd,
            _jump;
        void SpeedMenu(Rect pos)
        {
            wind = pos;
            Title = "";
            _spd = GUI.HorizontalSlider(new Rect(120/Screen.width, 40/Screen.height, pos.width - 120, 25), _spd, -100, 100);
            _jump = GUI.HorizontalSlider(new Rect(120, 75, pos.width - 120, 25), _jump, -100, 100);
            GUI.Label(new Rect(0, -10/Screen.height, 400, 50), $"<size=25><i><color=yellow>SPEED</color></i>:{MonoSingleton<SpeedMod>.Instance.spd}/{MonoSingleton<SpeedMod>.Instance.jump}</size>");
            if(GUI.Button(new Rect(pos.width / 2 + 125, 0, 75, 25), "Back"))
            {
                MonoSingleton<SpeedMod>.Instance.enabled = false;
                id = 2;
            }
            if(GUI.Button(new Rect(pos.width / 2 + 40, 0, 75, 25), "Reset"))
            {
                MonoSingleton<SpeedMod>.Instance.spd = 750;
                MonoSingleton<SpeedMod>.Instance.jump = 90;
            }
            if(GUI.Button(new Rect(pos.width/2+10, 35/pos.height, 100, 25), $"<size=13>Speed+- {(int)_spd}</size>"))
            {
                MonoSingleton<SpeedMod>.Instance.spd += (int)_spd;
            }
            if(GUI.Button(new Rect(pos.width/2+10, 70/pos.height, 100, 25), $"<size=13>Jump+- {(int)_jump}</size>"))
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
