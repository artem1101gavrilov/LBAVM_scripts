using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;


//скрипт будет делать хз что вообще, я запуталась, нафиг он нужен, в нем нет нихрена и одновременно есть всё и все его дергают
//пусть живет, в общем

public class UserData : MonoBehaviour
{
    public int SceneID;

    //ГГДата наследуется от CreatureData (поэтому имеет инвентарь и статы) и добавляет от себя квесты (в скриптах квестов поменяла обращение, чтобы работало)
    //поэтому к инвентарю, квестам и статам ГГ обращаемся через userData.ggData.нужныйАттрибут()
    public GGData ggData;
    public SettingsData settings;

    //Не знаю где это должно храниться. 
    //Все вещи на ерсонаже
    public ItemData Lpocket;
    public ItemData Rpocket;
    public ItemData accessory;
    public ItemData corps;
    public ItemData weapon;
    public ItemData arms;
    public ItemData legs;

    public Vector3 positionTilTrandent;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        ggData = new GGData();
        settings = new SettingsData();
        //ItemsDatabase.ReadFile();

        //тестовое TODO
        ggData.stats.Set(Stats.Key.HP, 100);
        ggData.stats.Set(Stats.Key.ATTACK, 20);
        //ggData.quests.activeQuestID = 5;

        ggData.IsBattle = false;

        Lpocket = new ItemData();
        Lpocket.id = -1;
        Rpocket = new ItemData();
        Rpocket.id = -1;
        accessory = new ItemData();
        accessory.id = -1;
        corps = new ItemData();
        corps.id = -1;
        weapon = new ItemData();
        arms = new ItemData();
        arms.id = -1;
        legs = new ItemData();
        legs.id = -1;

        ReadXMLforLoadWeapon();

        positionTilTrandent = new Vector3(-12.58f, -17.63f, -1f);
        AudioListener.volume = settings.totalVolume;
        if (Application.loadedLevel == 0) SceneManager.LoadScene(1);
    }

    //Тестовая функция для того, чтобы на нашем ГГ был меч в руках
    private void ReadXMLforLoadWeapon()
    {
        int randNumb = 17; //номер id  в xml файле
        //Загружаем из ресурсов наш xml файл
        TextAsset xmlAsset = Resources.Load("ItemsData") as TextAsset;
        // надо получить число элементов в root'овом теге.
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset) xmlDoc.LoadXml(xmlAsset.text);

        XmlNodeList dataList = xmlDoc.GetElementsByTagName("item");

        foreach (XmlNode item in dataList)
        {
            XmlNodeList itemContent = item.ChildNodes;
            bool ThisItem = false;
            foreach (XmlNode itemItens in itemContent)
            {
                if (itemItens.Name == "id")
                {
                    if (int.Parse(itemItens.InnerText) == randNumb)
                    { //TODO to int
                        weapon.id = randNumb;
                        ThisItem = true;
                    }
                }
                else if (itemItens.Name == "name" && ThisItem) weapon.name = itemItens.InnerText;
                else if (itemItens.Name == "descriptionItem" && ThisItem) weapon.descriptionItem = itemItens.InnerText;
                else if (itemItens.Name == "pathIcon" && ThisItem) weapon.pathIcon = itemItens.InnerText;
                else if (itemItens.Name == "categories" && ThisItem) weapon.categories = int.Parse(itemItens.InnerText);
                else if (itemItens.Name == "cost" && ThisItem) weapon.cost = int.Parse(itemItens.InnerText);
                else if (itemItens.Name == "weight" && ThisItem) weapon.weight = int.Parse(itemItens.InnerText);
                else if (itemItens.Name == "isstackable") weapon.isStackable = int.Parse(itemItens.InnerText) == 1 ? true : false;
            }
        }
        ggData.stats.Set(Stats.Key.WEIGHT, weapon.weight);
    }

    public void SaveGame()
    {
        //Файл сохранения будет содержать следующие параметры
        //* Номер сцены
        //* Трансформ положение персонажа
        //* Все статистические параметры
        //* Инвентарь и то что на персонаже надето
        //* Квесты (id , Status) (на данный момент только второстепенные парметры (активный, выполне, не получен))

        SceneID = Application.loadedLevel;

        //Если папки для сохранения нет, то создаем новую папку
        if (!Directory.Exists(Application.dataPath + "/saves")) Directory.CreateDirectory(Application.dataPath + "/saves");
        //Создаем новый файл (или перезаписываем старый)
        FileStream fs = new FileStream(Application.dataPath + "/saves/save1.sv", FileMode.Create);
        BinaryFormatter bformatter = new BinaryFormatter();
        SerializeUserData data = new SerializeUserData(this);
        bformatter.Serialize(fs, data);
        fs.Close(); 
    }

    public void LoadGame()
    {
        if (File.Exists(Application.dataPath + "/saves/save1.sv"))
        {
            FileStream fs = new FileStream(Application.dataPath + "/saves/save1.sv", FileMode.Open);
            BinaryFormatter bformatter = new BinaryFormatter();
            try
            {
                SerializeUserData data = (SerializeUserData)bformatter.Deserialize(fs);
                SceneID = data.SceneID;
                positionTilTrandent.x = data.positionTilTrandentX;
                positionTilTrandent.y = data.positionTilTrandentY;
                ggData.quests.QuestList = data.QuestList;
                ggData.inventory.items = data.items;
                ggData.stats.stats = data.stats;
                Lpocket = data.Lpocket;
                Rpocket = data.Rpocket;
                accessory = data.accessory;
                corps = data.corps;
                weapon = data.weapon;
                arms = data.arms;
                legs = data.legs;
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
    }

}

[Serializable]
public class SerializeUserData
{
    public int SceneID;
    public float positionTilTrandentX;
    public float positionTilTrandentY;

    public Dictionary<Stats.Key, float> stats;
    public List<ItemData> items;
    public List<Quest> QuestList;

    public ItemData Lpocket;
    public ItemData Rpocket;
    public ItemData accessory;
    public ItemData corps;
    public ItemData weapon;
    public ItemData arms;
    public ItemData legs;

    public SerializeUserData(UserData userData)
    {
        SceneID = userData.SceneID;
        positionTilTrandentX = userData.positionTilTrandent.x;
        positionTilTrandentY = userData.positionTilTrandent.y;
        stats = userData.ggData.stats.stats;
        items = userData.ggData.inventory.items;
        QuestList = userData.ggData.quests.QuestList;
        Lpocket = userData.Lpocket;
        Rpocket = userData.Rpocket;
        accessory = userData.accessory;
        corps = userData.corps;
        weapon = userData.weapon;
        arms = userData.arms;
        legs = userData.legs;
    }
}