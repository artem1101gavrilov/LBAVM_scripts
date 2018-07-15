using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest{

    public enum Status { NOT_RECEIVED, ACTIVE, DONE };

    public Status status;
    public int id; //Номер квеста
    public string name; //Текстовое поле для кнопки
    public string title; // Оглавление Квеста
    public string description; // Описание квеста
    public string toDo; // что надо сделать в квесте
    
    //public bool executable; // Какой квест выполняется сейчас. Нужно для отображение на главном канвасе и чтобы в листе указывался указатель на него.
}
