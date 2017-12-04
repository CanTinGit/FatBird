using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacles : MonoBehaviour {

    Vector3 positionOnCamera = new Vector3();
    public Camera myCamera;
	
    void Start()
    {
        myCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }
	// Update is called once per frame
	void Update () {
	    positionOnCamera = myCamera.WorldToViewportPoint(gameObject.transform.position);
        if (positionOnCamera.x <= 0f)
        {
            GameManager.Instance.iswuya = false;
            GameManager.Instance.AddToWuyaPoll(this.gameObject.transform.parent.gameObject);
        }
    }
}
