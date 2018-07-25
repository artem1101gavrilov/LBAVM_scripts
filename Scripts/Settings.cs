using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour {

    GameObject currentKey;

    private void OnEnable()
    {
        UpdateSettings();
        ChangeGame();
    }

    // Update is called once per frame
    void Update () {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(currentKey == null) GameObject.Find("UserData").GetComponent<UserData>().settings.SaveSettings();
            gameObject.SetActive(false);
        }
	}

    public void UpdateSettings()
    {
        //Игра
        transform.GetChild(7).gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn = GameObject.Find("UserData").GetComponent<UserData>().settings.autosave;
        transform.GetChild(7).gameObject.transform.GetChild(2).GetComponent<Toggle>().isOn = GameObject.Find("UserData").GetComponent<UserData>().settings.saveAtRest;
        transform.GetChild(7).gameObject.transform.GetChild(8).GetComponent<Slider>().value = GameObject.Find("UserData").GetComponent<UserData>().settings.mouseSensitivity;
        switch (GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity)
        {
            case 1:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case 3:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(true);
                break;
        }
        
        //Экран
        transform.GetChild(8).gameObject.transform.GetChild(1).GetComponent<Slider>().value = GameObject.Find("UserData").GetComponent<UserData>().settings.brightness;
        transform.GetChild(8).gameObject.transform.GetChild(2).GetComponent<Toggle>().isOn = GameObject.Find("UserData").GetComponent<UserData>().settings.generalSubtitles;
        transform.GetChild(8).gameObject.transform.GetChild(3).GetComponent<Toggle>().isOn = GameObject.Find("UserData").GetComponent<UserData>().settings.subtitlesOfDialogs;

        //Аудио
        transform.GetChild(9).gameObject.transform.GetChild(1).GetComponent<Slider>().value = GameObject.Find("UserData").GetComponent<UserData>().settings.totalVolume;
        transform.GetChild(9).gameObject.transform.GetChild(2).GetComponent<Slider>().value = GameObject.Find("UserData").GetComponent<UserData>().settings.soundEffects;
        transform.GetChild(9).gameObject.transform.GetChild(3).GetComponent<Slider>().value = GameObject.Find("UserData").GetComponent<UserData>().settings.music;

        //Управление
        transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Up"].ToString();
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Left"].ToString();	    
    	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(2).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Down"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(3).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Right"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(4).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Roll"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(5).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Acceleration"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(6).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Interaction"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(7).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Inventory"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(8).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Map"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(9).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Journal"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(10).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Skills"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(11).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Organizer"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(12).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Menu"].ToString();	    
	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(13).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Lpocket"].ToString();	    
    	transform.GetChild(10).gameObject.transform.GetChild(2).gameObject.transform.GetChild(0).gameObject.transform.GetChild(14).gameObject.transform.GetChild(1).gameObject.transform.GetChild(0).GetComponent<Text>().text = GameObject.Find("UserData").GetComponent<UserData>().settings.keys["Rpocket"].ToString();
    }

    public void ChangeGame()
    {
        transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Игра выбор");
        transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Экран");
        transform.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Аудио");
        transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Управление");
        transform.GetChild(7).gameObject.SetActive(true);
        transform.GetChild(8).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(false);
    }

    public void ChangeScreen()
    {
        transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Игра");
        transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Экран выбор");
        transform.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Аудио");
        transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Управление");
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(true);
        transform.GetChild(9).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(false);
    }

    public void ChangeAudio()
    {
        transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Игра");
        transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Экран");
        transform.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Аудио выбор");
        transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Управление");
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(true);
        transform.GetChild(10).gameObject.SetActive(false);
    }

    public void ChangeControl()
    {
        transform.GetChild(3).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Игра");
        transform.GetChild(4).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Экран");
        transform.GetChild(5).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Аудио");
        transform.GetChild(6).GetComponent<Image>().sprite = Resources.Load<Sprite>("Settings/Управление выбор");
        transform.GetChild(7).gameObject.SetActive(false);
        transform.GetChild(8).gameObject.SetActive(false);
        transform.GetChild(9).gameObject.SetActive(false);
        transform.GetChild(10).gameObject.SetActive(true);
    }

    public void SetAutosave()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.autosave = transform.GetChild(7).gameObject.transform.GetChild(1).GetComponent<Toggle>().isOn;
    }

    public void SetSaveAtRest()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.saveAtRest = transform.GetChild(7).gameObject.transform.GetChild(2).GetComponent<Toggle>().isOn;
    }

    public void SetMouseSensitivity()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.mouseSensitivity = transform.GetChild(7).gameObject.transform.GetChild(8).GetComponent<Slider>().value;
    }

    public void SetBrightness()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.brightness = transform.GetChild(8).gameObject.transform.GetChild(1).GetComponent<Slider>().value;
    }

    public void SetGeneralSubtitles()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.generalSubtitles = transform.GetChild(8).gameObject.transform.GetChild(2).GetComponent<Toggle>().isOn;
    }

    public void SetSubtitlesOfDialogs()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.subtitlesOfDialogs = transform.GetChild(8).gameObject.transform.GetChild(3).GetComponent<Toggle>().isOn;
    }

    public void SetTotalVolume()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.totalVolume = transform.GetChild(9).gameObject.transform.GetChild(1).GetComponent<Slider>().value;
    }

    public void SetSoundEffects()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.soundEffects = transform.GetChild(9).gameObject.transform.GetChild(2).GetComponent<Slider>().value;
    }

    public void SetMusic()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.music = transform.GetChild(9).gameObject.transform.GetChild(3).GetComponent<Slider>().value;
    }

    public void LeftChangeLevelOfComplexity()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity--;
        if (GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity < 1) GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity = 3;
        switch (GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity)
        {
            case 1:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case 3:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(true);
                break;
        }
    }

    public void RightChangeLevelOfComplexity()
    {
        GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity++;
        if (GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity > 3) GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity = 1;
        switch (GameObject.Find("UserData").GetComponent<UserData>().settings.levelOfComplexity)
        {
            case 1:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case 2:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(true);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(false);
                break;
            case 3:
                transform.GetChild(7).gameObject.transform.GetChild(5).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(6).gameObject.SetActive(false);
                transform.GetChild(7).gameObject.transform.GetChild(7).gameObject.SetActive(true);
                break;
        }
    }

    public void ChangeKeyUp(GameObject clicked)
    {
        //GameObject.Find("UserData").GetComponent<UserData>().settings.KeyUp = KeyCode.X;
        //UpdateSettings();
        clicked.transform.GetChild(0).GetComponent<Text>().text = "Введите кнопку";
        currentKey = clicked;
    }

    public void OnGUI()
    {
        if(currentKey != null)
        {
            Event e = Event.current;
            if (e.isKey)
            {
                currentKey.transform.GetChild(0).GetComponent<Text>().text = e.keyCode.ToString();
                GameObject.Find("UserData").GetComponent<UserData>().settings.keys[currentKey.name] = e.keyCode;
                currentKey = null;
            }
        }
    }
}
