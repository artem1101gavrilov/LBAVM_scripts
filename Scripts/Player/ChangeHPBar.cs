using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeHPBar : MonoBehaviour {
    public Canvas thisHPBar;
    public GameObject PanelHPBar;

    private Image HPbar; //Полоска ХП ГГ
    private Image ENERGYbar; //Полоска ЭНЕРГИИ ГГ
    private Image Expbar; //Полоска Опыта ГГ
    private UserData userData;

    float currentHP;
    float currentEnergy;
    float maxHP;
    float maxEnergy;


    void Start () {

        userData = GameObject.Find("UserData").GetComponent<UserData>();
        FunctionOnEnable();
        HPbar = gameObject.transform.GetChild(0).GetChild(1).GetComponent<Image>();
        ENERGYbar = gameObject.transform.GetChild(0).GetChild(2).GetComponent<Image>();
        Expbar = gameObject.transform.GetChild(0).GetChild(3).GetComponent<Image>();


        maxHP = userData.ggData.stats.Get(Stats.Key.MAX_HP);
        maxEnergy = userData.ggData.stats.Get(Stats.Key.MAX_ENERGY);

        /*
         PanelHPBar.GetComponent<RectTransform>().sizeDelta = new Vector2(100,60);
         PanelHPBar.GetComponent<RectTransform>().localScale = new Vector2(thisHPBar.GetComponent<RectTransform>().rect.width/1008F, thisHPBar.GetComponent<RectTransform>().rect.height / 458F);
         PanelHPBar.GetComponent<RectTransform>().transform.position = new Vector3(PanelHPBar.GetComponent<RectTransform>().transform.position.x + PanelHPBar.GetComponent<RectTransform>().localScale.x * 250F/2, // - 125F, 
                                                                                   PanelHPBar.GetComponent<RectTransform>().transform.position.y - PanelHPBar.GetComponent<RectTransform>().localScale.y * 135/2, // + 67.5F, 
                                                                                   PanelHPBar.GetComponent<RectTransform>().transform.position.z);
                                                                                   */
    }



    void Update()
    {
        currentHP = userData.ggData.stats.Get(Stats.Key.HP);
        currentEnergy = userData.ggData.stats.Get(Stats.Key.ENERGY);
        //TODO (рефакторинг): надо перенести в две разные функции и вызывать тогда, когда происходит их изменение.
        HPbar.fillAmount = currentHP / maxHP;
        ENERGYbar.fillAmount = currentEnergy / maxEnergy;
        Expbar.fillAmount = userData.ggData.stats.Get(Stats.Key.CURRENT_EXPERIENCE) / userData.ggData.stats.Get(Stats.Key.NEXT_EXPERIENCE);
    }

    void OnEnable()
    {
        FunctionOnEnable();
    }

    //Потому что видимо OnEnable() вызывается раньше всех стартов и эвейков
    public void FunctionOnEnable()
    {
        if (userData != null)
        {
            if (userData.Lpocket.id != -1)
            {
                transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>(userData.Lpocket.pathIcon);
                transform.GetChild(1).GetChild(2).GetComponent<Text>().text = userData.Lpocket.Stackable + "/5 " + userData.Lpocket.name;
            }
            else
            {
                transform.GetChild(1).GetChild(1).GetComponent<Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");
                transform.GetChild(1).GetChild(2).GetComponent<Text>().text = "";
            }

            if (userData.Rpocket.id != -1)
            {
                transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>(userData.Rpocket.pathIcon);
                transform.GetChild(1).GetChild(4).GetComponent<Text>().text = userData.Rpocket.Stackable + "/5 " + userData.Rpocket.name;
            }
            else
            {
                transform.GetChild(1).GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Inv/blank_picture");
                transform.GetChild(1).GetChild(4).GetComponent<Text>().text = "";
            }

            if (userData.ggData.quests.currentQuestID > 0)
            {
                if (userData.ggData.quests.QuestList[userData.ggData.quests.currentQuestID - 1].status == Quest.Status.ACTIVE)
                {
                    transform.GetChild(3).GetChild(1).GetComponent<Text>().text = userData.ggData.quests.QuestList[userData.ggData.quests.currentQuestID - 1].title;
                    if (userData.ggData.quests.currentQuestID == 1)
                    {
                        /*int countForQuest = 0; // сколько яблок собрано в квесте
                        for (int i = 0; i < userData.ggData.inventory.items.Count; i++)
                        {
                            if (userData.ggData.inventory.items[i].id == 19)
                            {
                                countForQuest = userData.ggData.inventory.items[i].Stackable;
                            }
                        }*/
                        transform.GetChild(3).GetChild(3).GetComponent<Text>().text = userData.ggData.quests.QuestList[0].CurrentNumber.ToString();
                    }
                    else if (userData.ggData.quests.currentQuestID == 2)
                    {
                        transform.GetChild(3).GetChild(3).GetComponent<Text>().text = userData.ggData.quests.QuestList[1].CurrentNumber.ToString();
                    }
                    transform.GetChild(3).GetChild(3).GetComponent<Text>().text += userData.ggData.quests.QuestList[userData.ggData.quests.currentQuestID - 1].toDo;
                }
                else
                {
                    transform.GetChild(3).GetChild(1).GetComponent<Text>().text = userData.ggData.quests.QuestList[userData.ggData.quests.currentQuestID - 1].title;
                    transform.GetChild(3).GetChild(3).GetComponent<Text>().text = "Выполнено!";
                }
            }
        }
    }
}
