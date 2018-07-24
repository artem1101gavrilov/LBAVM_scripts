using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ForExample : MonoBehaviour {

    private void Start()
    {
        //DontDestroyOnLoad(GameObject.Find("UserData"));
        //SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void Update () {

        //дистанция до ГГ
        //10 нормальная дистанция
        //Debug.Log(Vector3.Distance(transform.position, GameObject.Find("Player").gameObject.transform.position));
	}
}
