using UnityEngine;
using System.Collections;

public class ScoreManagerScript : MonoBehaviour
{

    public static int Score { get; private set; }

    public int HighScore
    {
        get
        {
            return PlayerPrefs.GetInt(SCORE, 0);
        }
        set
        {
            PlayerPrefs.SetInt(SCORE, value);
        }
    }
    private const string SCORE = "score";

    // Use this for initialization
    void Start()
    {
        (Tens.gameObject as GameObject).SetActive(false);
        (Hundreds.gameObject as GameObject).SetActive(false);
        (Thousands.gameObject as GameObject).SetActive(false);
        GameObject.Find("Bird").GetComponent<BirdController>().Scored += ScoreManagerScript_Scored;
        GameObject.Find("Bird").GetComponent<BirdController>().GameOver += ScoreManagerScript_GameOver;
        GameObject.Find("Controllers").GetComponent<GameController>().GameStarted += ScoreManagerScript_GameStarted;
    }

    void ScoreManagerScript_GameStarted(object sender, System.EventArgs e)
    {
        Score = 0;
        Units.sprite = NumberSprites[0];
        previousScore = -1;
        (Tens.gameObject as GameObject).SetActive(false);
        (Hundreds.gameObject as GameObject).SetActive(false);
        (Thousands.gameObject as GameObject).SetActive(false);
    }

    void ScoreManagerScript_GameOver(object sender, System.EventArgs e)
    {
        if(Score>HighScore)
            HighScore = Score;
    }

    void ScoreManagerScript_Scored(object sender, int points)
    {
        Score -= points;
    }

    // Update is called once per frame
    void Update()
    {
        //int tempScore = Mathf.Abs(Score);
        if (previousScore != Score) //save perf from non needed calculations
        {
            if(Score<=0)
                ShowNegativeScore();
            else
                ShowPositiveScore();
            previousScore = Score;
        }

    }

    private void ShowPositiveScore()
    {
        if (Score < 10)
        {
            //just draw units
            (Tens.gameObject as GameObject).SetActive(false);
            (Hundreds.gameObject as GameObject).SetActive(false);
            Units.sprite = NumberSprites[Score];
        }
        else if (Score >= 10 && Score < 100)
        {
            (Tens.gameObject as GameObject).SetActive(true);
            Tens.sprite = NumberSprites[Score / 10];
            Units.sprite = NumberSprites[Score % 10];
        }
        else if (Score >= 100)
        {
            (Tens.gameObject as GameObject).SetActive(true);
            (Hundreds.gameObject as GameObject).SetActive(true);
            Hundreds.sprite = NumberSprites[Score / 100];
            int rest = Score % 100;
            Tens.sprite = NumberSprites[rest / 10];
            Units.sprite = NumberSprites[rest % 10];
        }
    }

    private void ShowNegativeScore()
    {

        if (Score == 0)
        {
            (Tens.gameObject as GameObject).SetActive(false);
            Units.sprite = NumberSprites[Score];
        }
        else if (Score > -10)
        {
            //just draw units
            (Tens.gameObject as GameObject).SetActive(true);
            Tens.sprite = NumberSprites[10];
            Units.sprite = NumberSprites[-Score];
        }
        else if (Score <= -10 && Score > -100)
        {
            (Tens.gameObject as GameObject).SetActive(true);
            (Hundreds.gameObject as GameObject).SetActive(true);
            Hundreds.sprite = NumberSprites[10];
            Tens.sprite = NumberSprites[-Score / 10];
            Units.sprite = NumberSprites[-Score % 10];
        }
        else if (Score <= -100)
        {
            (Hundreds.gameObject as GameObject).SetActive(true);
            (Thousands.gameObject as GameObject).SetActive(true);
            Thousands.sprite = NumberSprites[10];
            Hundreds.sprite = NumberSprites[-Score / 100];
            int rest = -Score % 100;
            Tens.sprite = NumberSprites[rest / 10];
            Units.sprite = NumberSprites[rest % 10];
        }
    }

    int previousScore = -1;
    public Sprite[] NumberSprites;
    public SpriteRenderer Units, Tens, Hundreds, Thousands;
}
