using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;


//скрипт будет делать хз что вообще, я запуталась, нафиг он нужен, в нем нет нихрена и одновременно есть всё и все его дергают
//пусть живет, в общем

public class UserData : MonoBehaviour
{
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
        ItemsDatabase.ReadFile();

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
        if(Application.loadedLevel == 0) SceneManager.LoadScene(1);
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
}
