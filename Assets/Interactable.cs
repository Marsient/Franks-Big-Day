using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public GameObject player;
    public Transform range;
    public float range2;
    public LayerMask play;
    public int value = 1;
    private bool selfRange;
    void Start() {
        player = GameObject.Find("2DPlayer");
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) {
            selfRange = Physics.CheckSphere(range.position, range2, play);
            if(player.GetComponent<SecondMove>().inRange && player.GetComponent<SecondMove>().canHold && selfRange) {
                player.GetComponent<SecondMove>().holding = value;
                Debug.Log("Picked up");
                player.GetComponent<SecondMove>().canHold = false;
                Destroy(gameObject);
            }
        }
    }
}
