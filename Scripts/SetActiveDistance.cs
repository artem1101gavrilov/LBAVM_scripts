using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetActiveDistance : MonoBehaviour {

    public GameObject Player;

	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, Player.transform.position) < 10.0f)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
