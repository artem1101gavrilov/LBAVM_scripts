using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TAVERNA : MonoBehaviour {
    //Скрипт создан для того, чтобы на разных столиках люди чекались в разное время
    
    public float playTime; //Через какое время должна начать проигрываться анимация у данного НПС

    private void OnEnable()
    {
        GetComponent<Animator>().speed = 0;
        StartCoroutine(SetAnim());
    }

    IEnumerator SetAnim()
    {
        yield return new WaitForSeconds(playTime); 
        GetComponent<Animator>().speed = 1;
    }
}
