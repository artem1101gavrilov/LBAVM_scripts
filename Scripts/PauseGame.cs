using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

//TODO:
// В сохранения добавить Лист с квестами 
// Решить вопрос с нажатием на кнопки клавиатуры

public class PauseGame : MonoBehaviour {
    //Для управления окнами разных Менюшек
    public bool ispaused; //true - пауза игры, false - нет паузы
    public bool isinventory; //true - инвентарь,
    public bool isTabMenu; //true - органайзер
    public bool isQuests; //true - задания
    public bool isMap; //true - map
    public bool isSkills; //true - skills

    //Для меню Паузы во время игры
    public GameObject PauseMenu; //Панель меню с кнопками
    public GameObject thisHPBar; //Для того, чтобы картинка во время паузы принимала полный размер экрана.
    //public Image imagePause; //Темный фон во время паузы

    //Для меню Органайзера во время игры
    public GameObject Organaizer; //Канвас органайзера
    //public Image TabMenuImage; //фон для кнопок органайзера

    //Способ вызвать инвентарь
    public GameObject InventoryCanvas;
    //public Image imageInventory; // Фоновая картинка для инвентаря

    //Способ вызвать задания
    public GameObject QuestsCanvas;

    //Пустые фоны для Карты и навыков 
    public GameObject MapCanvas;
    public GameObject SkillsCanvas;

    public GameObject SettingsCanvas;

    //Для сохранения и загрузки данных
    public PlayerScript player; //Объект ГГ для сохранения и загрузки данных
   
	UserData userData;
	

	// Use this for initialization
    void Start () {
	    userData = GameObject.Find("UserData").GetComponent<UserData>();
        //пауза и инвентарь изначально не показываются
        ispaused = false;
        isinventory = false;
        isTabMenu = false; 

        /*
        //Установке картинке размера во весь экран
        imagePause.rectTransform.sizeDelta = new Vector2(thisHPBar.GetComponent<RectTransform>().rect.width, thisHPBar.GetComponent<RectTransform>().rect.height);
        //Установке картинке размера во весь экран
        TabMenuImage.rectTransform.sizeDelta = new Vector2(thisHPBar.GetComponent<RectTransform>().rect.width, thisHPBar.GetComponent<RectTransform>().rect.height);
        //Установке картинке размера во весь экран
        imageInventory.rectTransform.sizeDelta = new Vector2(thisHPBar.GetComponent<RectTransform>().rect.width, thisHPBar.GetComponent<RectTransform>().rect.height);
        */
    }
	
	// Update is called once per frame
	void Update () {
	    //Любой канвас можно вывать с игры. А также с других канвасов исключая себя 
	    //На данный момент спиок канвасов (Пауза, Инвентарь, Органайзер, Задания)
	    //Выключаем все и устанавливаем все в false, А по нажатию на опреденную кнопку устанавливаем на труе. 
	    if(Input.GetKeyDown(userData.settings.keys["Menu"])){
		    isinventory = false; 
		    isTabMenu = false; 
		    isQuests = false;
            isMap = false;
            isSkills = false;
            Organaizer.SetActive(false);
		    InventoryCanvas.SetActive(false);
		    QuestsCanvas.SetActive(false);
            MapCanvas.SetActive(false);
            SkillsCanvas.SetActive(false);
            if (ispaused == false){
			    Time.timeScale = 0.0F;
			    ispaused = true;
			    PauseMenu.SetActive(true);
			    thisHPBar.SetActive(false);
		    }
		    else{
			    ResumeButton();
		    }
	    }
	    if(Input.GetKeyDown(userData.settings.keys["Organizer"]) && !SettingsCanvas.activeSelf)
        {
		    ispaused = false; 
		    isinventory = false; 
		    isQuests = false;
            isMap = false;
            isSkills = false;
            PauseMenu.SetActive(false);
		    InventoryCanvas.SetActive(false);
		    QuestsCanvas.SetActive(false);
            MapCanvas.SetActive(false);
            SkillsCanvas.SetActive(false);
            if (isTabMenu == false){
			    Time.timeScale = 0.0F;
			    isTabMenu = true;
			    Organaizer.SetActive(true);
			    thisHPBar.SetActive(false);
		    }
		    else{
			    isTabMenu = false; 
			    Organaizer.SetActive(false);
			    Time.timeScale = 1.0F;
			    thisHPBar.SetActive(true);
		    }
	    }
	    if(Input.GetKeyDown(userData.settings.keys["Inventory"]) && !SettingsCanvas.activeSelf)
        {
		    ispaused = false; 
		    isTabMenu = false; 
		    isQuests = false;
            isMap = false;
            isSkills = false;
            PauseMenu.SetActive(false);
            Organaizer.SetActive(false);
		    QuestsCanvas.SetActive(false);
            MapCanvas.SetActive(false);
            SkillsCanvas.SetActive(false);
            if (isinventory == false){
			    Time.timeScale = 0.0F;
			    isinventory = true;
			    InventoryCanvas.SetActive(true);
                thisHPBar.SetActive(false);
                //InventoryCanvas.transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
            }
		    else{
			    isinventory = false; 
			    InventoryCanvas.SetActive(false);
			    Time.timeScale = 1.0F;
			    thisHPBar.SetActive(true);
		    }
	    }
	    if(Input.GetKeyDown(userData.settings.keys["Journal"]) && !SettingsCanvas.activeSelf)
        {
		    ispaused = false; 
		    isinventory = false; 
		    isTabMenu = false;
            isMap = false;
            isSkills = false;
            PauseMenu.SetActive(false);
            Organaizer.SetActive(false);
		    InventoryCanvas.SetActive(false);
            MapCanvas.SetActive(false);
            SkillsCanvas.SetActive(false);
            if (isQuests == false){
			    Time.timeScale = 0.0F;
			    isQuests = true;
			    QuestsCanvas.SetActive(true);
			    thisHPBar.SetActive(false);
		    }
		    else{
			    isQuests = false; 
			    QuestsCanvas.SetActive(false);
			    Time.timeScale = 1.0F;
			    thisHPBar.SetActive(true);
		    }
	    }
        if (Input.GetKeyDown(userData.settings.keys["Skills"]) && !SettingsCanvas.activeSelf)
        {
            ispaused = false;
            isinventory = false;
            isTabMenu = false;
            isMap = false;
            isQuests = false;
            PauseMenu.SetActive(false);
            Organaizer.SetActive(false);
            InventoryCanvas.SetActive(false);
            MapCanvas.SetActive(false);
            QuestsCanvas.SetActive(false);
            
            if (isSkills == false)
            {
                Time.timeScale = 0.0F;
                isSkills = true;
                SkillsCanvas.SetActive(true);
                thisHPBar.SetActive(false);
            }
            else
            {
                isSkills = false;
                SkillsCanvas.SetActive(false);
                Time.timeScale = 1.0F;
                thisHPBar.SetActive(true);
            }
        }
        if (Input.GetKeyDown(userData.settings.keys["Map"]) && !SettingsCanvas.activeSelf)
        {
            ispaused = false;
            isinventory = false;
            isTabMenu = false;
            isSkills = false;
            isQuests = false;
            PauseMenu.SetActive(false);
            Organaizer.SetActive(false);
            InventoryCanvas.SetActive(false);
            SkillsCanvas.SetActive(false);
            QuestsCanvas.SetActive(false);

            if (isMap == false)
            {
                Time.timeScale = 0.0F;
                isMap = true;
                MapCanvas.SetActive(true);
                thisHPBar.SetActive(false);
            }
            else
            {
                isMap = false;
                MapCanvas.SetActive(false);
                Time.timeScale = 1.0F;
                thisHPBar.SetActive(true);
            }
        }
    }

