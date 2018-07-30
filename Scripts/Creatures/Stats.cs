using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Stats {

    //к статам теперь обращаться примерно так
    //Set(Stats.Key.ATTACK, 20);


    public enum Key {  LEVEL, GOLD, WEIGHT, MAX_WEIGHT, CURRENT_EXPERIENCE, NEXT_EXPERIENCE,
                       HP, MAX_HP, RESTORING_HP, ENERGY, MAX_ENERGY, RESTORING_ENERGY, EXPENSE_ENERGY, REGEN_ENERGY,
                       SPEED,
                       STRENGTH, AGILITY, ENDURANCE, INTELLECT,
                       DEFENSE, MAGICDEFENSE, ARMOR, MAGICARMOR,
                       RESISTANCETO_POISONS, RESISTANCETO_STUNNING, RESISTANCETO_BLEEDING, RESISTANCETO_MAGIC,
                       ATTACKSPEED, PHYSICALDAMAGE, CRITICALDAMAGE, CHANCECRITICALDAMAGE,
                       ATTACK, DEFENCE                  
    };


    public Dictionary<Key, float> stats;
    public Stats ()
    {
        //хранилище статов - ключ-название и значение (float)
        stats = new Dictionary<Key, float>(); //ВОПРОС: Нужно ли было 20 в скобках?
        InitializationStats();
    }

    public void Set(Key key, float value)
    {
        stats[key] = value;
    }

    public float Get(Key key)
    {
        float result = 0.0f;

        if (stats.ContainsKey(key))
        {
            result = stats[key];
        }

        return result;
    }

    //Инициализация всех параметров 
    private void InitializationStats()
    {
        stats.Add(Key.LEVEL, 1);
        stats.Add(Key.GOLD, 0);
        stats.Add(Key.WEIGHT, 0);
        stats.Add(Key.MAX_WEIGHT, 100);
        stats.Add(Key.CURRENT_EXPERIENCE, 0);
        stats.Add(Key.NEXT_EXPERIENCE, 100);

        stats.Add(Key.STRENGTH, 1);
        stats.Add(Key.AGILITY, 1);
        stats.Add(Key.ENDURANCE, 1);
        stats.Add(Key.INTELLECT, 1);

        stats.Add(Key.DEFENSE, 1);
        stats.Add(Key.MAGICDEFENSE, 1);
        stats.Add(Key.ARMOR, 1);
        stats.Add(Key.MAGICARMOR, 1);

        stats.Add(Key.RESISTANCETO_POISONS, 1);
        stats.Add(Key.RESISTANCETO_STUNNING, 1);
        stats.Add(Key.RESISTANCETO_BLEEDING, 1);
        stats.Add(Key.RESISTANCETO_MAGIC, 1);

        stats.Add(Key.ATTACKSPEED, 1);
        stats.Add(Key.PHYSICALDAMAGE, 1);
        stats.Add(Key.CRITICALDAMAGE, 1);
        stats.Add(Key.CHANCECRITICALDAMAGE, 1);

        stats.Add(Key.ATTACK, 25);
        stats.Add(Key.HP, 100);
        stats.Add(Key.MAX_HP, 100);
        stats.Add(Key.ENERGY, 100);
        stats.Add(Key.MAX_ENERGY, 100);
        stats.Add(Key.RESTORING_ENERGY, 1);
        stats.Add(Key.EXPENSE_ENERGY, 0.2f);
        stats.Add(Key.DEFENCE, 10);
        stats.Add(Key.REGEN_ENERGY, 20);
        stats.Add(Key.SPEED, 1);
    }

    //Инициализуем Статы на начальные значени для новой игры
    public void InitializationStatsNewGame()
    {
        Set(Key.LEVEL, 1);
        Set(Key.GOLD, 0);
        Set(Key.WEIGHT, 0);
        Set(Key.MAX_WEIGHT, 100);
        Set(Key.CURRENT_EXPERIENCE, 0);
        Set(Key.NEXT_EXPERIENCE, 100);

        Set(Key.STRENGTH, 1);
        Set(Key.AGILITY, 1);
        Set(Key.ENDURANCE, 1);
        Set(Key.INTELLECT, 1);

        Set(Key.DEFENSE, 1);
        Set(Key.MAGICDEFENSE, 1);
        Set(Key.ARMOR, 1);
        Set(Key.MAGICARMOR, 1);

        Set(Key.RESISTANCETO_POISONS, 1);
        Set(Key.RESISTANCETO_STUNNING, 1);
        Set(Key.RESISTANCETO_BLEEDING, 1);
        Set(Key.RESISTANCETO_MAGIC, 1);

        Set(Key.ATTACKSPEED, 1);
        Set(Key.PHYSICALDAMAGE, 1);
        Set(Key.CRITICALDAMAGE, 1);
        Set(Key.CHANCECRITICALDAMAGE, 1);

        Set(Key.ATTACK, 25);
        Set(Key.HP, 100);
        Set(Key.MAX_HP, 100);
        Set(Key.ENERGY, 100);
        Set(Key.MAX_ENERGY, 100);
        Set(Key.RESTORING_ENERGY, 1);
        Set(Key.EXPENSE_ENERGY, 0.2f);
        Set(Key.DEFENCE, 10);
        Set(Key.REGEN_ENERGY, 20);
        Set(Key.SPEED, 1);
    }
}
