using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warn : MonoBehaviour {

    void OnEnable()
    {
        GameManager.Instance.Warn(this.gameObject);
    }
}
