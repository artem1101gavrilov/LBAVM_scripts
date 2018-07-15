using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC_TAVERNA : MonoBehaviour {
    public float playTime;

    private void OnEnable()
    {
        GetComponent<Animator>().speed = 0;
        StartCoroutine(SetAnim());
    }

    IEnumerator SetAnim()
    {
        yield return new WaitForSeconds(playTime);
        GetComponent<Animator>().speed = 1;
    }
}
