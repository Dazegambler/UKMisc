using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        Rect
        ConfigWindow = new Rect(60/Screen.width,400/Screen.height, 300, 350),
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
                if (gameObject.GetComponent<SandMode>().isActiveAndEnabled == true)
                {
                    Text(35, "Sand Mode ON", new Rect(
                        8/Screen.width,
                        10/Screen.height, 
                        1000, 
                        1000), 
                        "white");
                }

                if(gameObject.GetComponent<TestModifier>().isActiveAndEnabled == true)
                {
                    Text(35, "Test Mode ON", new Rect(8, ratio - (Screen.height / 2) + 130, 1000, 1000), "white");
                }
            }
            switch (ACT.name)
            {
                case "Intro":
                    break;
                default:
                    //if (Enemies != null)Text(35,Enemies.Count.ToString(),new Rect(Screen.width/2,Screen.height/2,1000,1000),"White");
                    switch (_Modifiers[2])
                    {
                        case true:
                            break;
                        default:
                            break;
                    }
                    switch (_Modifiers[1])
                    {
                        case true:
                            var As = GameObject.Find("GameController").GetComponent<AssistController>();
                            if (As.cheatsEnabled != true && As.majorEnabled != true)
                            {
                                Warning();
                                hookrdy = false;
                            }
                            else
                            {
                                Text(35, "Vampire Mode ON", new Rect(8, ratio - (Screen.height / 2) + 160, 1000, 1000), "white");
                                hookrdy = true;
                            }
                            break;
                        default:
                            hookrdy = false;
                            break;
                    }
                    switch (_Modifiers[3])
                    {
                        case true:
                            var As = GameObject.Find("GameController").GetComponent<AssistController>();
                            if (As.cheatsEnabled != true && As.majorEnabled != true)
                            {
                                Warning();
                                spdrdy = false;
                            }
                            else
                            {
                                spdrdy = true;
                            }
                            break;
                        default:
                            spdrdy = false;
                            break;
                    }
                    switch (_Modifiers[4])
                    {
                        case true:
                            Text(35, "Frictionless Mode ON", new Rect(8, ratio - (Screen.height / 2) + 70, 1000, 1000), "white");
                            break;
                        default:
                            break;

                    }
                    switch (toggle)
                    {
                        case true:
                            GUI.Window(id, wind, UI, Title);
                            break;
                    }
                    switch (Nail)
                    {
                        case true:
                            int nails = (int)GameObject.Find("Player").GetComponentInChildren<WeaponCharges>().naiAmmo;
                            string col;
                            if (nails >= 75) col = "cyan";
                            else if (nails >= 35 && nails < 75) col = "darkblue";
                            else col = "black";
                            Text(35, "NAILS", new Rect(NailCounter.Value.x, NailCounter.Value.y - 50f, 1000, 1000), "white");
                            Text(60, nails.ToString(), new Rect(NailCounter.Value.x, NailCounter.Value.y, 1000, 1000), col);
                            break;
                    }
                    switch (Hp)
                    {
                        case true:
                            int hp = mov.hp;
                            float hard = Mathf.Round(mov.antiHp * 100) * 0.01f;
                            float Sta = Mathf.Round(mov.boostCharge * 100) * 0.01f;
                            string col, hardcol;
                            if (hp >= 101) col = "lime";
                            else if (hp >= 75 && hp < 101) col = "green";
                            else if (hp >= 45 && hp < 75) col = "grey";
                            else if (hp >= 25 && hp < 45) col = "red";
                            else col = "black";
                            Text(35, "HP/Stamina", new Rect(HPCounter.Value.x, HPCounter.Value.y - 50f, 1000f, 1000f), "white");
                            if (hard > 0)
                            {
                                if (hard > 75) hardcol = "red";
                                else if (hard > 50 && hard < 75) hardcol = "maroon";
                                else if (hard > 25 && hard < 50) hardcol = "brown";
                                else hardcol = "black";
                                Text(15, $"*{hard}", new Rect(HPCounter.Value.x, HPCounter.Value.y - 25f, 1000f, 1000f), hardcol);
                            }
                            Text(45, $"{hp}/{Sta}", new Rect(HPCounter.Value.x, HPCounter.Value.y, 1000f, 1000f), col);
                            break;
                    }
                    break;
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
                case 5:
                    ConfigMenu(Huds[2], ConfigWindow, NailCounter);
                    break;
                case 6:
                    ConfigMenu(Huds[3], ConfigWindow, HPCounter);
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
        void ConfigMenu(string _Title, Rect pos)
        {
            wind = pos;
            Title = "";
            GUI.Label(new Rect(0, -10, 400, 50), $"<size=22>{_Title}</size>");
            switch (GUI.Button(new Rect(pos.width / 2 + 15, 5, pos.width / 2 - 15, 30), "Toggle"))
            {
                case true:
                    switch (_Title)
                    {
                        case "<color=cyan>Nail</color> Counter":
                            Nail = !Nail;
                            break;
                        case "HP Counter":
                            Hp = !Hp;
                            break;
                    }
                    break;
            }
            for (int i = 0; i <= 4; i++)
            {
                switch (i)
                {
                    case 0:
                        switch (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X+.5"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x + .5f, ui.Value.y);
                                break;
                        }
                        switch (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y+.5"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x, ui.Value.y + .5f);
                                break;
                        }
                        break;
                    case 1:
                        switch (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X+1"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x + 1f, ui.Value.y);
                                break;
                        }
                        switch (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y+1"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x, ui.Value.y + 1f);
                                break;
                        }
                        break;
                    case 2:
                        switch (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X-.5"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x - .5f, ui.Value.y);
                                break;
                        }
                        switch (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y-.5"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x, ui.Value.y - .5f);
                                break;
                        }
                        break;
                    case 3:
                        switch (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X-1"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x - 1f, ui.Value.y);
                                break;
                        }
                        switch (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y-1"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x, ui.Value.y - 1f);
                                break;
                        }
                        break;
                    case 4:
                        switch (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), $"X+ {(int)X}"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x + X, ui.Value.y);
                                break;
                        }
                        switch (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), $"Y+ {(int)Y}"))
                        {
                            case true:
                                ui.Value = new Vector2(ui.Value.x, ui.Value.y + Y);
                                break;
                        }
                        break;
                }
            }
            GUI.Label(new Rect(3.5f, 230, pos.width, 50), "<size=22><X/Y----------------></size>");
            X = GUI.HorizontalSlider(new Rect(0, 265, pos.width, 10), X, -100, 100);
            Y = GUI.HorizontalSlider(new Rect(0, 295, pos.width, 10), Y, -100, 100);
            if (GUI.Button(new Rect(pos.width / 2 + 15, 315, pos.width / 2 - 15, 30), "Back")) id = 4;
        }
        void SpeedMenu(Rect pos)
        {
            wind = pos;
            Title = "";
            GUI.Label(new Rect(0, -10, 400, 50), $"<size=25><i><color=yellow>SPEED</color></i>:{spd.Value}/{jump.Value}</size>");
            switch (GUI.Button(new Rect(pos.width / 2 + 125, 0, 75, 25), "Back"))
            {
                case true:
                    spdrdy = false;
                    id = 2;
                    _Modifiers[3] = false;
                    break;
            }
            switch (GUI.Button(new Rect(pos.width / 2 + 40, 0, 75, 25), "Reset"))
            {
                case true:
                    spd.Value = 750;
                    jump.Value = 90;
                    break;
            }
            switch (GUI.Button(new Rect(10, 35, 100, 25), $"<size=13>Speed+- {(int)_spd}</size>"))
            {
                case true:
                    spd.Value += (int)_spd;
                    break;
            }
            switch (GUI.Button(new Rect(10, 70, 100, 25), $"<size=13>Jump+- {(int)_jump}</size>"))
            {
                case true:
                    jump.Value += (int)_jump;
                    break;
            }
            _spd = GUI.HorizontalSlider(new Rect(120, 40, pos.width - 120, 25), _spd, -100, 100);
            _jump = GUI.HorizontalSlider(new Rect(120, 75, pos.width - 120, 25), _jump, -100, 100);
        }
        private void Text(int fntsize, string txt, Rect Pos, string col)
        {
            GUI.Label(new Rect(Pos.x + 1f, Pos.y - 1f, Pos.width, Pos.height), $"<size={fntsize}><color=black>{txt}</color></size>");
            GUI.Label(Pos, $"<size={fntsize}><color={col}>{txt}</color></size>");
        }
        private void Warning()
        {
            Text(40, "<b>MUST HAVE MAJOR ASSISTS/CHEATS ACTIVATED</b>", new Rect(Screen.width / 4, Screen.height / 2, 1000, 100), "red");
        }
        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
}
