using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetFoodActive : MonoBehaviour {

    public List<GameObject> foods = new List<GameObject>();
    
    public void AllActive()
    {
        for (int i = 0; i < foods.Count; i++)
        {
            foods[i].SetActive(true);
        }
    }
}
