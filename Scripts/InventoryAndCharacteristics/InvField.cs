using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InvField : MonoBehaviour {

    UserData userData;
    private List<ItemData> itemList;

    public UnityEngine.UI.Button Cat0;
    public UnityEngine.UI.Button Cat1;
    public UnityEngine.UI.Button Cat2;
    public UnityEngine.UI.Button Cat3;
    public UnityEngine.UI.Button Cat4;
    public UnityEngine.UI.Button Cat5;

    private void Start()
    {
        //userData = GameObject.Find("UserData").GetComponent<UserData>();
    }

    void OnEnable()
    {
        userData = GameObject.Find("UserData").GetComponent<UserData>();
        Everything();
    }

    public void Everything()
    {
        //удаляем старые дочерние объекты для отрисовки актуального списка
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Копируем себе список предметов из инвентаря
        //TODO при закрытии отдавать список обратно, так как в инвентаре могут происходить изменения этого списка
        itemList = userData.ggData.inventory.GetList();

        Cat0.Select();
        Cat0.OnSelect(null); //Первая кнопка в инвентаре нажата автоматически.

        for (int i = 0; i < itemList.Count; i++)
        {
            //по списку создаем префабы для каждого объекта
            GameObject item = Instantiate(Resources.Load("Inv/ItemCell"), transform.position, Quaternion.identity) as GameObject;
            //делаем их дочерними к полю вывода
            item.transform.SetParent(this.transform, false);
            //и отдаем каждому их itemData
            item.GetComponent<ItemOnInv>().itemData = itemList[i];
            //установка функции на кнопку для смены вещей в инвентаре
            //item.transform.GetChild(3).GetComponent<UnityEngine.UI.Button>().onClick.AddListener(delegate { ChangeItem(); });
        }
    }

    public void Weapon()
    {
        //удаляем старые дочерние объекты для отрисовки актуального списка
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Копируем себе список предметов из инвентаря
        //TODO при закрытии отдавать список обратно, так как в инвентаре могут происходить изменения этого списка
        itemList = userData.ggData.inventory.GetList();

        Cat1.Select();
        Cat1.OnSelect(null);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].categories == 1)
            {
                //по списку создаем префабы для каждого объекта
                GameObject item = Instantiate(Resources.Load("Inv/ItemCell"), transform.position, Quaternion.identity) as GameObject;
                //делаем их дочерними к полю вывода
                item.transform.SetParent(this.transform, false);
                //и отдаем каждому их itemData
                item.GetComponent<ItemOnInv>().itemData = itemList[i];
            }
        }
    }

    public void Armor()
    {
        //удаляем старые дочерние объекты для отрисовки актуального списка
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Копируем себе список предметов из инвентаря
        //TODO при закрытии отдавать список обратно, так как в инвентаре могут происходить изменения этого списка
        itemList = userData.ggData.inventory.GetList();

        Cat2.Select();
        Cat2.OnSelect(null);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].categories == 2)
            {
                //по списку создаем префабы для каждого объекта
                GameObject item = Instantiate(Resources.Load("Inv/ItemCell"), transform.position, Quaternion.identity) as GameObject;
                //делаем их дочерними к полю вывода
                item.transform.SetParent(this.transform, false);
                //и отдаем каждому их itemData
                item.GetComponent<ItemOnInv>().itemData = itemList[i];
            }
        }
    }

    public void Food()
    {
        //удаляем старые дочерние объекты для отрисовки актуального списка
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Копируем себе список предметов из инвентаря
        //TODO при закрытии отдавать список обратно, так как в инвентаре могут происходить изменения этого списка
        itemList = userData.ggData.inventory.GetList();

        Cat3.Select();
        Cat3.OnSelect(null);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].categories == 3)
            {
                //по списку создаем префабы для каждого объекта
                GameObject item = Instantiate(Resources.Load("Inv/ItemCell"), transform.position, Quaternion.identity) as GameObject;
                //делаем их дочерними к полю вывода
                item.transform.SetParent(this.transform, false);
                //и отдаем каждому их itemData
                item.GetComponent<ItemOnInv>().itemData = itemList[i];
            }
        }
    }

    public void Other()
    {
        //удаляем старые дочерние объекты для отрисовки актуального списка
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Копируем себе список предметов из инвентаря
        //TODO при закрытии отдавать список обратно, так как в инвентаре могут происходить изменения этого списка
        itemList = userData.ggData.inventory.GetList();

        Cat4.Select();
        Cat4.OnSelect(null);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].categories == 4)
            {
                //по списку создаем префабы для каждого объекта
                GameObject item = Instantiate(Resources.Load("Inv/ItemCell"), transform.position, Quaternion.identity) as GameObject;
                //делаем их дочерними к полю вывода
                item.transform.SetParent(this.transform, false);
                //и отдаем каждому их itemData
                item.GetComponent<ItemOnInv>().itemData = itemList[i];
            }
        }
    }

    public void QuestItem()
    {
        //удаляем старые дочерние объекты для отрисовки актуального списка
        foreach (Transform child in transform) Destroy(child.gameObject);

        //Копируем себе список предметов из инвентаря
        //TODO при закрытии отдавать список обратно, так как в инвентаре могут происходить изменения этого списка
        itemList = userData.ggData.inventory.GetList();

        Cat5.Select();
        Cat5.OnSelect(null);

        for (int i = 0; i < itemList.Count; i++)
        {
            if (itemList[i].categories == 5)
            {
                //по списку создаем префабы для каждого объекта
                GameObject item = Instantiate(Resources.Load("Inv/ItemCell"), transform.position, Quaternion.identity) as GameObject;
                //делаем их дочерними к полю вывода
                item.transform.SetParent(this.transform, false);
                //и отдаем каждому их itemData
                item.GetComponent<ItemOnInv>().itemData = itemList[i];
            }
        }
    }

    /*public void ChangeItem()
    {
        Debug.Log("");
    }*/
}
