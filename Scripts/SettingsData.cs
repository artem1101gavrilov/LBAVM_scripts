using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class SettingsData {
    //Поля с панели Игра
    public bool autosave;
    public bool saveAtRest;
    public int levelOfComplexity;
    public float mouseSensitivity;

    //Поля с панели Экран
    public float brightness;
    public bool generalSubtitles;
    public bool subtitlesOfDialogs;

    //Поля с панели Аудио
    public float totalVolume;
    public float soundEffects;
    public float music;

    //Поля с панели Управление
    public Dictionary<string, KeyCode> keys;

    //Класс для сохранения
    [System.Serializable]
    public class PlayerSettings //Класс для сохранения данных персонажа
    {
        public bool autosave;
        public bool saveAtRest;
        public int levelOfComplexity;
        public float mouseSensitivity;
        
        public float brightness;
        public bool generalSubtitles;
        public bool subtitlesOfDialogs;

        public float totalVolume;
        public float soundEffects;
        public float music;

        public string KeyUp;
        public string KeyLeft;
        public string KeyDown;
        public string KeyRight;
        public string KeyRoll;
        public string KeyAcceleration;
        public string KeyInteraction;
        public string KeyInventory;
        public string KeyMap;
        public string KeyJournal;
        public string KeySkills;
        public string KeyOrganizer;
        public string KeyMenu;
        public string KeyLpocket;
        public string KeyRpocket;
    }


    public SettingsData()
    {
        keys = new Dictionary<string, KeyCode>();
        if (File.Exists(Application.dataPath + "/settings.set"))
        {
            Debug.Log("LoadSettings");
            FileStream fs = new FileStream(Application.dataPath + "/settings.set", FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            try
            {
                PlayerSettings positionPlayer = (PlayerSettings)bformatter.Deserialize(fs);
                autosave = positionPlayer.autosave;
                saveAtRest = positionPlayer.saveAtRest;
                levelOfComplexity = positionPlayer.levelOfComplexity;
                mouseSensitivity = positionPlayer.mouseSensitivity;

                brightness = positionPlayer.brightness;
                generalSubtitles = positionPlayer.generalSubtitles;
                subtitlesOfDialogs = positionPlayer.subtitlesOfDialogs;

                totalVolume = positionPlayer.totalVolume;
                soundEffects = positionPlayer.soundEffects;
                music = positionPlayer.music;

                keys.Add("Up", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyUp));
                keys.Add("Left",(KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyLeft));
                keys.Add("Down", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyDown));
                keys.Add("Right", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyRight));
                keys.Add("Roll", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyRoll));
                keys.Add("Acceleration", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyAcceleration));
                keys.Add("Interaction", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyInteraction));
                keys.Add("Inventory", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyInventory));
                keys.Add("Map", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyMap));
                keys.Add("Journal", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyJournal));
                keys.Add("Skills", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeySkills));
                keys.Add("Organizer", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyOrganizer));
                keys.Add("Menu", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyMenu));
                keys.Add("Lpocket", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyLpocket));
                keys.Add("Rpocket", (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyRpocket));
            }
            catch (System.Exception e)
            {
                Debug.Log(e.Message);
            }
            finally
            {
                fs.Close();
            }
        }
        else
        {
            autosave = false;
            saveAtRest = false;
            levelOfComplexity = 1;
            mouseSensitivity = 0.5f;

            brightness = 0.5f;
            generalSubtitles = false;
            subtitlesOfDialogs = false;

            totalVolume = 0.5f;
            soundEffects = 0.5f;
            music = 0.5f;

            keys.Add("Up", KeyCode.W);
            keys.Add("Left", KeyCode.A);
            keys.Add("Down", KeyCode.S);
            keys.Add("Right", KeyCode.D);
            keys.Add("Roll", KeyCode.Space);
            keys.Add("Acceleration", KeyCode.LeftShift);
            keys.Add("Interaction", KeyCode.E);
            keys.Add("Inventory", KeyCode.I);
            keys.Add("Map", KeyCode.M);
            keys.Add("Journal", KeyCode.J);
            keys.Add("Skills", KeyCode.K);
            keys.Add("Organizer", KeyCode.Tab);
            keys.Add("Menu", KeyCode.Escape);
            keys.Add("Lpocket", KeyCode.R);
            keys.Add("Rpocket", KeyCode.F);

            SaveSettings();
        }
    }

    public void SaveSettings()
    {
        Debug.Log("SaveSettings");
        PlayerSettings positionPlayer = new PlayerSettings();
        positionPlayer.autosave = autosave;
        positionPlayer.saveAtRest = saveAtRest;
        positionPlayer.levelOfComplexity = levelOfComplexity;
        positionPlayer.mouseSensitivity = mouseSensitivity;

        positionPlayer.brightness = brightness;
        positionPlayer.generalSubtitles = generalSubtitles;
        positionPlayer.subtitlesOfDialogs = subtitlesOfDialogs;

        positionPlayer.totalVolume = totalVolume;
        positionPlayer.soundEffects = soundEffects;
        positionPlayer.music = music;

        positionPlayer.KeyUp = keys["Up"].ToString();
        positionPlayer.KeyLeft = keys["Left"].ToString();
        positionPlayer.KeyDown = keys["Down"].ToString();
        positionPlayer.KeyRight = keys["Right"].ToString();
        positionPlayer.KeyRoll = keys["Roll"].ToString();
        positionPlayer.KeyAcceleration = keys["Acceleration"].ToString();
        positionPlayer.KeyInteraction = keys["Interaction"].ToString();
        positionPlayer.KeyInventory = keys["Inventory"].ToString();
        positionPlayer.KeyMap = keys["Map"].ToString();
        positionPlayer.KeyJournal = keys["Journal"].ToString();
        positionPlayer.KeySkills = keys["Skills"].ToString();
        positionPlayer.KeyOrganizer = keys["Organizer"].ToString();
        positionPlayer.KeyMenu = keys["Menu"].ToString();
        positionPlayer.KeyLpocket = keys["Lpocket"].ToString();
        positionPlayer.KeyRpocket = keys["Rpocket"].ToString();

        //Создаем новый файл (или перезаписываем старый)
        FileStream fs = new FileStream(Application.dataPath + "/settings.set", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Serialize(fs, positionPlayer);
        fs.Close();
    }
}
