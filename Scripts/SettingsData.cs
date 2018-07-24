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

    //public KeyCode KeyUp;

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
                //KeyUp = (KeyCode)System.Enum.Parse(typeof(KeyCode), positionPlayer.KeyUp); 
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
            //KeyUp = KeyCode.W;

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

        //Создаем новый файл (или перезаписываем старый)
        FileStream fs = new FileStream(Application.dataPath + "/settings.set", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        bformatter.Serialize(fs, positionPlayer);
        fs.Close();
    }
}
