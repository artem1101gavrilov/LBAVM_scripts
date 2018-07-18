using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GGData : CreatureData {

    public QuestsData quests;

    public GGData()
    {
        quests = new QuestsData();
    }

    //сюда можно поместить остальные данные, которые не связаны со сценой и которые присущи игроку
    //Переменная отвечающая за состояние "Это битва?"
    public bool IsBattle;

    //получение урона ГГ от врагов
    public void GetDamage(float damage)
    {
        //так было
        /*
        float currHP = stats.Get(Stats.Key.HP);
        if (currHP > 0)
            stats.Set(Stats.Key.HP, currHP - damage);
        else
            Dying();
        */
        //Проверка на жив ГГ или нет сейчас находится в скрипте PlayerScript.cs
        stats.Set(Stats.Key.HP, stats.Get(Stats.Key.HP) - damage);
    }
    
    public void SetBattle(bool SetIsBattle)
    {
        IsBattle = SetIsBattle;
    }


    private void Dying()
    {

    }
}



