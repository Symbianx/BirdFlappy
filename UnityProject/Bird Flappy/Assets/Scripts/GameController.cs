using UnityEngine;
using System.Collections;
using System;

public class GameController : MonoBehaviour {

    void Start()
    {
        GameObject.Find("Bird").GetComponent<BirdController>().GameOver += GameController_GameOver;
    }

    void GameController_GameOver(object sender, EventArgs e)
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }

    public event EventHandler GameStarted;
    public void StartPlaying()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        if(GameStarted!=null)
            GameStarted(this, new EventArgs());
    }
}
