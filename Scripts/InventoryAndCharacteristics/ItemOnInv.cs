using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemOnInv : MonoBehaviour {

    public ItemData itemData;
    public int countItem; //TODO

    //ссылка на панель для вывода данных о предмете
    private Transform DescriptionField;

    private GameObject InvDescription; //нижняя панель

    void Start () {
        InvDescription = GameObject.Find("InventoryAndCharacteristics").transform.GetChild(2).transform.GetChild(10).gameObject;
        //находим панель как дочерний объект
        DescriptionField = transform.GetChild(0);
        //и выводим в нее текст
        DescriptionField.GetComponent<UnityEngine.UI.Text>().text = itemData.name + "\n" + itemData.descriptionItem;

        //Смена картинки у предмета в инвентаре
        transform.GetChild(1).GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>(itemData.pathIcon);

        //Если предметов в стеке больше 1, то указываем это в специальном поле
        if(itemData.Stackable > 1) transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = itemData.Stackable.ToString();
    }

    //наводим - появляется описание
    public void DescriptionEnter()
    {
        InvDescription.transform.GetChild(0).GetComponent<UnityEngine.UI.Text>().text = itemData.name;
        InvDescription.transform.GetChild(1).GetComponent<UnityEngine.UI.Text>().text = itemData.descriptionItem;
        InvDescription.transform.GetChild(2).GetComponent<UnityEngine.UI.Text>().text = itemData.cost.ToString();
        InvDescription.transform.GetChild(3).GetComponent<UnityEngine.UI.Text>().text = itemData.weight.ToString();
        InvDescription.SetActive(true);
    }

    //убираем курсор - описание пропадает
    public void DescriptionExit()
    {
        InvDescription.SetActive(false);
    }

    //Смена вещей в инвентаре
    public void ChangeItem()
    {
        Debug.Log(itemData.id + " | " + itemData.categories);
        if (itemData.categories == 1)
        {
            GameObject.Find("UserData").GetComponent<UserData>().ggData.inventory.items.Remove(itemData);
            GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().RemoveWeapon();
            GameObject.Find("UserData").GetComponent<UserData>().weapon = itemData;
            GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().UpdateCharacteristics();
            GameObject.Find("InventoryAndCharacteristics").transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
        }

        if (itemData.categories == 3 || itemData.categories == 4)
        {
            if (GameObject.Find("UserData").GetComponent<UserData>().Lpocket.id == -1) {
                if (itemData.Stackable < 6)
                {
                    GameObject.Find("UserData").GetComponent<UserData>().ggData.inventory.items.Remove(itemData);
                    //GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().RemoveLpocket();//Никогда не нужен будет
                    GameObject.Find("UserData").GetComponent<UserData>().Lpocket.SetParametrs(itemData);
                    GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().UpdateCharacteristics();
                    GameObject.Find("InventoryAndCharacteristics").transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
                }
                else
                {
                    itemData.Stackable -= 5;
                    GameObject.Find("UserData").GetComponent<UserData>().Lpocket.SetParametrs(itemData);
                    GameObject.Find("UserData").GetComponent<UserData>().Lpocket.SetStackable(5);
                    GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().UpdateCharacteristics();
                    GameObject.Find("InventoryAndCharacteristics").transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
                }
            }
            else
            {
                if (itemData.Stackable < 6)
                {
                    GameObject.Find("UserData").GetComponent<UserData>().ggData.inventory.items.Remove(itemData);
                    GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().RemoveRpocket();
                    GameObject.Find("UserData").GetComponent<UserData>().Rpocket.SetParametrs(itemData);
                    GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().UpdateCharacteristics();
                    GameObject.Find("InventoryAndCharacteristics").transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
                }
                else
                {
                    itemData.Stackable -= 5;
                    GameObject.Find("UserData").GetComponent<UserData>().Rpocket.SetParametrs(itemData);
                    GameObject.Find("UserData").GetComponent<UserData>().Rpocket.SetStackable(5);
                    GameObject.Find("InventoryAndCharacteristics").GetComponent<CharacteristicsScript>().UpdateCharacteristics();
                    GameObject.Find("InventoryAndCharacteristics").transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
                }
            }
        }
    }
}
