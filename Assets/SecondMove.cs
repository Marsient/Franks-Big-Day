using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SecondMove : MonoBehaviour
{
    public LayerMask ignoreUI;
    public Camera cam;
    public TMP_Text text;
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -10f;
    public float dashSpeed = 1000f;
    public bool inText;
    public Transform groundCheck;
    public Transform rangeCheck;
    public float rangeDistance = 2f;
    public float groundDistance = 0.4f;
    public int dashCD = 0;
    public LayerMask PickMask;
    public LayerMask groundMask;
    public int holding = 0;
    public bool canHold = true;
    Vector3 velocity;
    bool isGrounded;
    public bool inRange;
    public float clickProt = 0f;
    private Ray _ray;
    private RaycastHit _hit;
    private bool canMove = true;
    private bool inScene;
    private string textType = "";
    // Activate when building final product, for opening cutscene, set to 250 (5 second)
    private float startCutscene = 0f;
    //Throwable Objects
    public GameObject hold1;
    public GameObject hold2;
    public GameObject hold3;
    void Start() {
    }
    void Update() {
        if(inScene) {
        Clicking();
        Texts();
        if(canMove) {
        Movement();
        }
        }
        if(startCutscene > 0) {
            inScene = false;
        } else {
            inScene = true;
        }
        inRange = Physics.CheckSphere(rangeCheck.position, rangeDistance, PickMask);
    }
    void FixedUpdate() {
        clickProt -= 1;
        dashCD -= 1;
        startCutscene -= 1;
    }
    void Movement() {
 //Checks if the player is on the ground
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        //Resets the player's velocity if they are on the ground
        if(isGrounded && velocity.y < 0) {
            velocity.y = -2f;
        }
        //Takes the player's movement inputs
        float x = Input.GetAxis("Vertical");
        float z = Input.GetAxis("Horizontal") * -1;
        // Moves the player based on their movememnt inputs
        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime * 2f;
        controller.Move(velocity * Time.deltaTime);
        if(!canHold && Input.GetKeyDown(KeyCode.E)) {
            switch (holding) {
                case 1:
                    GameObject toss;
                    toss = Instantiate(hold1, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
                    toss.GetComponent<Rigidbody>().velocity = move * 30f;
                    canHold = true;
                    break;
                case 2:
                    GameObject toss2;
                    toss2 = Instantiate(hold2, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
                    toss2.GetComponent<Rigidbody>().velocity = move * 30f;
                    canHold = true;
                    break;
                    case 3:
                    GameObject toss3;
                    toss3 = Instantiate(hold3, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
                    toss3.GetComponent<Rigidbody>().velocity = move * 30f;
                    canHold = true;
                    break;
                // new Vector3(transform.position.x, transform.position.y + 3, transform.position.z)
            }
        }
}
    void Clicking() {
        Vector2 mousePos = Input.mousePosition;
        _ray = Camera.main.ScreenPointToRay(mousePos);
        if (Input.GetMouseButtonDown(0)) {
        if(Physics.Raycast(_ray, out _hit, 10000f, ~ignoreUI)) {
            if(_hit.transform.gameObject.name == "BaseClickable") {
                canMove = false;
                inText = true;
                clickProt = 5f;
                    text.text = "it's just a square";
            }
            if(_hit.transform.gameObject.name == "Chair") {
                canMove = false;
                inText = true;
                clickProt = 5f;
                    text.text = "a chair !";
            }
            if(_hit.transform.gameObject.name == "bottle") {
                canMove = false;
                inText = true;
                clickProt = 5f;
                    text.text = " i get no kicks from champagne";
                    textType = "bottle1";
            }
            if(_hit.transform.gameObject.name == "frame") {
                canMove = false;
                inText = true;
                clickProt = 5f;
                    text.text = "my buddy kelvin";
                    textType = "";
            }
            if(_hit.transform.gameObject.name == "bed") {
                canMove = false;
                inText = true;
                clickProt = 5f;
                    text.text = "my bed.";
                    textType = "bed1";
            }
            if(_hit.transform.gameObject.name == "apwindow") {
                canMove = false;
                inText = true;
                clickProt = 5f;
                    text.text = "i probably shouldn't get near that";
                    textType = "";
            }
        }
        }
    }
    void Texts() {
        if(Input.GetMouseButtonDown(0) && inText && clickProt < 0f)  {
                    switch (textType) {
                        case "":
                        text.text = "";
                        canMove = true;
                        inText = false;
                        break;
                        case "bottle1":
                        text.text = "mere alcohol doesnt fill me at all";
                        clickProt = 5f;
                        textType = "bottle2";
                        break;
                        case "bottle2":
                        text.text = "so tell my why, shouldnt it be true?";
                        clickProt = 5f;
                        textType = "bottle3";
                        break;
                        case "bottle3":
                        text.text = "i get a kick out of brew";
                        clickProt = 5f;
                        textType = "";
                        break;
                        case "bed1":
                        text.text = " at some point the rocks in there have to get softer, right?";
                        clickProt = 5f;
                        textType = "";
                        break;
                    }
                    }
    }
}