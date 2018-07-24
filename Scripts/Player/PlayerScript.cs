﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    public AudioClip[] Shagi;
    public int shag;

    //TODO: Понять где лучше разместить эти переменную
    //public bool IsBattle; //Режим битвы сейчас или нет
    private bool IsDeath; //Мертв ли наш персонаж. True - мертв
    private bool IsDash; //Происходит ли анимация - кувырок
    private bool isFirstAttack; //если сейчас первый удар
    int KudaFirstAttack; //Какое направление первой атаки (1 - Назад) (2 - Вперед) (3 - Лево) (4 - Право)


    [SerializeField]
    private Vector2 speed = new Vector2(2, 2); //Скорость персонажа.

    public GameObject YouDied; //Канвас "Вы погибли"
    public GameObject Sword; //TODO: А надо ли.
    private Animator animator; //работа с анимацией
    private SpriteRenderer sprite; //для разворота персонажа в анимации лево-право
    private Vector3 direction; //для перемещения нашего ГГ
    private UserData userData; 
    private float timer; //Таймер для проигрывания анимации кувырков и смерти.
    Collider2D nearestObject;
    [SerializeField]
    List<Collider2D> nearObjectsList;
    GameObject interactePanel;

    //Все основные параметры нашего персонажа
    private float currentEnergy;    //текущее количество ЭНЕРГИИ
    private float restoringEnergy; //скорость восстановления энергии
    private float expenseEnergy; //скорость расхода энергии

    /*public int currentGold;     //Текущее бабло
    public int currentWeight;   //Насколько тяжела ноша
    public int maxWeight;       //А сколько сможешь поднять ты!?
    public int Level; //текущий левел
    public int CurrentExperience; //сколько опыта есть
    public int NextExperience; //сколько опыта до следующего
    //Характеристики
    //Основные параметры
    public int strength; // Сила ГГ
    public int agility; //Ловкость
    public int endurance; //Выносливость
    public int intellect; //Интеллект 
    //Дополнительные параметры
    public int defense;      //Защита
    public int magicdefense; //Магическая Защита
    public int armor;        //Броня
    public int magicarmor;   //Магическая броня
    //Сопротивляемость
    public int resistanceToPoisons; //сопротивляемость к ядам
    public int resistanceToStunning; // сопротивляемость к оглушению
    public int resistanceToBleeding; //сопротивляемость к кровотечению 
    public int resistanceToMagic; //сопротивляемость к магии

    public float travelspeed; //скорость передвижения
    public float attackSpeed; //скорость атаки
    public float physicalDamage; // физический урон 
    public float criticalDamage; // критический урон 
    public float chanceCriticalDamage; //шанс критический урон */
    Color col;

    private GGState State //Установка и получение состояния ГГ (анимации)
    {
        get
        {
            return (GGState)animator.GetInteger("StatePlayer");
        }
        set
        {
            animator.SetInteger("StatePlayer", (int)value);
        }
    }

    private void Start()
    {
        userData = GameObject.Find("UserData").GetComponent<UserData>();

        
        shag = 0;

        isFirstAttack = true;
        //timer = 0;
        IsDeath = false;
        //IsBattle = true; //false;
        Sword.SetActive(false);

        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        
        nearestObject = new Collider2D();
        nearObjectsList = new List<Collider2D>();
        interactePanel = Instantiate(Resources.Load<GameObject>("InteractePanel"), Vector3.zero, Quaternion.identity);
        interactePanel.SetActive(false);

        if (Application.loadedLevel == 2)
        {
            transform.position = userData.positionTilTrandent;
            sprite.flipX = true;
        }
    }


    void Update()
    {
        //обновляем значение энергии из данных игрока. Использование этой переменной в дальнейшем не меняет значения энергии в юзердата
        //поэтому после каждого изменения здесь обновляю вручную с помощью Set TODO решить этот вопрос по другому
        currentEnergy = userData.ggData.stats.Get(Stats.Key.ENERGY);
        restoringEnergy = userData.ggData.stats.Get(Stats.Key.RESTORING_ENERGY);
        expenseEnergy = userData.ggData.stats.Get(Stats.Key.EXPENSE_ENERGY);

        //Первая проверка на смерть персонажа, если умер, то нет смысла что-то нажимать
        //ВОПРОС!? А нормально каждый кадр проверять userData.ggData.stats.Get(Stats.Key.HP) > 0
        if (userData.ggData.stats.Get(Stats.Key.HP) > 0)
        {
            //Таймер для анимации кувырок
            timer += 1 * Time.deltaTime;
            //Если нажата ЛКМ, то анимация удара
            if (Input.GetMouseButtonUp(0) && (State == GGState.IdleDown || State == GGState.IdleRight || State == GGState.IdleUp)) Attack(Input.mousePosition);
            //Если нажат пробел - по анимцию Dash
            else if (Input.GetKeyDown(KeyCode.Space) && currentEnergy > 50)
            {
                Dash();
            }

            //Используем свои карманы
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                if(userData.Lpocket.id != -1)
                {
                    userData.ggData.stats.Set(Stats.Key.HP, userData.ggData.stats.Get(Stats.Key.HP) + userData.Lpocket.RestoringHP);
                    userData.Lpocket.Stackable--;
                    if (userData.Lpocket.Stackable == 0) userData.Lpocket.id = -1;
                    GameObject.Find("HP_Bar").GetComponent<ChangeHPBar>().FunctionOnEnable();
                    userData.ggData.stats.Set(Stats.Key.WEIGHT, userData.ggData.stats.Get(Stats.Key.WEIGHT) - userData.Lpocket.weight);
                }
                
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                if (userData.Rpocket.id != -1)
                {
                    userData.ggData.stats.Set(Stats.Key.HP, userData.ggData.stats.Get(Stats.Key.HP) + userData.Rpocket.RestoringHP);
                    userData.Rpocket.Stackable--;
                    if (userData.Rpocket.Stackable == 0) userData.Rpocket.id = -1;
                    GameObject.Find("HP_Bar").GetComponent<ChangeHPBar>().FunctionOnEnable();
                    userData.ggData.stats.Set(Stats.Key.WEIGHT, userData.ggData.stats.Get(Stats.Key.WEIGHT) - userData.Rpocket.weight);
                }
            }

            //Если Shift + направление - бег (LeftShift)
            else if (!IsDash && Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Vertical") && Input.GetButton("Horizontal") && (currentEnergy > 10.0F)) RunDiag();
            else if (!IsDash && Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Vertical") && (currentEnergy > 10.0F)) RunUp(2 * 1.0F);
            else if (!IsDash && Input.GetKey(KeyCode.LeftShift) && Input.GetButton("Horizontal") && (currentEnergy > 10.0F)) RunSide(2 * 1.0F);

            //ХОДЬБА
            //Если нажаты две кнопки - происходит движение по диагонали и отображается анимация ходьбы в сторону.
            //Если ходьба в какую-либо сторону, то происходит движение именно туда.
            //Если нажатия кнопок нет, то происходит анимация "стоит". 
            else if (!IsDash && Input.GetButton("Vertical") && Input.GetButton("Horizontal") && (currentEnergy > 10.0F)) WakeDiag();
            else if (!IsDash && Input.GetButton("Vertical") && (currentEnergy > 10.0F)) WakeUp(1.0F);
            else if (!IsDash && Input.GetButton("Horizontal") && (currentEnergy > 10.0F)) Wake(1.0F);
            else
            {
                GetComponent<AudioSource>().Stop();
                //Возвращаемся в состояние "Стояние"
                currentEnergy += restoringEnergy;
                currentEnergy = currentEnergy > 100.0F ? 100.0F : currentEnergy;
                userData.ggData.stats.Set(Stats.Key.ENERGY, currentEnergy);
                if (State == (GGState)3 || State == (GGState)8 || State == (GGState)14 || State == (GGState)15 || State == (GGState)21 || State == (GGState)22 || (State == (GGState)11 && timer >= 0.5f))
                {
                    State = GGState.IdleRight;
                    if (State == (GGState)14 || State == (GGState)21) sprite.flipX = true;
                    IsDash = false;
                }
                else if (State == (GGState)4 || State == (GGState)6 || State == (GGState)12 || State == (GGState)19  || (State == (GGState)9 && timer >= 0.5f))
                {
                    State = GGState.IdleUp;
                    IsDash = false;
                }
                else if (State == (GGState)5 || State == (GGState)7 || State == (GGState)13 || State == (GGState)20 || (State == (GGState)10 && timer >= 0.5f))
                {
                    State = GGState.IdleDown;
                    IsDash = false;
                }
            }
            if (IsDash)
            {
                //В зависимости от того, куда кувырок, то производим смещение.
                if (State == GGState.DashFront) transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(0, 1.5f), 3*Time.deltaTime);
                else if (State == GGState.DashBack) transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(0, 1.5f), 3*Time.deltaTime);
                else if (State == GGState.DashSide && sprite.flipX == false) transform.position = Vector3.MoveTowards(transform.position, transform.position + new Vector3(1.5f, 0), 3*Time.deltaTime);
                else transform.position = Vector3.MoveTowards(transform.position, transform.position - new Vector3(1.5f, 0), 3*Time.deltaTime);
            }
        }
        else
        {
            //Для начала проигрывания анимации
            if (!IsDeath) Dying();
            //Таймер для того, чтобы сыграть анимацию умирания
            timer += 1 * Time.deltaTime;
            if(timer > 1.5F)
            {
                //Останавливаем анимацию ГГ, он будет лежать на земле
                GetComponent<Animator>().speed = 0;
                //Если еще не был открыт канвас "Вы погибли", то обнуляем таймер, чтобы сыграть анимацию на канвасе
                if (!YouDied.activeSelf) timer = 0;
                //Открываем канвас "Вы погибли" и тем самым проигрывается анимация 
                YouDied.SetActive(true);
                //Если анимация подходит к концу, то останавливаем игровой таймер, тем самым анимация на канвасе тоже закончится и 
                //делаем активными кнопки на этом канвасе
                if (YouDied.activeSelf && timer > 2)
                {
                    Time.timeScale = 0.0F;
                    YouDied.GetComponent<YouDiedScript>().SetActiveAllButtons();
                }
            }
        }

        //Для того, чтобы все люди относительно друг друга имели передний и задний план.
        transform.position = new Vector3(transform.position.x, transform.position.y, -2 + transform.position.y/1000);


        //это решили не делать, но может еще пригодиться как образец рабочего рейкаста
        //прозрачность перса если он зашел за дом

        /*RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward);
           
        if (hit.collider != null && hit.transform.parent && hit.transform.parent.gameObject.tag == "Building")
        {
            col = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 0.2f);
            sprite.color = col;
        }
        else
        {
            col = new Color(sprite.color.r, sprite.color.g, sprite.color.b, 1);
            sprite.color = col;
        }*/
    }


    void Wake(float mnozhitel_speed)
    {
        GetComponent<AudioSource>().clip = Shagi[shag];
        if (!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        //shag = 1 - shag;
        isFirstAttack = true; //Чтобы после бега/ходьбы была проиграна первая атака

        //currentEnergy -= mnozhitel_speed == 1 ? expenseEnergy : expenseEnergy / 2;
        //currentEnergy = currentEnergy < 0.0F ? 0.0F : currentEnergy;
        //userData.ggData.stats.Set(Stats.Key.ENERGY, currentEnergy);
        //Так как одна анимация на два случая жизни, то сразу отображаем анимацию, а потом осуществляем ходьбу.
        State = GGState.WalkRight;
        direction = transform.right * Input.GetAxis("Horizontal");
        if (direction.x < 0.0F)
        {
            //Vector2 PointA = new Vector2(transform.position.x - (float)0.22, transform.position.y + (float)0.3);
            //Vector2 PointB = new Vector2(transform.position.x - (float)0.15, transform.position.y - (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if(colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
        }
        else
        {
            //Vector2 PointA = new Vector2(transform.position.x + (float)0.22, transform.position.y + (float)0.3);
            //Vector2 PointB = new Vector2(transform.position.x + (float)0.15, transform.position.y - (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if (colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
        }
        sprite.flipX = direction.x < 0.0F;
    }

    void WakeUp(float mnozhitel_speed)
    {
        GetComponent<AudioSource>().clip = Shagi[shag];
        if(!GetComponent<AudioSource>().isPlaying) GetComponent<AudioSource>().Play();
        //shag = 1 - shag;
        isFirstAttack = true; //Чтобы после бега/ходьбы была проиграна первая атака

        //currentEnergy -= mnozhitel_speed == 1 ? expenseEnergy : expenseEnergy / 2;
        //currentEnergy = currentEnergy < 0.0F ? 0.0F : currentEnergy;
        //userData.ggData.stats.Set(Stats.Key.ENERGY, currentEnergy);
        //Останавливаем анимацию, после чего определяем направление движения и затем осуществляем нужную анимацию и движение.
        //ВОЗМОЖНО, можно перенести на две разные функции, чтобы был механизм похожий на функцию Wake(...)
        animator.StopPlayback();
        direction = transform.up * Input.GetAxis("Vertical");
        if (direction.y > 0.0F)
        {
            State = GGState.WalkUp;
            //Vector2 PointA = new Vector2(transform.position.x - (float)0.15, transform.position.y + (float)0.4);
            //Vector2 PointB = new Vector2(transform.position.x + (float)0.15, transform.position.y + (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if (colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
        }
        else
        {
            State = GGState.WalkDown;
            //Vector2 PointA = new Vector2(transform.position.x - (float)0.15, transform.position.y - (float)0.4);
            //Vector2 PointB = new Vector2(transform.position.x + (float)0.15, transform.position.y - (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if (colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
        }
    }

    void WakeDiag()
    {
        //Вызываем поочередно две функции.
        //0.7071F = корень(2)/2, что есть движение на 45 градусов.
        WakeUp(0.7071F);
        Wake(0.7071F);
    }

    void RunSide(float mnozhitel_speed)
    {
        isFirstAttack = true; //Чтобы после бега/ходьбы была проиграна первая атака

        currentEnergy -= mnozhitel_speed == 2 ? 2 * expenseEnergy : expenseEnergy;
        currentEnergy = currentEnergy < 0.0F ? 0.0F : currentEnergy;
        //userData.ggData.stats.Set(Stats.Key.ENERGY, currentEnergy);
        //Так как одна анимация на два случая жизни, то сразу отображаем анимацию, а потом осуществляем ходьбу.
        State = GGState.RunSide;
        direction = transform.right * Input.GetAxis("Horizontal");
        if (direction.x < 0.0F)
        {
            //Vector2 PointA = new Vector2(transform.position.x - (float)0.22, transform.position.y + (float)0.3);
            //Vector2 PointB = new Vector2(transform.position.x - (float)0.15, transform.position.y - (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if(colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
        }
        else
        {
            //Vector2 PointA = new Vector2(transform.position.x + (float)0.22, transform.position.y + (float)0.3);
            //Vector2 PointB = new Vector2(transform.position.x + (float)0.15, transform.position.y - (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if (colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.x * Time.deltaTime);
        }
        sprite.flipX = direction.x < 0.0F;
    }

    void RunUp(float mnozhitel_speed)
    {
        isFirstAttack = true; //Чтобы после бега/ходьбы была проиграна первая атака

        currentEnergy -= mnozhitel_speed == 2 ? 2 * expenseEnergy : expenseEnergy;
        currentEnergy = currentEnergy < 0.0F ? 0.0F : currentEnergy;
        //userData.ggData.stats.Set(Stats.Key.ENERGY, currentEnergy);
        //Останавливаем анимацию, после чего определяем направление движения и затем осуществляем нужную анимацию и движение.
        //ВОЗМОЖНО, можно перенести на две разные функции, чтобы был механизм похожий на функцию Wake(...)
        animator.StopPlayback();
        direction = transform.up * Input.GetAxis("Vertical");
        if (direction.y > 0.0F)
        {
            State = GGState.RunBack;
            //Vector2 PointA = new Vector2(transform.position.x - (float)0.15, transform.position.y + (float)0.4);
            //Vector2 PointB = new Vector2(transform.position.x + (float)0.15, transform.position.y + (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if (colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
        }
        else
        {
            State = GGState.RunFront;
            //Vector2 PointA = new Vector2(transform.position.x - (float)0.15, transform.position.y - (float)0.4);
            //Vector2 PointB = new Vector2(transform.position.x + (float)0.15, transform.position.y - (float)0.3);
            //Collider2D[] colliders = Physics2D.OverlapAreaAll(PointA, PointB);
            //if (colliders.Length == 0) transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
            transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, mnozhitel_speed * speed.y * Time.deltaTime);
        }
    }

    void RunDiag()
    {
        RunUp(2 * 0.7071F);
        RunSide(2 * 0.7071F);
    }

    void Attack(Vector3 MousePosition)
    {
        if (userData.ggData.IsBattle && isFirstAttack)
        {
            //Проверка в какую из четырех областей было нажато ЛКМ
            sprite.flipX = false;
            if (MousePosition.y / MousePosition.x < (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x < -((float)Screen.height / (float)Screen.width))
            {
                State = GGState.Attack1Front;
                KudaFirstAttack = 2;
                //Debug.Log("Attack1Front | " + isFirstAttack);
            }

            if (MousePosition.y / MousePosition.x >= (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x <= -((float)Screen.height / (float)Screen.width))
            {
                State = GGState.Attack1Left;
                KudaFirstAttack = 3;
                //Debug.Log("Attack1Left | " + isFirstAttack);
            }

            if (MousePosition.y / MousePosition.x <= (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x >= -((float)Screen.height / (float)Screen.width))
            {
                State = GGState.Attack1Right;
                KudaFirstAttack = 4;
                //Debug.Log("Attack1Right | " + isFirstAttack);
            }

            if (MousePosition.y / MousePosition.x > (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x > -((float)Screen.height / (float)Screen.width))
            {
                State = GGState.Attack1Back;
                KudaFirstAttack = 1;
                //Debug.Log("Attack1Back | " + isFirstAttack);
            }
        }
        else if(userData.ggData.IsBattle)
        {
            //Проверка в какую из четырех областей было нажато ЛКМ
            sprite.flipX = false;
            if (MousePosition.y / MousePosition.x < (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x < -((float)Screen.height / (float)Screen.width))
            {
                //Debug.Log("Attack2Front | " + isFirstAttack);
                if (KudaFirstAttack == 2) State = GGState.Attack2Front;
                else
                {
                    KudaFirstAttack = 2;
                    isFirstAttack = true;
                    State = GGState.Attack1Front;
                }
            }

            if (MousePosition.y / MousePosition.x >= (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x <= -((float)Screen.height / (float)Screen.width))
            {
                //Debug.Log("Attack2Left | " + isFirstAttack);
                if (KudaFirstAttack == 3) State = GGState.Attack2Left;
                else
                {
                    KudaFirstAttack = 3;
                    isFirstAttack = true;
                    State = GGState.Attack1Left;
                }
            }

            if (MousePosition.y / MousePosition.x <= (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x >= -((float)Screen.height / (float)Screen.width))
            {
                //Debug.Log("Attack2Right | " + isFirstAttack);
                if (KudaFirstAttack == 4) State = GGState.Attack2Right;
                else
                {
                    KudaFirstAttack = 4;
                    isFirstAttack = true;
                    State = GGState.Attack1Right;
                }
            }

            if (MousePosition.y / MousePosition.x > (float)Screen.height / (float)Screen.width &&
                (-(float)Screen.height + MousePosition.y) / MousePosition.x > -((float)Screen.height / (float)Screen.width))
            {
                //Debug.Log("Attack2Back | " + isFirstAttack);
                if (KudaFirstAttack == 1) State = GGState.Attack2Back;
                else
                {
                    KudaFirstAttack = 1;
                    isFirstAttack = true;
                    State = GGState.Attack1Back;
                }
            }
        }
        isFirstAttack = !isFirstAttack;
    }

    void Dying()
    {
        //Персонаж умер. Устанавливаем таймер в 0 для проигрывания анимации
        IsDeath = true;
        timer = 0;
        //Установка нужной анимации
        if (State == (GGState)0 || State == (GGState)3 || State == (GGState)8 || State == (GGState)14 || State == (GGState)15 || State == GGState.DashSide)
        {
            State = GGState.DeathSide;
            if (State == (GGState)14) sprite.flipX = true;
        }
        else if (State == (GGState)2 || State == (GGState)4 || State == (GGState)6 || State == (GGState)12 || State == GGState.DashFront)
        {
            State = GGState.DeathFront;
        }
        else if (State == (GGState)1 || State == (GGState)5 || State == (GGState)7 || State == (GGState)13 || State == GGState.DashBack)
        {
            State = GGState.DeathBack;
        }
    }

    void Dash()
    {
        currentEnergy -= 50;
        userData.ggData.stats.Set(Stats.Key.ENERGY, currentEnergy);
        if (State == (GGState)0 || State == (GGState)3 || State == (GGState)8 || State == (GGState)14 || State == (GGState)15)
        {
            State = GGState.DashSide;
            if (State == (GGState)14)
            {
                sprite.flipX = true;
            }
        }
        else if (State == (GGState)2 || State == (GGState)5 || State == (GGState)7 || State == (GGState)12)
        {
            State = GGState.DashFront;
        }
        else if (State == (GGState)1 || State == (GGState)4 || State == (GGState)6 || State == (GGState)13)
        {
            State = GGState.DashBack;
        }
        IsDash = true;
        timer = 0 + Time.deltaTime;
    }

}

public enum GGState
{
    IdleRight, //0
    IdleUp, //1
    IdleDown, //2
    WalkRight, //3
    WalkUp, //4
    WalkDown, //5
    RunBack, //6
    RunFront, //7
    RunSide, //8
    DashBack, //9
    DashFront, //10
    DashSide, //11
    Attack1Back, //12
    Attack1Front, //13
    Attack1Left, //14
    Attack1Right, //15
    DeathBack, //16
    DeathFront, //17
    DeathSide, //18
    Attack2Back, //19
    Attack2Front, //20
    Attack2Left, //21
    Attack2Right, //22
}
