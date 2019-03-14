using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scoreCounter : MonoBehaviour {
    public Text scoreText; // Score textbox
    public int score;
    public int starValue; // Points given when a star is hit
    public int meteoriteValue; // Points removed when a meteorite is hit
	// Use this for initialization
	void Start () {
        score = 0;
        UpdateScore();
	}
    // When a collision with another sprite takes place
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Equals("star") == true) //if object is a star add points
        {
            score += starValue;
            UpdateScore();
        }
        else if (collision.gameObject.tag.Equals("meteorite") == true) // if object is a meteorite remove points
        {
            score += meteoriteValue;
            UpdateScore();
        }
    }
    //This function updates the score text in the game
    void UpdateScore()
    {
        scoreText.text = "Score:\n" + score;
    }
}
