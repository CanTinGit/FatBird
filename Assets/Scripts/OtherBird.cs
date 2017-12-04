using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OtherBird : MonoBehaviour {

	// Update is called once per frame

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<BirdMove>().Dead();
        }
    }
}
