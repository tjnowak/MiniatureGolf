using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour {

    const string GAME_INFO = "                                      Let's play miniature golf! \n\n" +
                             "You are playing 4 holes of golf. The number of putter strokes " + 
                              "each hole\nshould require is displayed as the par number. Try " + 
                              "to finish each hole with \nas few strokes as possible.\n\n" +
                              "Use the 'A' and 'D' keys or left and right arrow keys to pick the " + 
                              "direction the \nball will move. Hold down the spacebar and release " + 
                              "it to putt the ball. The \nlonger the spacebar is held, the harder " + 
                              "the ball will be \"hit\". The ball must \ncome to a complete stop " + 
                              "before it can be aimed again. When a hole is finished, press any key " +
		                      "to go on to the next hole.\n\nPress any key to start the game...";
    const int GAME_PAR = 8;  // Strokes needed to par for course

    public Text txtInfo;
    public GameObject player = null;
    public GameObject txtBackground = null;
    public int level;  // level the player is currently on 
    bool instructionsDisplayed;  // are the instructions currently displayed?
    public bool gameOver;  // whether or not the level has been won 


    // Use this for initialization
    void Start () {
        gameOver = false;
        if (level == 1) {
            txtInfo.text = GAME_INFO;
            instructionsDisplayed = true;
        }
        else if (level == 5)  // level 5 is the end scene
            DisplayResults();
        else
            txtInfo.text = "";            
    }
	
	// Update is called once per frame
	void Update () {
        if (instructionsDisplayed && Input.anyKey) { // only reset the info txt once per level
            txtInfo.text = "";
            txtBackground.SetActive(false);  // disable background around text
            instructionsDisplayed = false;
        }
        if (gameOver && Input.anyKey)   
            if (level <= 3)
                SceneManager.LoadScene("Level" + (level + 1)); // go to next level
            else
                SceneManager.LoadScene("End"); // display results

    }

    // Display the player's total game score and the score achieved if every hole was parred
    void DisplayResults() {
        txtInfo.text = "Your Score: " + PlayerController.totalStrokes +
                       "\nCourse Par Score: " + GAME_PAR;
    }
}
