using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NPCBehaviour : MonoBehaviour
{
    Color col;
    private Vector3 direction; //для перемещения 
    private Animator animator; //работа с анимацией
    private SpriteRenderer sprite; //для разворота персонажа в анимации лево-право
    private int rnumber;
    private Vector3[] target;
    private Vector3 currTarget;
    public float timer;

    GameObject dialogPanel;
    public bool isDialog;
    public string NPCName;
    Transform Dialog;
    UserData userData;

    [SerializeField]
    private int sizeAnimation;

    NPCData data;
    //string[] Frazi = { "Всего хорошего!", "Привет!", "Какая хорошая погода.", "Как пройти в библиотеку?", "..." };

    private NPCState State //Установка и получение состояния (анимации)
    {
        get
        {
            return (NPCState)animator.GetInteger("StateNpc");
        }
        set
        {
            animator.SetInteger("StateNpc", (int)value);
        }
    }

    private void Start()
    {
        isDialog = false;
        animator = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
        if (NPCName == null) NPCName = "NPC0";
        data = new NPCData(NPCName);
        userData = GameObject.Find("UserData").GetComponent<UserData>();

        //создаем массив точек для гуляния
        target = new Vector3[5];
        //и радиус хождения
        float rad = 1;
        //заполняем массив точками на окружности
        for (int i = 0; i < 5; i++)
        {
            //магическая математика
            double rand = Random.Range(0, 1 * (float)System.Math.PI);
            target[i] = new Vector3((transform.position.x + rad * (float)System.Math.Sin(rand)), (transform.position.y + rad * (float)System.Math.Cos(rand)), -2);
        }
        GetNextPoint();

        //StartCoroutine(NPCCoroutine());
    }

    /*IEnumerator NPCCoroutine()
    {
        while (true)
        {
            if (Vector3.Distance(transform.position, GameObject.Find("Player").gameObject.transform.position) < 10.0f)
            {
                if (!isDialog)
                    Walking();
                else
                {
                    timer += 1 * Time.deltaTime;
                    if (timer >= 2)
                    {
                        Destroy(dialogPanel);
                        timer = 0;
                        isDialog = false;
                    }
                }
                transform.position = new Vector3(transform.position.x, transform.position.y, -2 + transform.position.y / 1000);
            }
            yield return null;
        }
    }*/

    private void Update()
    {
        if (Vector3.Distance(transform.position, GameObject.Find("Player").gameObject.transform.position) < 10.0f)
        {
            if (!isDialog)
                Walking();
            else
            {
                timer += 1 * Time.deltaTime;
                if (timer >= 2)
                {
                    Destroy(dialogPanel);
                    timer = 0;
                    isDialog = false;
                }
            }
            transform.position = new Vector3(transform.position.x, transform.position.y, -2 + transform.position.y / 1000);
        }
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

    private void Walking()
    {
        //просто движемся до выбранной точки, если дошли - меняем точку
        Vector3 dir = currTarget - transform.position;
        transform.Translate(dir.normalized * 0.1f * Time.deltaTime, Space.World); //было 0.5
        if (Vector3.Distance(transform.position, currTarget) <= 0.3f)
        {
            timer += 1 * Time.deltaTime;
            if (timer >= 2)
            {
                GetNextPoint();
                timer = 0;
            }

        }
    }

    private void GetNextPoint()
    {
        //выбираем новую точку, до которой будем двигаться
        currTarget = target[Random.Range(0, 5)];
        //Для смены анимации у НПС. Считаем угол от НПС до следующей точки
        //Подумать еще с условиями (где-то лишние больше или равно)
        float x = currTarget.x - transform.position.x;
        float b = Mathf.Atan2((currTarget.y - transform.position.y), x);
        b = b * 180 / 3.14f;

        if (b >= -45 && b <= 45)
        {
            switch (sizeAnimation)
            {
                case 3:
                    State = NPCState.NPC1Right;
                    sprite.flipX = false;
                    break;
                case 4:
                    State = NPCState.NPC1Right;
                    break;
            }
        }
        else if (b >= 45 && b <= 135)
        {
            State = NPCState.NPC1Back;
        }
        else if (b >= 135 || b <= -135)
        {
            switch (sizeAnimation)
            {
                case 3:
                    State = NPCState.NPC1Right;
                    sprite.flipX = true;
                    break;
                case 4:
                    State = NPCState.NPC1Left;
                    break;
            }
        }
        else if (b >= -135 && b <= -45)
        {
            State = NPCState.NPC1Front;
        }
    }

    //всплывание реплики
    public void Interacte()
    {
        //Если уже панелька есть, то не будем создавать еще одну.
        if (data.questID == -1 && !isDialog)//если не дает квест - выводим панельку
        {
            timer = 0;
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 pos = new Vector3(0, 0, 0);
            isDialog = true;
            dialogPanel = Instantiate(Resources.Load("DialogPanel"), pos, Quaternion.identity) as GameObject;
            dialogPanel.transform.SetParent(this.transform, false);
            dialogPanel.transform.position += offset;
            Transform TextPanel = dialogPanel.transform.GetChild(1);
            TextPanel.GetComponent<UnityEngine.UI.Text>().text = data.GetDialog();
        }
        else if (data.questID != -1)// если есть квест выводим диалог
        {
            Time.timeScale = 0.0F; //Это останавливает вообще игроковое время, но можно нажимать на кнопки.
            Dialog = GameObject.Find("Dialog").transform.Find("Canvas");
            Dialog.gameObject.SetActive(true);
            Dialog.Find("Text").GetComponent<UnityEngine.UI.Text>().text = data.GetDialog();

            GameObject agreeButton = Dialog.Find("ButtonAgree").gameObject;
            agreeButton.SetActive(true);
            GameObject disagreeButton = Dialog.Find("ButtonDisagree").gameObject;
            disagreeButton.SetActive(true);

            agreeButton.GetComponent<Button>().onClick.AddListener(delegate { GetQuest(); });
            disagreeButton.GetComponent<Button>().onClick.AddListener(delegate { GetDisagree(); });
        }
    }

    //выдавание квеста
    public void GetQuest()
    {
        userData.ggData.quests.TakeTheQuest(data.questID);
        Dialog.gameObject.SetActive(false);
        Time.timeScale = 1.0F; //Возвращает скорость в мир наш как было до этого.
    }

    public void GetDisagree()
    {
        Dialog.gameObject.SetActive(false);
        Time.timeScale = 1.0F; //Возвращает скорость в мир наш как было до этого.
    }

    /*
    void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "GG" && (data.questID == -1) && !isDialog)
        {
            Vector3 offset = new Vector3(0, 0.5f, 0);
            Vector3 pos = new Vector3(0, 0, 0);
            isDialog = true;
            dialogPanel = Instantiate(Resources.Load("DialogPanel"), pos, Quaternion.identity) as GameObject;
            dialogPanel.transform.SetParent(this.transform, false);
            dialogPanel.transform.position += offset;
            Transform TextPanel = dialogPanel.transform.GetChild(1);
            if(NPCName == "NPC0") TextPanel.GetComponent<UnityEngine.UI.Text>().text = Frazi[Random.Range(0, Frazi.Length - 1)];
            else TextPanel.GetComponent<UnityEngine.UI.Text>().text = data.dialog;
        }
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.tag == "GG" && (data.questID == -1))
        {
            Destroy(dialogPanel);
            isDialog = false;
        }
    }*/

}

public enum NPCState
{
    NPC1Right, //0
    NPC1Front, //1
    NPC1Back, //2
    NPC1Left
}
