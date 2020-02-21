using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;

public class PlayerController : MonoBehaviour
{

    public GameObject cam, gameMaster;
    public Text txtHole, txtPar, txtStrokes, txtTotalStrokes, txtVelocity, txtWin;
    public float speed;
    public int par;  // expected strokes to complete hole
    public int hole;  // hole number being played
    public int strokes;
    public static int totalStrokes = 0;
    bool strokesIncreased;  // flag whether to increase strokes
    bool firstSpace;  // flag whether the spacebar has been pressed yet
    Vector3 startPosition = new Vector3(9f, 0.2f, 0f);

    // Use this for initialization
    void Start() {
        speed = 0f;
        strokesIncreased = false;
        firstSpace = false;
        strokes = 0;
        txtPar.text = "Par For Hole: " + par;
        txtHole.text = "Hole: " + hole;
        txtWin.text = "";
        UpdateDisplays();    
    }

    // Update is called once per frame
    void Update() {
        // Increase speed as spacebar is held
        if (Input.GetKey("space")) {
            if (gameMaster.GetComponent<GameMaster>().level == 1 && !firstSpace) {
                firstSpace = true;
                Thread.Sleep(200); // so ball isn't hit while disabling instructions
            }
            else {
                speed += 1.5f;
                strokesIncreased = true;
            }               
        }
        else if (strokesIncreased) {
            MovePlayer();
            strokesIncreased = false;  // reset strokes flag
        }
        CheckBounds();
        UpdateDisplays();
    }

    // Handle collisions
    void OnCollisionEnter(Collision colInfo) {
        if (colInfo.collider.tag == "Hole") {
            if (strokes == 1)
                txtWin.text = "Hole in One!";
            else if (strokes == (par - 1))
                txtWin.text = "You Got a Birdie!";
            else if (strokes < par)
                txtWin.text = "You Scored Under Par!";
            else if (strokes == par)
                txtWin.text = "You Parred the Hole!";
            else if (strokes == (par + 1))
                txtWin.text = "You Got a Bogey!";
            else
                txtWin.text = "You Scored!";
            colInfo.collider.GetComponent<AudioSource>().Play();  // play win sound
            gameMaster.GetComponent<GameMaster>().gameOver = true;  // hole complete
        }
    }

    // Update strokes, game strokes, and velocity displays
    void UpdateDisplays() {
        txtStrokes.text = "Hole Strokes: " + strokes;
        txtTotalStrokes.text = "Game Strokes: " + totalStrokes;
        txtVelocity.text = "Ball Velocity: " + GetComponent<Rigidbody>().velocity;
    }

    // Move the player by adding a force to it which corresponds to the
    // length of time the spacebar is held and the direction the camera is pointed.  
    //  Also increase strokes count.
    void MovePlayer() {
        // Get direction camera is pointing
        Vector3 direction = cam.transform.forward;
        direction.y = 0f;  // don't move in the y direction
        // Move player
        GetComponent<Rigidbody>().AddForce(speed * direction.normalized);  // normalize to give direction vector length of 1
        GetComponent<AudioSource>().Play();  // play putting sound
        // Update variables
        speed = 0f;
        strokes++;
        totalStrokes++;
    }

    // Check if the player is out of bounds. If so, place the ball
    // back at the starting position.
    void CheckBounds() {
        if (transform.position.y <= -1f) {
            GetComponent<Rigidbody>().velocity = new Vector3(0f, 0f, 0f);  // stop movement
            transform.position = startPosition;
        }
    }
}