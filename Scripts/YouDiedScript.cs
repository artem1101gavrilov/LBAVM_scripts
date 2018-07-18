using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class YouDiedScript : MonoBehaviour {

    public  GameObject LButton; // кнопка вернуться в главное меню
    public  GameObject RButton; // Кнопка загрузить последнее сохранение

    public void SetActiveAllButtons()
    {
        LButton.SetActive(true);
        //TODO: Здесь нужна проверка на то, что если нет последнего сохранения, то или не отображать кнопку или делать ее темной
        RButton.SetActive(true);
    }
}
