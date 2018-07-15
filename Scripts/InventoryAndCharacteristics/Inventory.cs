using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    public List<ItemData> items;

    //конструктор создает пустой список
    public Inventory() {
        items = new List<ItemData>();
    }

    //отдать список предметов
    public List <ItemData> GetList ()
    {
        return items;
    }

    //положить предмет в инвентарь
    public void PutItem(ItemData item)
    {
        if (item == null)
        {
            items.Add(item);
            return;
        }

        //Если вещь уже есть в инвентаре и она стакается, то плюсуем к стеку
        //Если вещи есть, но не стакается - новая вещь
        bool ThisIsNewItem = true; //Если новая вещь - то добавим к инвентарю
        //Проверяем, можно ли стековать новую вещь
        if (item.isStackable) { 
            for (int i = 0; i < items.Count; i++)
            {
                if(item.id == items[i].id)
                {
                    items[i].Stackable++;
                    ThisIsNewItem = false;
                }
            }
        }
        if (ThisIsNewItem) items.Add(item);
    }

}
