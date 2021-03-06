﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

    private EnemyData enemyData;
    private UserData userData;

    private Animator animator; //работа с анимацией

    //float stayTime = 1;

    private bool IsDeath;

    //private Rigidbody2D rigidbody;
    private SpriteRenderer sprite;
    private Vector3 direction;
    private bool isGGNear;

    private Vector3[] target;
    private Vector3 currTarget;
    private bool isDamage;
    private float timer;
    private Vector3 GGPos;
    GameObject itemPrefab;
    
    private UnityEngine.UI.Image HPBar;

    private EnemyState State //Установка и получение состояния (анимации)
    {
        get
        {
            return (EnemyState)animator.GetInteger("StateEnemy");
        }
        set
        {
            animator.SetInteger("StateEnemy", (int)value);
        }
    }

    private void Awake()
    {
        itemPrefab = Resources.Load("Inv/ItemOnScene") as GameObject;

        IsDeath = false;

        animator = GetComponent<Animator>();
        //получаем всякие ссылки
        //rigidbody = GetComponent<Rigidbody2D>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        userData = GameObject.Find("UserData").GetComponent<UserData>();

        //получаем ссылку на панельку для тестового вывода хп
        HPBar = gameObject.transform.GetChild(0).GetChild(1).GetComponent<UnityEngine.UI.Image>();

        //создаем экземпляр данных
        enemyData = new EnemyData();

        //создаем массив точек для гуляния пока не видно ГГ
        target = new Vector3[5];
        //и радиус хождения
        float rad = 2;
        //заполняем массив точками на окружности
        for (int i = 0; i < 5; i++)
        {
            //магическая математика
            double rand = Random.Range(0, 2 * (float)System.Math.PI);
            target[i] = new Vector3((transform.position.x + rad * (float)System.Math.Sin(rand)), (transform.position.y + rad * (float)System.Math.Cos(rand)), -2);
        }
        //currTarget = target[0];
        GetNextPoint();
    }


    void Update () {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").gameObject.transform.position) < 10.0f)
        {
            //вывод текущего хп на панельку, панелька тестовая
            HPBar.fillAmount = enemyData.stats.Get(Stats.Key.HP) / enemyData.stats.Get(Stats.Key.MAX_HP);

            if (!IsDeath)
            {
                // проверяем, достаточно ли близко гг, если да - идти к нему и атаковать, если нет - прогуливаться
                if (isGGNear == true)
                {
                    //Dying();
                    GoToAttack();
                }
                else
                {
                    Walking();
                }
            }
            else
            {
                timer += 1 * Time.deltaTime;
                if (timer > 2)
                {
                    Destroy(gameObject, 0);
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -2 + transform.position.y / 1000);
        }
    }

    private void GoToAttack()
    {
        //если близко к ГГ, то бьем его
        Vector3 dir = GGPos - transform.position;
        float x = GGPos.x - transform.position.x;
        float b = Mathf.Atan2((GGPos.y - transform.position.y), x);
        b = b * 180 / 3.14f;
        if (Vector3.Distance(transform.position, GGPos) <= 0.6f)
        {
            //Для смены анимации у НПС. Считаем угол от НПС до следующей точки
            //Подумать еще с условиями (где-то лишние больше или равно)
            if (b >= -45 && b <= 45)
            {
                sprite.flipX = false;
                if(State != EnemyState.EnemyStateAttackRight) State = EnemyState.EnemyStateAttackRight;

            }
            else if (b >= 45 && b <= 135)
            {
                if (State != EnemyState.EnemyStateAttackBack) State = EnemyState.EnemyStateAttackBack;
            }
            else if (b >= 135 || b <= -135)
            {
                sprite.flipX = false;
                if (State != EnemyState.EnemyStateAttackLeft) State = EnemyState.EnemyStateAttackLeft;
            }
            else if (b >= -135 && b <= -45)
            {
                if (State != EnemyState.EnemyStateAttackFront) State = EnemyState.EnemyStateAttackFront;
            }
            Attack();
        }
        //если далеко - идем к нему
        else
        {
            //Для смены анимации у НПС. Считаем угол от НПС до следующей точки
            //Подумать еще с условиями (где-то лишние больше или равно)
            if (b >= -45 && b <= 45)
            {
                sprite.flipX = false;
                if (State != EnemyState.EnemyStateRight) State = EnemyState.EnemyStateRight;
            }
            else if (b >= 45 && b <= 135)
            {
                if (State != EnemyState.EnemyStateBack) State = EnemyState.EnemyStateBack;
            }
            else if (b >= 135 || b <= -135)
            {
                sprite.flipX = true;
                if (State != EnemyState.EnemyStateRight) State = EnemyState.EnemyStateRight;
            }
            else if (b >= -135 && b <= -45)
            {
                if (State != EnemyState.EnemyStateFront) State = EnemyState.EnemyStateFront;
            }
            transform.Translate(dir.normalized * enemyData.stats.Get(Stats.Key.SPEED) * Time.deltaTime/2, Space.World);
        }

    }

    private void Attack()
    {
        if (isDamage == true)
        {
            //магические таймеры позволяют делать паузы между ударами
            timer += 1 * Time.deltaTime;
            if (timer >= 1)
            {
                userData.ggData.GetDamage(enemyData.stats.Get(Stats.Key.ATTACK));
                timer = 0;
            }
        }
    }

    private void Walking()
    {
        //просто движемся до выбранной точки, если дошли - меняем точку
        Vector3 dir = currTarget - transform.position;
        float x = currTarget.x - transform.position.x;
        float b = Mathf.Atan2((currTarget.y - transform.position.y), x);
        b = b * 180 / 3.14f;

        if (b >= -45 && b <= 45)
        {
            State = EnemyState.EnemyStateRight;
            sprite.flipX = false;
        }
        else if (b >= 45 && b <= 135)
        {
             State = EnemyState.EnemyStateBack;
        }
        else if (b >= 135 || b <= -135)
        {
            State = EnemyState.EnemyStateRight;
            sprite.flipX = true;
        }
        else if (b >= -135 && b <= -45)
        {
            State = EnemyState.EnemyStateFront;
        }
        transform.Translate(dir.normalized * enemyData.stats.Get(Stats.Key.SPEED) * Time.deltaTime / 2, Space.World);
        if (Vector3.Distance(transform.position, currTarget) <= 0.3f)
        {
            GetNextPoint();
        }
    }

    private void GetNextPoint()
    {
        //выбираем новую точку, до которой будем двигаться
        currTarget = target[Random.Range(0, 5)];

        //Для смены анимации у НПС. Считаем угол от НПС до следующей точки
        //Подумать еще с условиями (где-то лишние больше или равно)
        float x = currTarget.x - transform.position.x;
        float b = Mathf.Atan2((GGPos.y - transform.position.y), x);
        if (b >= -45 && b <= 45)
        {
            State = EnemyState.EnemyStateRight;
            sprite.flipX = false;
        }
        else if (b >= 45 && b <= 135)
        {
            State = EnemyState.EnemyStateBack;
        }
        else if (b >= 135 || b <= -135)
        {
            State = EnemyState.EnemyStateRight;
            sprite.flipX = true;
        }
        else if (b >= -135 && b <= -45)
        {
            State = EnemyState.EnemyStateFront;
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "GG")
        {
            isGGNear = true;
            isDamage = true;
            GGPos = col.GetComponentInParent<Transform>().position;
            //Если видим нашего ГГ, значит и он нас видит. Пора поднимать свои мечи.
            userData.ggData.SetBattle(true);
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "GG")
        {
            isGGNear = false;
            isDamage = false;
            //Его нигде не видно? Тогда пора быть мирным гражданином нашему ГГ.
            userData.ggData.SetBattle(false);
        }
    }

    public void GetDamage(float damage)
    {
        //Debug.Log(enemyData.stats.Get(Stats.Key.HP));
        //по клику здоровье врага отнимается, если стало 0, то через секнду объект врага удаляется со сцены
        float currHP = enemyData.stats.Get(Stats.Key.HP);
        enemyData.stats.Set(Stats.Key.HP, currHP - damage);
        if ((currHP - damage <= 0) && (!IsDeath))
            Dying();    
    }

    public void Dying ()
    {
        IsDeath = true;

        DropLoot();

        if (State == (EnemyState)0 || State == (EnemyState)3 || State == (EnemyState)6)
        {
            State = EnemyState.EnemyStateDeathRight;
        }
        if (State == (EnemyState)1 || State == (EnemyState)4)
        {
            State = EnemyState.EnemyStateDeathRight;
        }
        if (State == (EnemyState)5 || State == (EnemyState)2)
        {
            State = EnemyState.EnemyStateDeathBack;
        }
        userData.ggData.stats.Set(Stats.Key.CURRENT_EXPERIENCE, userData.ggData.stats.Get(Stats.Key.CURRENT_EXPERIENCE) + Random.Range(1, 50));
        userData.ggData.quests.QuestList[1].CurrentNumber++;
        if (userData.ggData.quests.QuestList[1].CurrentNumber >= 5){ 
            userData.ggData.quests.QuestList[1].status = Quest.Status.DONE;
            userData.ggData.quests.QuestList[1].CurrentNumber = 5;
        }
        GameObject.Find("HP_Bar").GetComponent<ChangeHPBar>().FunctionOnEnable();
        timer = 0;
    }

    private void DropLoot()
    {
        List<ItemData> itemList = enemyData.inventory.GetList();

        //создаем массив точек для выпадения предметов
        Vector3[] points = new Vector3[itemList.Count];
        float rad = 0.5f;
        //заполняем массив точками на окружности
        for (int i = 0; i < itemList.Count; i++)
        {
            //магическая математика
            double rand = Random.Range(0, 2 * (float)System.Math.PI);
            points[i] = new Vector3((transform.position.x + rad * (float)System.Math.Sin(rand)), (transform.position.y + rad * (float)System.Math.Cos(rand)), -0.9f);
        }

        for (int i = 0; i < itemList.Count; i++)
        {
            GameObject item = Instantiate(itemPrefab, points[i], Quaternion.identity) as GameObject;
            item.GetComponent<ItemOnScene>().itemData = itemList[i];
        }

        //Artem1101: для теста
        for (int i = 0; i < Random.Range(1,4); i++)
        {
            double rand = Random.Range(0, 2 * (float)System.Math.PI);
            Instantiate(itemPrefab, new Vector3((transform.position.x + rad * (float)System.Math.Sin(rand)), (transform.position.y + rad * (float)System.Math.Cos(rand)), -0.9f), Quaternion.identity);
        }
    }

}



public enum EnemyState
{
    EnemyStateRight, //0
    EnemyStateFront, //1
    EnemyStateBack, //2
    EnemyStateAttackRight, //3
    EnemyStateAttackFront, //4
    EnemyStateAttackBack, //5
    EnemyStateAttackLeft,
    EnemyStateDeathRight, //7
    EnemyStateDeathFront, //8
    EnemyStateDeathBack //9
}
