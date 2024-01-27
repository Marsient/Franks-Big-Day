using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textbg : MonoBehaviour
{
    public GameObject player;
    public MeshRenderer mesh;
    void Start() {
        mesh.enabled = false;
    }
    void Update()
    {
            if(player.GetComponent<SecondMove>().inText) {
                mesh.enabled = true;
            } else {
                mesh.enabled = false;
            }
    }
}
