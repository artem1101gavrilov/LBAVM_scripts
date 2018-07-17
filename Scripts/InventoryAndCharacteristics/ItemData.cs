using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TypeOfItem { Everything, Weapon, Armor, Food, Other, QuestItems };

public class ItemData {
    public int id;
    public string name;
    public bool isStackable;
    public int Stackable; //сколько в стеке
    [Multiline(5)]
    public string descriptionItem;
    public string pathIcon;
    public int categories; //К какой категории будет относиться во вкладках в инвентаре
    public int cost; //стоимость предмета
    public int weight; //вес предмета
    public int RestoringHP; //предметы с восстановление жизни

    public ItemData()
    {
        id = -1; //если нет id значит нет предмета, нужно для отображения в ячейках инвентаря.
        isStackable = false;
        Stackable = 1;
        RestoringHP = 0;
    }

    public void SetParametrs(ItemData newItem)
    {
        this.id = newItem.id;
        this.name = newItem.name;
        this.isStackable = newItem.isStackable;
        this.Stackable = newItem.Stackable;
        this.descriptionItem = newItem.descriptionItem;
        this.pathIcon = newItem.pathIcon;
        this.categories = newItem.categories;
        this.cost = newItem.cost;
        this.weight = newItem.weight;
        this.RestoringHP = newItem.RestoringHP;
    }

    public void SetStackable(int NewStackable)
    {
        Stackable = NewStackable;
    }
}
