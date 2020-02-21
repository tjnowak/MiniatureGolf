using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayAction : MonoBehaviour {

	// Use this for initialization
	public void ClickAction () {
        PlayerController.totalStrokes = 0;
        SceneManager.LoadScene("Level1");
    }
}
