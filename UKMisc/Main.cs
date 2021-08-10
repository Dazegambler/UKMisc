using BepInEx;
using BepInEx.Configuration;
using System;
using System.Reflection;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UKMisc
{
    [BepInPlugin("UK.MISC", "UKMisc", "2.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        AssetBundle UIBundle = Plugin.LoadAssetBundle(Properties.Resources.ui);
        GUISkin skin;
        string Title;
        /// <summary>
        ///Button Configs.
        string[] Main = { "UK Misc 2.0.0", "Back(Wont do anything here)", "Modifiers", "Tools" };
        int[] MainVals = { 0, 1, 2, 3 };
        string[] Modifiers = { "MODIFIERS", "Back", "Toggle <color=orange>Sand</color> Mode", "Toggle <color=maroon>Vampire</color> Hook", "Toggle <color=red>Test</color> Mode","<size=13>Toggle <color=yellow><i>SPEED</i></color> Multiplier</size>" };
        bool[] _Modifiers = { false, false, false ,false};
        string[] Tools = { "TOOLS", "Back", "Hud Modifiers" };
        int[] _Tools = { 0, 1, 4 };
        string[] Huds = { "HUD MODIFIERS", "Back", "<color=cyan>Nail</color> Counter", "HP Counter" };
        int[] _Huds = { 0, 1, 5, 6 };
        /// </summary>

        int id; Rect wind,window,ConfigWindow; 
        float sWidth = Screen.width, ratio,X,Y;
        Vector3 _Ratio;

        bool toggle,hookrdy,spdrdy;
        bool Nail,Hp;

        ConfigEntry<Vector2> NailCounter,HPCounter;
        ConfigEntry<float> spd,jump;

        GameObject[] Objects,pool;
        GameObject WC, HUD, Hook, Player;

        public void Start()
        {
            NailCounter = Config.Bind("NAILCOUNTER","Pos",new Vector2(ratio+100,ratio+100));
            HPCounter = Config.Bind("HPCOUNTER","Pos",new Vector2(ratio+100,ratio+100));
            spd = Config.Bind("SPEED MULTIPLIER","speed",750f);
            jump = Config.Bind("SPEED MULTIPLIER","jump",90f);
            skin = UIBundle.LoadAsset<GUISkin>("UI");
        }
        public void Update()
        {
            window = new Rect(ratio + 60, ratio + 400, 200, 75);
            ConfigWindow = new Rect(ratio + 60, ratio + 400, 300, 350);
            _Ratio = new Vector3(ratio, ratio, 1);
            ratio = sWidth / 1920;
            Scene ACT = SceneManager.GetActiveScene();
            switch (ACT.name)
            {
                case "Intro":
                    break;
                default:
                    if (WC == null || HUD == null || Hook == null || Player == null || pool == null) ObjFind();
                    else Objects = FindObjectsOfType<GameObject>();
                    Binds();

                    switch (_Modifiers[0])
                    {
                        case true:
                            Sand();
                            break;
                        default:
                            break;
                    }
                    switch (_Modifiers[2])
                    {
                        case true:
                            Test();
                            break;
                        default:
                            break;
                    }
                    switch (hookrdy)
                    {
                        case true:
                            VampHook();
                            break;
                        default:
                            break;
                    }
                    switch (spdrdy)
                    {
                        case true:
                            Speed(spd.Value,jump.Value);
                            break;
                        default:
                            var _spd = Player.GetComponent<NewMovement>();
                            _spd.walkSpeed = 750f;
                            _spd.jumpPower = 90;
                            _spd.wallJumpPower = 150;
                            break;
                    }
                    break;
            }
        }
        private void VampHook()
        {
            var hp = Player.GetComponent<NewMovement>();
            switch (hp)
            {
                case null:
                    break;
                default:
                    foreach (GameObject obj in Objects)
                    {
                        switch (obj.tag)
                        {
                            case "Enemy":
                                var eid = obj.GetComponent<EnemyIdentifier>(); eid = obj.GetComponentInChildren<EnemyIdentifier>();
                                switch (eid.hooked)
                                {
                                    case true:
                                        if (eid.health > 0.1f)
                                        {
                                            if (hp.hp < 200) hp.hp++;
                                        }
                                        switch (eid.enemyType)
                                        {
                                            case EnemyType.Filth:
                                                var Filhp = obj.GetComponent<Zombie>();
                                                if (Filhp.health > 0.1f) Filhp.health -= Filhp.health * 0.03f;
                                                break;
                                            case EnemyType.Stray:
                                                var Strhp = obj.GetComponent<Zombie>();
                                                if (Strhp.health > 0.1f) Strhp.health -= Strhp.health * 0.0250f;
                                                break;
                                            case EnemyType.Schism:
                                                var Schhp = obj.GetComponent<Zombie>();
                                                if (Schhp.health > 0.1f) Schhp.health -= Schhp.health * 0.0250f;
                                                break;
                                            case EnemyType.Soldier:
                                                var Solhp = obj.GetComponent<Zombie>();
                                                if (Solhp.health > 0.1f) Solhp.health -= Solhp.health * 0.0250f;
                                                break;
                                            case EnemyType.Stalker:
                                                var Stalhp = obj.GetComponent<Machine>();
                                                if (Stalhp.health > 0.1f) Stalhp.health -= Stalhp.health * 0.0250f;
                                                break;
                                            case EnemyType.Sisyphus:
                                                var Sisyhp = obj.GetComponent<Machine>();
                                                if (Sisyhp.health > 0.1f) Sisyhp.health -= Sisyhp.health * 0.005f;
                                                break;
                                            case EnemyType.Swordsmachine:
                                                var Swohp = obj.GetComponent<Machine>();
                                                if (Swohp.health > 0.1f) Swohp.health -= Swohp.health * 0.0065f;
                                                break;
                                            case EnemyType.Drone:
                                                var Drohp = obj.GetComponent<Drone>();
                                                if (Drohp.health > 0.1f) Drohp.health -= Drohp.health * 0.03f;
                                                break;
                                            case EnemyType.Streetcleaner:
                                                var Strehp = obj.GetComponent<Machine>();
                                                if (Strehp.health > 0.1f) Strehp.health -= Strehp.health * 0.0095f;
                                                break;
                                            case EnemyType.Mindflayer:
                                                var Mndhp = obj.GetComponent<Machine>();
                                                if (Mndhp.health > 0.1f) Mndhp.health -= Mndhp.health * 0.01f;
                                                break;
                                            case EnemyType.MaliciousFace:
                                                var Malhp = obj.GetComponentInChildren<SpiderBody>();
                                                if (Malhp.health > 0.1f) Malhp.health -= Malhp.health * 0.0065f;
                                                break;
                                            case EnemyType.Cerberus:
                                                var Cerhp = obj.GetComponentInChildren<Statue>();
                                                if (Cerhp.health > 0.1f) Cerhp.health -= Cerhp.health * 0.0065f;
                                                break;
                                            case EnemyType.HideousMass:
                                                var Mashp = obj.GetComponent<Statue>();
                                                if (Mashp.health > 0.1f) Mashp.health -= Mashp.health * 0.0075f;
                                                break;
                                            case EnemyType.Virtue:
                                                var Virhp = obj.GetComponent<Drone>();
                                                if (Virhp.health > 0.1f) Virhp.health -= Virhp.health * 0.01f;
                                                break;
                                            default:
                                                break;
                                        }
                                        break;
                                    default:
                                        break;
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        private void Sand()
        {
            switch (Objects)
            {
                case null:
                    break;
                default:
                    foreach (GameObject Obj in Objects)
                    {
                        switch (Obj.tag)
                        {
                            case "Enemy":
                                var eid = Obj.GetComponent<EnemyIdentifier>();
                                switch (eid.sandified)
                                {
                                    case true:
                                        break;
                                    default:
                                        eid.Sandify();
                                        break;
                                }
                                break;
                        }
                    }
                    break;
            }
        }
        private void Speed(float spd,float jump)
        {
            var _spd = Player.GetComponent<NewMovement>();
            _spd.walkSpeed = spd;
            _spd.jumpPower = jump;
            _spd.wallJumpPower = jump + 60;
        }
        private void ObjFind()
        {
            WC = GameObject.FindGameObjectWithTag("GunControl");
            HUD = GameObjFind("HUD", HUD);
            Hook = GameObjFind("Hook Arm", Hook);
            Player = GameObjFind("Player", Player);
            pool = Resources.FindObjectsOfTypeAll<GameObject>();
        }
        private GameObject GameObjFind(string name, GameObject target)
        {
            //Find Object in Scene
            if (target == null) target = GameObject.Find(name);
            return target;
        }
        private GameObject AssetFind(string name, GameObject Original)
        {
            //Find set Object in the prefabs
            foreach (GameObject obj in pool)
            {
                if (obj.gameObject.name == name)
                {
                    Original = obj;
                }
            }
            return Original;
        }
        private void Binds()
        {
            if (Input.GetKeyDown(KeyCode.K)) toggle = !toggle;
        }
        private void Test()
        {
        }
        public void OnGUI()
        {
            GUI.matrix = Matrix4x4.TRS(new Vector3(_Ratio.x, _Ratio.y, 0), Quaternion.identity, _Ratio);
            GUI.skin = skin;
            Scene ACT = SceneManager.GetActiveScene();
            switch (ACT.name)
            {
                case "Intro":
                    break;
                default:
                    switch (_Modifiers[0])
                    {
                        case true:
                            Text(35, "Sand Mode ON", new Rect(8, ratio-(Screen.height/2)+100, 1000, 1000),"white");
                            break;
                        default:
                            break;
                    }
                    switch (_Modifiers[2])
                    {
                        case true:
                            Text(35, "Test Mode ON", new Rect(8, ratio-(Screen.height/2)+130, 1000, 1000),"white");
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
                                Text(35, "Vampire Mode ON", new Rect(8, ratio - (Screen.height/2)+160, 1000, 1000),"white");
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
                                Text(35, $"<color=yellow><i>SPEED</i></color> Value: {spd.Value}/{jump.Value}", new Rect(ratio+8, ratio - (Screen.height / 2) + 190, 1000, 1000), "white");
                                spdrdy = true;
                            }
                            break;
                        default:
                            spdrdy = false;
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
                            Text(35,"NAILS", new Rect(NailCounter.Value.x, NailCounter.Value.y-50f, 1000, 1000),"white");
                            Text(60,nails.ToString(), new Rect(NailCounter.Value.x, NailCounter.Value.y, 1000, 1000),col);
                            break;
                    }
                    switch (Hp)
                    {
                        case true:
                            int hp = Player.GetComponent<NewMovement>().hp;
                            float hard = Mathf.Round(Player.GetComponent<NewMovement>().antiHp*100)*0.01f;
                            float Sta = Mathf.Round(Player.GetComponent<NewMovement>().boostCharge*100)*0.01f;
                            string col,hardcol;
                            if(hp>=101) col = "lime";
                            else if (hp>=75 && hp < 101) col = "green";
                            else if (hp>=45 && hp < 75) col = "grey";
                            else if(hp>=25 && hp<45) col = "red";
                            else col = "black";
                            Text(35,"HP/Stamina",new Rect(HPCounter.Value.x,HPCounter.Value.y-50f,1000f,1000f),"white");
                            if (hard > 0)
                            {
                                if (hard > 75) hardcol = "red";
                                else if(hard > 50 && hard < 75) hardcol = "maroon";
                                else if(hard>25 && hard<50) hardcol = "brown";
                                else hardcol = "black";
                                Text(15, $"*{hard}", new Rect(HPCounter.Value.x, HPCounter.Value.y - 25f, 1000f, 1000f),hardcol);
                            }
                            Text(45,$"{hp}/{Sta}",new Rect(HPCounter.Value.x,HPCounter.Value.y,1000f,1000f),col);
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
            }
        }
        private void Menu(string[] strings, int[] vals, Rect Pos)
        {
            Title = strings[0];
            if (id != 0)
            {
                if (GUI.Button(new Rect(0, 25, Pos.width, 35), strings[1]))
                {
                    id = 0;
                }
            }
            else GUI.Label(new Rect(0, 25, Pos.width, 35), "<size=14>Welcome to <i><b>UKMisc</b></i> 2.0.0</size>");
            for (int i = 2; i < strings.Length; i++)
            {
                if (GUI.Button(new Rect(0, (40 * i) - 15, Pos.width, 35), strings[i]))
                {
                    id = vals[i];
                }
                window = new Rect(window.x, window.y, window.width, 40 * strings.Length);
            }
            wind = window;
        }
        private void MenuBool(string[] strings, bool[] vals, Rect Pos)
        {
            Title = strings[0];
            if (GUI.Button(new Rect(0, 25, Pos.width, 35), strings[1]))
            {
                id = 0;
            }
            for (int i = 2; i < strings.Length; i++)
            {
                if (GUI.Button(new Rect(0, (40 * i) - 15, Pos.width, 35), strings[i]))
                {
                    vals[i - 2] = !vals[i - 2];
                }
                window = new Rect(window.x, window.y, window.width, 40 * strings.Length);
            }
            wind = window;
        }
        void ConfigMenu(string _Title, Rect pos, ConfigEntry<Vector2> ui)
        {
            wind = ConfigWindow;
            Title = "";
            GUI.Label(new Rect(0, -10, 400, 50), $"<size=22>{_Title}</size>");
            if (GUI.Button(new Rect(pos.width / 2 + 15, 5, pos.width / 2 - 15,30), "Toggle"))
            {
                switch (_Title)
                {
                    case "<color=cyan>Nail</color> Counter":
                        Nail = !Nail;
                        break;
                    case "HP Counter":
                        Hp = !Hp;
                        break;
                }
            }
            for (int i = 0; i <= 4; i++)
            {
                switch (i)
                {
                    case 0:
                        if (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X+.5")) ui.Value = new Vector2(ui.Value.x + .5f, ui.Value.y);
                        if (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y+.5")) ui.Value = new Vector2(ui.Value.x, ui.Value.y + .5f);
                        break;
                    case 1:
                        if (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X+1")) ui.Value = new Vector2(ui.Value.x + 1f, ui.Value.y);
                        if (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y+1")) ui.Value = new Vector2(ui.Value.x, ui.Value.y + 1f);
                        break;
                    case 2:
                        if (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X-.5")) ui.Value = new Vector2(ui.Value.x - .5f, ui.Value.y);
                        if (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y-.5")) ui.Value = new Vector2(ui.Value.x, ui.Value.y - .5f);
                        break;
                    case 3:
                        if (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), "X-1")) ui.Value = new Vector2(ui.Value.x - 1f, ui.Value.y);
                        if (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), "Y-1")) ui.Value = new Vector2(ui.Value.x, ui.Value.y-1f);
                        break;
                    case 4:
                        if (GUI.Button(new Rect(0, 40 + (40 * i), pos.width / 2 - 15, 35), $"X+ {(int)X}")) ui.Value = new Vector2(ui.Value.x + X, ui.Value.y);
                        if (GUI.Button(new Rect(pos.width / 2 + 15, 40 + (40 * i), pos.width / 2 - 15, 35), $"Y+ {(int)Y}")) ui.Value = new Vector2(ui.Value.x, ui.Value.y + Y);
                        break;
                }
            }
            GUI.Label(new Rect(3.5f, 230, pos.width,50), "<size=22><X/Y----------------></size>");
            X = GUI.HorizontalSlider(new Rect(0, 265, pos.width, 10), X, -100, 100);
            Y = GUI.HorizontalSlider(new Rect(0, 295, pos.width, 10), Y, -100, 100);
            if (GUI.Button(new Rect(pos.width / 2 + 15, 315, pos.width / 2 - 15, 30), "Back"))id=4;
        }
        private void Text(int fntsize,string txt, Rect Pos,string col)
        {
            GUI.Label(new Rect(Pos.x+1f,Pos.y-1f,Pos.width,Pos.height),$"<size={fntsize}><color=black>{txt}</color></size>");
            GUI.Label(Pos,$"<size={fntsize}><color={col}>{txt}</color></size>");
        }
        private void Warning()
        {
            Text(40,"<b>MUST HAVE MAJOR ASSISTS/CHEATS ACTIVATED</b>", new Rect(Screen.width / 4, Screen.height / 2, 1000, 100),"red");
        }
        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
        }
    }
}
