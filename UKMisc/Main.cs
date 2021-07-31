using BepInEx;
using BepInEx.Configuration;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace UKMisc
{
    [BepInPlugin("UK.MISC", "UKMisc", "1.0.0")]
    public class Plugin : BaseUnityPlugin
    {
        private ConfigEntry<KeyCode> menuKey;
        private ConfigEntry<Color> MainColor, BackgroundColor, AmmoMain, AmmoBack;
        private ConfigEntry<float> AmmoposX, AmmoposY;
        int i = 0;
        bool sandrdy = false, testrdy = false, ammordy = false, hookrdy = false, _hookrdy = false, mnurdy = false;
        GameObject[] Objects = null, pool = null;
        GameObject WC, Player, HUD, Hook;
        public Rect MenuWindow = new Rect(60, 400, 400, 200), ModWindow, ToolsWindow, SpawnWindow;
        Text Txt; Font fnt;
        AssetBundle Bundle = Plugin.LoadAssetBundle(Properties.Resources.uibundle);
        Texture2D UISKIN, UISKINBT;
        GUIStyle STY,STYB,AMO,AMOB,AMOSTY,AMOSTYB,MNU,_UI,_UIBT,WAR,WARB;

        public void Start()
        {
            menuKey = Config.Bind("Binds", "Menu Toggle", KeyCode.K, "Toggles the menu");
            MainColor = Config.Bind("General", "Main GUI Color", Color.red, "Main color used for GUI");
            BackgroundColor = Config.Bind("General", "Background GUI Color", Color.white, "Background color used for GUI");
            AmmoMain = Config.Bind("Ammo Counter", "Ammo Counter Main Color", Color.white, "Main Color Used for ammo counter");
            AmmoBack = Config.Bind("Ammo Counter", "Ammo Counter  Background Color", Color.red, "Background Color Used for ammo counter");
            AmmoposX = Config.Bind("Ammo Counter", "NaiAmmo Counter X pos", 15f, "");
            AmmoposY = Config.Bind("Ammo Counter", "NaiAmmo Counter Y pos", 625f, "");
            UISKIN = Bundle.LoadAsset<GameObject>("UKUITest").GetComponent<SpriteRenderer>().sprite.texture;
            UISKINBT = Bundle.LoadAsset<GameObject>("UKUI(Test)").GetComponent<SpriteRenderer>().sprite.texture;
        }
        public void Update()
        {
            if(WC == null||HUD == null||Hook == null||Player == null||pool == null) ObjFind();
            else Objects = FindObjectsOfType<GameObject>();
            Binds();
            switch (sandrdy)
            {
                case true:
                    Sand();
                    break;
                default:
                    break;
            }
            switch (testrdy)
            {
                case true:
                    Test();
                    break;
                default:
                    break;
            }
            switch (_hookrdy)
            {
                case true:
                    VampHook();
                    break;
                default:
                    break;
            }
        }
        public void OnGUI()
        {
            Styles();
            switch (Txt)
            {
                case null:
                    Txt = GameObject.FindObjectOfType<Text>();
                    break;
                default:
                    switch (fnt)
                    {
                        case null:
                            fnt = Txt.font;
                            break;
                        default:
                            break;
                    }
                    break;
            }
            switch (sandrdy)
            {
                case true:
                    Text("Sand Mode ON", new Rect(8, 0, 150, 20), STY, STYB);
                    break;
                default:
                    break;
            }
            switch (testrdy)
            {
                case true:
                    Text("Test Mode ON", new Rect(8, 25, 150, 20), STY, STYB);
                    break;
                default:
                    break;
            }
            switch (hookrdy)
            {
                case true:
                    var As = GameObject.Find("GameController").GetComponent<AssistController>();
                    if (As.cheatsEnabled != true || As.majorEnabled != true)
                    {
                        Warning();
                        _hookrdy = false;
                    }
                    else
                    {
                        Text("Vampire Mode ON", new Rect(8, 50, 150, 20), STY, STYB);
                        _hookrdy = true;
                    }
                    break;
                default:
                    break;
            }
            switch (mnurdy)
            {
                case true:
                    switch (i)
                    {
                        case 0:
                            MenuWindow = GUI.Window(0, MenuWindow, UI, "", STY);
                            break;
                        case 1:
                            ToolsWindow = GUI.Window(1, MenuWindow, UI, "", STY);
                            break;
                        default:
                            ModWindow = GUI.Window(2, MenuWindow, UI, "", STY);
                            break;
                    }
                    break;
                default:
                    break;
            }
            if (ammordy)
            {
                if (WC != null)
                {
                    var wc = WC.GetComponent<WeaponCharges>();
                    switch (wc)
                    {
                        case null:
                            break;
                        default:
                            Text("NAILS:", new Rect(AmmoposX.Value, AmmoposY.Value-25, 300, 100), AMOSTY, AMOSTYB);
                            Text(Mathf.RoundToInt(wc.naiAmmo).ToString(), new Rect(AmmoposX.Value, AmmoposY.Value,300,100), AMO, AMOB);
                            Text("HEATSINKS:", new Rect(AmmoposX.Value+75, AmmoposY.Value-25, 300, 100), AMOSTY, AMOSTYB);
                            Text( wc.nai0charge.ToString(), new Rect(AmmoposX.Value+95, AmmoposY.Value, 300, 100), AMO, AMOB);
                            break;
                    }
                }
            }
        }
        private void Styles()
        {
            switch (STY)
            {
                case null:
                    STY = new GUIStyle(); STY.font = fnt; STY.fontSize = 23; STY.normal.textColor = MainColor.Value;
                    break;
                default:
                    break;
            }
            switch (STYB)
            {
                case null:
                    STYB = new GUIStyle(); STYB.font = fnt; STYB.fontSize = STY.fontSize; STYB.normal.textColor = BackgroundColor.Value;
                    break;
                default:
                    break;
            }
            switch (AMO)
            {
                case null:
                    AMO = new GUIStyle(); AMO.font = fnt; AMO.fontSize = 26; AMO.normal.textColor = AmmoMain.Value;
                    break;
                default:
                    break;
            }
            switch (AMOB)
            {
                case null:
                    AMOB = new GUIStyle(); AMOB.font = fnt; AMOB.fontSize = AMO.fontSize; AMOB.normal.textColor = AmmoBack.Value;
                    break;
                default:
                    break;
            }
            switch (AMOSTY)
            {
                case null:
                    AMOSTY = new GUIStyle(); AMOSTY.font = fnt; AMOSTY.fontSize = 19; AMOSTY.normal.textColor = Color.white;
                    break;
                default:
                    break;
            }
            switch (AMOSTYB)
            {
                case null:
                    AMOSTYB = new GUIStyle(); AMOSTYB.font = fnt; AMOSTYB.fontSize = 19; AMOSTYB.normal.textColor = Color.black;
                    break;
                default:
                    break;
            }
            switch (MNU)
            {
                case null:
                    MNU = new GUIStyle(); MNU.font = fnt; MNU.fontSize = 24; MNU.normal.textColor = Color.white;
                    break;
                default:
                    break;
            }
            switch (_UI)
            {
                case null:
                    _UI = new GUIStyle(); _UI.font = fnt; _UI.normal.textColor = MainColor.Value; _UI.normal.background = UISKIN; _UI.alignment = TextAnchor.UpperCenter;
                    break;
                default:
                    break;
            }
            switch (_UIBT)
            {
                case null:
                    _UIBT = new GUIStyle(); _UIBT.font = fnt; _UIBT.normal.textColor = MainColor.Value; _UIBT.normal.background = UISKINBT; _UIBT.alignment = TextAnchor.MiddleCenter;
                    break;
                default:
                    break;
            }
            switch (WAR)
            {
                case null:
                    WAR = new GUIStyle(); WAR.font = fnt; WAR.normal.textColor = Color.red;WAR.fontSize = 40;
                    break;
            }
            switch (WARB)
            {
                case null:
                    WARB = new GUIStyle(); WARB.font = fnt; WARB.normal.textColor = Color.black; WARB.fontSize = 40;
                    break;
            }

        }
        private void Warning()
        {
            GUI.Box(new Rect(Screen.width / 4, Screen.height / 2, 1000, 40), "", _UI);
            Text("MUST HAVE MAJOR ASSISTS/CHEATS ACTIVATED", new Rect(Screen.width / 4, Screen.height / 2, 1000, 100), WAR, WARB);
        }
        private void Text(string txt, Rect pos, GUIStyle main, GUIStyle back)
        {
            //Generates Funky text with custom colors
            GUI.Label(pos, txt, back);
            GUI.Label(pos, txt, main);
        }
        public void UI(int WindowID)
        {
            switch (WindowID)
            {
                case 0:
                    GUI.Box(new Rect(0, 0, 250, 200),"UK Misc 1.0.0",_UI);
                    if (GUI.Button(new Rect(5, 20, UISKINBT.width,UISKINBT.height/2), "Toggle Test Mode",_UIBT)) testrdy = !testrdy;
                    if (GUI.Button(new Rect(5, 60, UISKINBT.width, UISKINBT.height / 2), "Game Modifiers",_UIBT)) i = 1;
                    if (GUI.Button(new Rect(5, 100, UISKINBT.width, UISKINBT.height / 2), "Tools",_UIBT)) i = 2;
                    break;
                case 1:
                    GUI.Box(new Rect(0, 0, 250, 200), "MODIFIERS",_UI);
                    if (GUI.Button(new Rect(5, 20, UISKINBT.width, UISKINBT.height / 2), "Back",_UIBT)) i = 0;
                    if (GUI.Button(new Rect(5, 60, UISKINBT.width, UISKINBT.height / 2), "Sand Mode", _UIBT)) sandrdy = !sandrdy;
                    if (GUI.Button(new Rect(5, 100, UISKINBT.width, UISKINBT.height / 2), "Toggle Vampire hook", _UIBT)) hookrdy = !hookrdy;
                    break;
                case 2:
                    GUI.Box(new Rect(0, 0, 250, 200), "TOOLS",_UI);
                    if (GUI.Button(new Rect(5, 20, UISKINBT.width, UISKINBT.height / 2), "Back",_UIBT)) i = 0;
                    if (GUI.Button(new Rect(5, 60, UISKINBT.width, UISKINBT.height / 2), "Ammo Counter Toggle", _UIBT)) ammordy = !ammordy;
                    break;
            }
        }
        private void ObjFind()
        {
            WC = GameObject.FindGameObjectWithTag("GunControl");
            HUD = GameObjFind("HUD", HUD);
            Hook = GameObjFind("Hook Arm", Hook);
            Player = GameObjFind("Player", Player);
            pool = Resources.FindObjectsOfTypeAll<GameObject>();
        }
        private void Binds()
        {
            if (Input.GetKeyDown(menuKey.Value)) mnurdy = !mnurdy;
        }
        private void Test()
        {
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
        private GameObject GameObjFind(string name, GameObject target)
        {
            //Find Object in Scene
            if(target == null) target = GameObject.Find(name);
            return target;
        }
        static AssetBundle LoadAssetBundle(byte[] Bytes)
        {
            if (Bytes == null) throw new ArgumentNullException(nameof(Bytes));
            var bundle = AssetBundle.LoadFromMemory(Bytes);
            return bundle;
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
    }
}
