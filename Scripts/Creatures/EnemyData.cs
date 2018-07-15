using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : CreatureData
{
    public int countOfLoot;

    public EnemyData()
    {
        countOfLoot = Random.Range(1, 4);
        for (int i = 1; i <= countOfLoot; i++)
        {
            //Debug.Log("lalala");
            //Вызывается ли где-нибудь ItemsDatabase.ReadFile?
            //А то у меня все что возвращает ItemsDatabase.GetItem(4%i) - null
            //inventory.PutItem(ItemsDatabase.GetItem(4%i));
            //Debug.Log("lalala2");
        }
    }

}
