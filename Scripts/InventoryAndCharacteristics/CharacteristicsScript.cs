using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CharacteristicsScript : MonoBehaviour {

    private UserData userData;

    public PlayerScript player; //Объект ГГ для обновления характеристик

	public GameObject Inventory;
	public GameObject Characteristics;

    public Image EXP;

    private GameObject InvDescription; //нижняя панель

    //Верхняя полоска характеристик
    public Text TextGold;
    public Text TextWeight;
    public Text TextLevel;
    public Text TextExperience;

    //Все поля характеристик
    //Характеристики
    //Основные параметры
    public Text Textstrength; // Сила ГГ
    public Text Textagility; //Ловкость
    public Text Textendurance; //Выносливость
    public Text Textintellect; //Интеллект 
    //Дополнительные параметры
    public Text TextmaxHealth;
    public Text TextrestoringHealth;
    public Text TextmaxEnergy;
    public Text TextrestoringEnergy;
    public Text Textdefense;      //Защита
    public Text Textmagicdefense; //Магическая Защита
    public Text Textarmor;        //Броня
    public Text Textmagicarmor;   //Магическая броня
    //Сопротивляемость
    public Text TextresistanceToPoisons; //сопротивляемость к ядам
    public Text TextresistanceToStunning; // сопротивляемость к оглушению
    public Text TextresistanceToBleeding; //сопротивляемость к кровотечению 
    public Text TextresistanceToMagic; //сопротивляемость к магии

    public Text Texttravelspeed;
    public Text TextattackSpeed; //скорость атаки
    public Text TextphysicalDamage; // физический урон 
    public Text TextcriticalDamage; // критический урон 
    public Text TextchanceCriticalDamage; //шанс критический урон 

    private void Start()
    {
        InvDescription = gameObject.transform.GetChild(2).transform.GetChild(10).gameObject;
        userData = GameObject.Find("UserData").GetComponent<UserData>();
        UpdateCharacteristics();
    }

    public void UpdateCharacteristics() {
        TextGold.text = userData.ggData.stats.Get(Stats.Key.GOLD).ToString();     //Текущее бабло
        TextWeight.text = userData.ggData.stats.Get(Stats.Key.WEIGHT).ToString() + "/" + userData.ggData.stats.Get(Stats.Key.MAX_WEIGHT).ToString();       //А сколько сможешь поднять ты!?
        TextLevel.text = userData.ggData.stats.Get(Stats.Key.LEVEL).ToString();
        TextExperience.text = userData.ggData.stats.Get(Stats.Key.CURRENT_EXPERIENCE).ToString() + "/" + userData.ggData.stats.Get(Stats.Key.NEXT_EXPERIENCE).ToString();
        //Характеристики
        //Основные параметры
        Textstrength.text = userData.ggData.stats.Get(Stats.Key.STRENGTH).ToString(); // Сила ГГ
        Textagility.text = userData.ggData.stats.Get(Stats.Key.AGILITY).ToString(); //Ловкость
        Textendurance.text = userData.ggData.stats.Get(Stats.Key.ENDURANCE).ToString(); //Выносливость
        Textintellect.text = userData.ggData.stats.Get(Stats.Key.INTELLECT).ToString(); //Интеллект 
                                                     //Дополнительные параметры
        TextmaxHealth.text = userData.ggData.stats.Get(Stats.Key.MAX_HP).ToString();
        TextrestoringHealth.text = userData.ggData.stats.Get(Stats.Key.RESTORING_HP).ToString();
        TextmaxEnergy.text = userData.ggData.stats.Get(Stats.Key.MAX_ENERGY).ToString();
        TextrestoringEnergy.text = userData.ggData.stats.Get(Stats.Key.RESTORING_ENERGY).ToString();
        Textdefense.text = userData.ggData.stats.Get(Stats.Key.DEFENCE).ToString();      //Защита
        Textmagicdefense.text = userData.ggData.stats.Get(Stats.Key.MAGICDEFENSE).ToString(); //Магическая Защита
        Textarmor.text = userData.ggData.stats.Get(Stats.Key.ARMOR).ToString();        //Броня
        Textmagicarmor.text = userData.ggData.stats.Get(Stats.Key.MAGICARMOR).ToString();   //Магическая броня
                                                         //Сопротивляемость
        TextresistanceToPoisons.text = userData.ggData.stats.Get(Stats.Key.RESISTANCETO_POISONS).ToString(); //сопротивляемость к ядам
        TextresistanceToStunning.text = userData.ggData.stats.Get(Stats.Key.RESISTANCETO_STUNNING).ToString(); // сопротивляемость к оглушению
        TextresistanceToBleeding.text = userData.ggData.stats.Get(Stats.Key.RESISTANCETO_BLEEDING).ToString(); //сопротивляемость к кровотечению 
        TextresistanceToMagic.text = userData.ggData.stats.Get(Stats.Key.RESISTANCETO_MAGIC).ToString(); //сопротивляемость к магии

        Texttravelspeed.text = userData.ggData.stats.Get(Stats.Key.SPEED).ToString();
        TextattackSpeed.text = userData.ggData.stats.Get(Stats.Key.ATTACKSPEED).ToString(); //скорость атаки
        TextphysicalDamage.text = userData.ggData.stats.Get(Stats.Key.PHYSICALDAMAGE).ToString(); // физический урон 
        TextcriticalDamage.text = userData.ggData.stats.Get(Stats.Key.CRITICALDAMAGE).ToString(); // критический урон 
        TextchanceCriticalDamage.text = userData.ggData.stats.Get(Stats.Key.CHANCECRITICALDAMAGE).ToString(); //шанс критический урон

        EXP.fillAmount = userData.ggData.stats.Get(Stats.Key.CURRENT_EXPERIENCE) / userData.ggData.stats.Get(Stats.Key.NEXT_EXPERIENCE);

        //Смена картинок
        //Корпус
        if (userData.corps.id != -1) gameObject.transform.GetChild(4).transform.GetChild(3).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.corps.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(3).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

        //Оружие
        if (userData.weapon.id != -1) gameObject.transform.GetChild(4).transform.GetChild(7).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.weapon.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(7).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

        //Ноги
        if (userData.legs.id != -1) gameObject.transform.GetChild(4).transform.GetChild(11).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.legs.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(11).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

        //Руки
        if (userData.arms.id != -1) gameObject.transform.GetChild(4).transform.GetChild(15).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.arms.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(15).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

        //Аксессуар
        if (userData.accessory.id != -1) gameObject.transform.GetChild(4).transform.GetChild(19).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.accessory.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(19).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

        //Карманы
        //Левый
        if (userData.Lpocket.id != -1) gameObject.transform.GetChild(4).transform.GetChild(23).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.Lpocket.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(23).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

        //Правый
        if (userData.Rpocket.id != -1) gameObject.transform.GetChild(4).transform.GetChild(26).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(userData.Rpocket.pathIcon);
        else gameObject.transform.GetChild(4).transform.GetChild(26).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");

    }

    void OnEnable()
    {
        userData = GameObject.Find("UserData").GetComponent<UserData>();
        UpdateCharacteristics();
    }


    public void GoToCharacteristics(){
		Inventory.SetActive(false);
        Characteristics.SetActive(true);
	}
	
	public void GoToInventory(){
        Characteristics.SetActive(false);
        Inventory.SetActive(true);
	}

    /*
     Lpocket = new ItemData();
        Rpocket = new ItemData();
        accessory = new ItemData();
        corps = new ItemData();
        weapon = new ItemData();
        arms = new ItemData();
        legs 
     */

    //Функции убирания вещей с ГГ и перенос их в инвентарь
    public void RemoveLpocket()
    {
        if (userData.Lpocket.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.Lpocket);
            //проверяем, если уже в инвентаре эти вещи, чтобы добавить в стак
            bool isItemInInventory = false;
            for (int i = 0; i < userData.ggData.inventory.items.Count; i++)
            {
                if (userData.ggData.inventory.items[i].id == newItem.id)
                {
                    userData.ggData.inventory.items[i].Stackable += newItem.Stackable;
                    isItemInInventory = true;
                }
            }
            if (!isItemInInventory) userData.ggData.inventory.items.Add(newItem);
            userData.Lpocket.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Food();
        }
    }

    public void RemoveRpocket()
    {
        if (userData.Rpocket.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.Rpocket);
            //проверяем, если уже в инвентаре эти вещи, чтобы добавить в стак
            bool isItemInInventory = false;
            for(int i = 0; i < userData.ggData.inventory.items.Count; i++)
            {
                if(userData.ggData.inventory.items[i].id == newItem.id)
                {
                    userData.ggData.inventory.items[i].Stackable += newItem.Stackable;
                    isItemInInventory = true;
                }
            }
            if(!isItemInInventory) userData.ggData.inventory.items.Add(newItem);
            userData.Rpocket.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Food();
        }
    }

    public void RemoveAccessory()
    {
        if (userData.accessory.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.accessory);
            userData.ggData.inventory.items.Add(newItem);
            userData.accessory.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Armor();
        }
    }

    public void RemoveCorps()
    {
        if (userData.corps.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.corps);
            userData.ggData.inventory.items.Add(newItem);
            userData.corps.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Armor();
        }
    }

    public void RemoveWeapon()
    {
        if(userData.weapon.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.weapon);
            userData.ggData.inventory.items.Add(newItem);
            userData.weapon.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Weapon();
        }
    }

    public void RemoveArms()
    {
        if (userData.arms.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.arms);
            userData.ggData.inventory.items.Add(newItem);
            userData.arms.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Armor();
        }
    }
    public void RemoveLegs()
    {
        if (userData.legs.id != -1)
        {
            ItemData newItem = new ItemData();
            newItem.SetParametrs(userData.legs);
            userData.ggData.inventory.items.Add(newItem);
            userData.legs.id = -1; //Проблемная зона, эта смена ведет за сменой айди в инвентаре
            UpdateCharacteristics();
            transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Armor();
        }
    }

    //Функции для отображения вещей на панеле слева внизу
    public void DescriptionEnterLpocket()
    {
        if (userData.Lpocket.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.Lpocket.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.Lpocket.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.Lpocket.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.Lpocket.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    public void DescriptionEnterRpocket()
    {
        if (userData.Rpocket.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.Rpocket.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.Rpocket.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.Rpocket.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.Rpocket.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    public void DescriptionEnterAccessory()
    {
        if (userData.accessory.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.accessory.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.accessory.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.accessory.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.accessory.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    public void DescriptionEnterCorps()
    {
        if (userData.corps.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.corps.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.corps.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.corps.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.corps.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    public void DescriptionEnterWeapon()
    {
        if (userData.weapon.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.weapon.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.weapon.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.weapon.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.weapon.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    public void DescriptionEnterArms()
    {
        if (userData.arms.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.arms.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.arms.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.arms.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.arms.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    public void DescriptionEnterLegs()
    {
        if (userData.legs.id != -1)
        {
            InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = userData.legs.name;
            InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = userData.legs.descriptionItem;
            InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = userData.legs.cost.ToString();
            InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = userData.legs.weight.ToString();
            InvDescription.SetActive(true);
        }
    }

    //убираем курсор - описание пропадает
    public void DescriptionExit()
    {
        InvDescription.SetActive(false);
    }
}
