using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InteractCollider : MonoBehaviour {

    Collider2D nearestObject;
    [SerializeField]
    List<Collider2D> nearObjectsList;
    GameObject hand;
    GameObject door;
    GameObject dialog;
    GameObject interactePanel;

    void Awake () {
        nearestObject = new Collider2D();
        nearObjectsList = new List<Collider2D>();
        hand = Instantiate(Resources.Load<GameObject>("Hand"), Vector3.zero, Quaternion.identity);
        hand.SetActive(false);
        door = Instantiate(Resources.Load<GameObject>("Door"), Vector3.zero, Quaternion.identity);
        door.SetActive(false);
        dialog = Instantiate(Resources.Load<GameObject>("Dialog"), Vector3.zero, Quaternion.identity);
        dialog.SetActive(false);
        //interactePanel = Instantiate(Resources.Load<GameObject>("InteractePanel"), Vector3.zero, Quaternion.identity);
        //interactePanel.SetActive(false);
    }
	
	void Update () {
        //взаимодействие на е
        nearestObject = FindNearest();
        if (nearestObject == null)
        {
            hand.SetActive(false);
            door.SetActive(false);
            dialog.SetActive(false);
            //interactePanel.SetActive(false);
        }
        
        else
        {
            //вброс говнокода
            //если панелька переместилась - активируем, если нет - ничего не делаем, она и так активна
            if (nearestObject.transform != dialog.transform.parent)
            {
                if (nearestObject.gameObject.tag == "NPC")
                {
                    dialog.transform.SetParent(nearestObject.transform);
                    dialog.transform.localPosition = new Vector3(0, 0.5f, -1);
                    dialog.SetActive(true);
                }
                if (nearestObject.gameObject.tag == "Loot")
                {
                    hand.transform.SetParent(nearestObject.transform);
                    hand.transform.localPosition = new Vector3(0, 0.2f, -1);
                    hand.SetActive(true);
                }
                if (nearestObject.gameObject.tag == "Door")
                {
                    door.transform.SetParent(nearestObject.transform);
                    door.transform.localPosition = new Vector3(0, 0.2f, -1);
                    door.SetActive(true);
                }
            }
            //а если это нпс с которым говорим - то убираем панельку, чтобы не закрывала диалог
            else if ((nearestObject.gameObject.tag == "NPC") && (nearestObject.gameObject.GetComponent<NPCBehaviour>().isDialog))
                dialog.SetActive(false);

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (nearestObject.gameObject.tag == "NPC")
                {
                    nearestObject.GetComponent<NPCBehaviour>().Interacte();
                }

                if (nearestObject.gameObject.tag == "Loot")
                {
                    hand.transform.SetParent(transform);
                    hand.SetActive(false);
                    nearestObject.GetComponent<ItemOnScene>().Interacte();
                    nearObjectsList.Remove(nearestObject);
                }

                if (nearestObject.gameObject.tag == "Door")
                {
                    //Выдает сразу кучу ошибок при переходе на другую сцену.
                    door.transform.SetParent(transform);
                    door.SetActive(false);
                    nearObjectsList.Remove(nearestObject); 
                    //GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color(GameObject.Find("Player").GetComponent<SpriteRenderer>().color.r, GameObject.Find("Player").GetComponent<SpriteRenderer>().color.g, GameObject.Find("Player").GetComponent<SpriteRenderer>().color.b, 1.0f);
                    //DontDestroyOnLoad(GameObject.Find("UserData"));
                    //DontDestroyOnLoad(GameObject.Find("Player"));
                    //GameObject.Find("Player").transform.position = new Vector3(-2.0f, 0.0f, GameObject.Find("Player").transform.position.z);
                    int idscene = Application.loadedLevel;
                    switch (idscene)
                    {
                        case 2:
                            GameObject.Find("UserData").GetComponent<UserData>().positionTilTrandent = transform.position;
                            SceneManager.LoadScene(3);
                            break;
                        case 3:
                            SceneManager.LoadScene(2);
                            break;
                    }
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" || collision.gameObject.tag == "Loot" || collision.gameObject.tag == "Door")
        {
            nearObjectsList.Add(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "NPC" || collision.gameObject.tag == "Loot" || collision.gameObject.tag == "Door")
        {
            if (nearObjectsList.Contains(collision))
            {
                nearObjectsList.Remove(collision);
            }
        }
    }

    private Collider2D FindNearest()
    {
        Collider2D nearest = new Collider2D();
        float minDistance = 1;
        if (nearObjectsList.Count > 0)
        {
            for (int i = 0; i < nearObjectsList.Count; i++)
            {
                float currdistance = Vector2.Distance(transform.position, nearObjectsList[i].transform.position);
                if (minDistance > currdistance)
                {
                    minDistance = currdistance;
                    nearest = nearObjectsList[i];
                }
            }
            minDistance = 1;
            return nearest;
        }
        return null;
    }
}