    //Кнопка возвратиться к игре, находиться на Панеле паузы
    public void ResumeButton()
    {
        //GetComponent<AudioSource>().Play();
        //Убираем Меню паузы
        PauseMenu.SetActive(false);
	    thisHPBar.SetActive(true);
        //Возвращаем игру в движение
        Time.timeScale = 1.0F;
        //Меню паузы не вызвано, т.е. false
        ispaused = false;
    }

    //Кнопка сохранить игру, находиться на Панеле паузы
    public void SaveButton()
    {
        userData.positionTilTrandent.x = player.transform.position.x;
        userData.positionTilTrandent.y = player.transform.position.y;
        userData.SaveGame();
        //GetComponent<AudioSource>().Play();
        //Возвращаем движение игре
        ResumeButton();
    }

    //Кнопка загрузить игру, находиться на Панеле паузы
    public void LoadButton()
    {
        userData.LoadGame();
        SceneManager.LoadScene(userData.SceneID);
        //GetComponent<AudioSource>().Play();
        //Возвращаем движение игре
        ResumeButton();
    }

    //Кнопка настройки игры, находиться на Панеле паузы
    public void SettingsButton()
    {
        PauseMenu.SetActive(false);
        SettingsCanvas.SetActive(true);
        //GetComponent<AudioSource>().Play();
    }

    //Кнопка выйти в главное меню, находиться на Панеле паузы
    public void BacktoMainSceneButton()
    {
        //GetComponent<AudioSource>().Play();
        //Возвращаем скорость игре, а потом выходим в главное меню
        ResumeButton();
        SceneManager.LoadScene(1);
    }

    //Кнопка выйти из игры, находиться на Панеле паузы
    public void ExitButton()
    {
        //GetComponent<AudioSource>().Play();
        Application.Quit();
    }

    //Кнопки для органайзера
    public void MapButton()
    {
        //GetComponent<AudioSource>().Play();
        isTabMenu = false;
        MapCanvas.SetActive(true);
        Organaizer.SetActive(false);
        isMap = true;
    }

    public void JornalButton()
    {
        //GetComponent<AudioSource>().Play();
        isTabMenu = false;
        QuestsCanvas.SetActive(true);
        Organaizer.SetActive(false);
        isQuests = true;
    }

    public void SkillsButton()
    {
        //GetComponent<AudioSource>().Play();
        isTabMenu = false;
        SkillsCanvas.SetActive(true);
        Organaizer.SetActive(false);
        isSkills = true;
    }

    public void InventoryButton()
    {
        //GetComponent<AudioSource>().Play();
        isTabMenu = false;
        isinventory = true;
        Organaizer.SetActive(false);
        InventoryCanvas.SetActive(true);
        //InventoryCanvas.transform.GetChild(2).transform.GetChild(9).transform.GetChild(0).transform.GetChild(0).GetComponent<InvField>().Everything();
    }
}
