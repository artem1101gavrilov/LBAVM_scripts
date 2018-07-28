using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
    public Canvas thisMenu;
    public GameObject ResumeButton; //Кнопка "вернуться в игру"
    public GameObject LoadButton; //Кнопка загрузки игры
    //Кнопки "новая игра" в количестве двух штук, все из-за того, чтобы было два разных меню
    //1. Меню без возможности загрузиться (нет файла сохранения)
    //2. Меню с возможностью загрузить игру
    public GameObject NewButton; //Кнопка "новая игра"
    public GameObject NewButton2; //Кнопка "новая игра"
    public GameObject PanelSettings; //БАЛВАНКА. Старая панель с регулированием музыки и звуков
    public GameObject Settings;

    public float music; //Для БАЛВАНКИ
    public float sound; //Для БАЛВАНКИ

    UserData userData;

    //public Image img1;
    //public Image img2;
    //public Image img3;

    public void Start()
    {
        //img1.rectTransform.sizeDelta = new Vector2(thisMenu.GetComponent<RectTransform>().rect.width, thisMenu.GetComponent<RectTransform>().rect.height);
        //img2.rectTransform.sizeDelta = new Vector2(thisMenu.GetComponent<RectTransform>().rect.width, thisMenu.GetComponent<RectTransform>().rect.height);
        //img3.rectTransform.sizeDelta = new Vector2(thisMenu.GetComponent<RectTransform>().rect.width, thisMenu.GetComponent<RectTransform>().rect.height);
        if (File.Exists(Application.dataPath + "/saves/save1.sv"))
        {
            ResumeButton.SetActive(true);
            LoadButton.SetActive(true);
            NewButton.SetActive(true);
            NewButton2.SetActive(false);
        }
        else
        {
            ResumeButton.SetActive(false);
            LoadButton.SetActive(false);
            NewButton.SetActive(false);
            NewButton2.SetActive(true);
        }

        userData = GameObject.Find("UserData").GetComponent<UserData>();
    }

    public void StartGame()
    {
        PlayerPrefs.SetInt("loading", 0);
        //Application.LoadLevel(1);
        //SceneManager.LoadScene(1);
        transform.GetChild(10).gameObject.SetActive(true);

        userData.ggData.stats.InitializationStatsNewGame();
        Time.timeScale = 1.0F;
    }

    public void LoadGame()
    {
        PlayerPrefs.SetInt("loading", 1);
        //Application.LoadLevel(1);
        SceneManager.LoadScene(2);
    }

    public void SettingGame()
    {
        //PanelSettings.SetActive(!PanelSettings.activeSelf);
        Settings.SetActive(true);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void SetMusic(float val)
    {
        music = val;
    }

    public void SetSound(float val)
    {
        sound = val;
    }
}
