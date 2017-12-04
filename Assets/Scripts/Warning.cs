using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour {

    public float time;
    public Transform warnPosition;
    public GameObject poll;
	// Use this for initialization
	void OnEnable()
    {
        Invoke("WarnDispear", time);
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position = new Vector3(warnPosition.position.x, gameObject.transform.position.y);
	}


    void WarnDispear()
    {
        GameManager.Instance.warns.Add(gameObject);
        gameObject.transform.parent = poll.transform;
    }
}
