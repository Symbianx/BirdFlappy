using UnityEngine;
using System.Collections;
using System;
using GoogleMobileAds.Api;

public class GameController : MonoBehaviour
{
    private AdController adController;

    public const float GameSpeed = 0.8f;
    void Start()
    {
        GameObject.Find("Bird").GetComponent<BirdController>().GameOver += GameController_GameOver;
        adController = new AdController();
        adController.SetEvents(this);
        adController.BottomBanner();
    }

    void GameController_GameOver(object sender, EventArgs e)
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = true;
    }

    public event EventHandler GameStarted;
    public void StartPlaying()
    {
        GameObject.Find("Canvas").GetComponent<Canvas>().enabled = false;
        if (GameStarted != null)
            GameStarted(this, new EventArgs());
    }
}
