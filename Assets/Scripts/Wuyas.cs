using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wuyas : MonoBehaviour {

    public float velocity;
	// Use this for initialization

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += new Vector3(-Time.deltaTime * velocity, 0);
    }
}
