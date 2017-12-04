using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour {

    public float addMass;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {           
            other.GetComponent<BirdMove>().eatedFood++;
            other.GetComponent<BirdMove>().eatedfoodtest++;
            other.GetComponent<BirdMove>().AddMass(addMass);
            gameObject.SetActive(false);
			other.GetComponent<BirdMove>().audioSource [3].Play ();
        }
    }
}
