using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class swag : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;

    private void Awake() {
        p1.SetActive(true);
        p2.SetActive(true);
    }

    private void Start() {
        p1.SetActive(false);
        p2.SetActive(false);
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Q)) {
            Debug.Log("p1");
            p1.SetActive(true);
            p2.SetActive(false);
        }
        else if (Input.GetKeyDown(KeyCode.P)) {
            Debug.Log("p2");
            p2.SetActive(true);
            p1.SetActive(false);
        }
    }

}
