using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;

public class ItemOnScene : MonoBehaviour {
    [SerializeField]
    public int IdItemQuest; //назначить айди предметам на сцене, которые должны задаваться не рандомно.

    //всплывающая панель описания
    private GameObject DescriptionPanel;
    private Transform TextPanel;

    //ссылка на инвентарь
    private Inventory inv;

    UserData userData;

    //характеристики объекта
    public ItemData itemData;

    private void Start()
    {
        //находим дочернюю панель и делаем ее неактивной
        DescriptionPanel = this.gameObject.transform.Find("DescriptionPanel").gameObject;
        DescriptionPanel.SetActive(false);

        itemData = new ItemData();

        //криворукое рандомное заполнение данных
        /*int randNumb = Random.Range(0, 4); //это временное
        System.IO.StreamReader file = new System.IO.StreamReader("ItemsData.txt", System.Text.Encoding.GetEncoding(1251));
        string line;
        int i = 0;
        while (((line = file.ReadLine()) != null) && (i != randNumb * 4))
        {
            i++;      
        }
        bool res = int.TryParse(line, out itemData.id);//записываем id
        line = file.ReadLine();
        itemData.name = line;//записываем имя
        line = file.ReadLine();
        itemData.descriptionItem = line;//описание
        line = file.ReadLine();
        itemData.pathIcon = line;//и путь до иконки

        file.Close();*/
        
        
        int randNumb = IdItemQuest == 0 ? Random.Range(1, 19) : IdItemQuest; //это временное
        //Загружаем из ресурсов наш xml файл
        TextAsset xmlAsset = Resources.Load("ItemsData") as TextAsset;
        // надо получить число элементов в root'овом теге.
        XmlDocument xmlDoc = new XmlDocument();
        if (xmlAsset) xmlDoc.LoadXml(xmlAsset.text);

        XmlNodeList dataList = xmlDoc.GetElementsByTagName("item");
        
        foreach (XmlNode item in dataList) {
            XmlNodeList itemContent = item.ChildNodes;
            bool ThisItem = false;
            foreach (XmlNode itemItens in itemContent) {
                if (itemItens.Name == "id") {
                    if (int.Parse(itemItens.InnerText) == randNumb){ //TODO to int
                        itemData.id = randNumb;
                        ThisItem = true;
                    }
                }
                else if (itemItens.Name == "name" && ThisItem) itemData.name = itemItens.InnerText; 
                else if (itemItens.Name == "descriptionItem" && ThisItem) itemData.descriptionItem = itemItens.InnerText;
                else if (itemItens.Name == "pathIcon" && ThisItem) itemData.pathIcon = itemItens.InnerText;
                else if (itemItens.Name == "categories" && ThisItem) itemData.categories = int.Parse(itemItens.InnerText);
                else if (itemItens.Name == "isstackable" && ThisItem) itemData.isStackable = int.Parse(itemItens.InnerText) == 1? true:false; //был bool.Parse
                else if (itemItens.Name == "weight" && ThisItem) itemData.weight = int.Parse(itemItens.InnerText); 
                else if (itemItens.Name == "RestoringHP" && ThisItem) itemData.RestoringHP = int.Parse(itemItens.InnerText); 
            }
        }

        //найдем всплывающую панель как дочерний объект и поместим на нее текст описания
        TextPanel = DescriptionPanel.transform.GetChild(1);
        TextPanel.GetComponent<UnityEngine.UI.Text>().text = itemData.name + "\n\n" + itemData.descriptionItem;

        //Артем1101: начало дополнения
        //Смена картинки у предмета на сцене
        transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>(itemData.pathIcon);
        //Артем1101: конец дополнения
    }

    //наводим - появляется описание
    void OnMouseOver()
    {
        if(itemData.categories != 6) DescriptionPanel.SetActive(true); //Если это не деньги, то высвечиваем панель описания предмета
    }

    //убираем курсор - описание пропадает
    void OnMouseExit()
    {
        DescriptionPanel.SetActive(false);
    }

    //по клику подбираем предмет в инвентарь и удаляем его со сцены
    public void Interacte()
    {
        userData = GameObject.Find("UserData").GetComponent<UserData>();

        //Если вещь - добавляем в инвентарь
        //Если деньги - добавляем к статам деньги
        if (itemData.categories != 6)
        {
            userData.ggData.stats.Set(Stats.Key.WEIGHT, userData.ggData.stats.Get(Stats.Key.WEIGHT) + itemData.weight);
            userData.ggData.inventory.PutItem(itemData);
        }
        else userData.ggData.stats.Set(Stats.Key.GOLD, userData.ggData.stats.Get(Stats.Key.GOLD) + Random.Range(1, 50));

        //Для квестов
        if(itemData.id == 19)
        {
            //Проверяем, если собрали все 10 яблок
            for (int i = 0; i < userData.ggData.inventory.items.Count; i++)
            {
                if(userData.ggData.inventory.items[i].id == 19)
                {
                    if (userData.ggData.inventory.items[i].Stackable == 10)
                    {
                        userData.ggData.quests.QuestList[0].status = Quest.Status.DONE;
                    }
                }
            }
            GameObject.Find("HP_Bar").GetComponent<ChangeHPBar>().FunctionOnEnable();
        }

        //удаляем объект со сцены
        Destroy(gameObject);
    }


    private void Update()
    {
        if (itemData.categories != 6) OnAlt(); //Если это не деньги, то высвечиваем панель описания предмета
    }

    //подсвечивание всех предметов на земле при нажатии alt
    void OnAlt()
    {
        if (Input.GetKey(KeyCode.LeftAlt))
        {
            DescriptionPanel.SetActive(true);
        }

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            DescriptionPanel.SetActive(false);
        }
    }
}
