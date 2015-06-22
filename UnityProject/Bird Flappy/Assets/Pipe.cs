using System;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Pipe : MonoBehaviour {
    public enum PrimaryPipeType
    {
        Normal,
        Increaser
    }

    public enum SecondaryPipeType
    {
        Normal,
        Plus3
    }


    public  Sprite PrimaryInverse;
    public  Sprite PrimaryNormal;
    public  Sprite SecondaryPlus3;


    public PrimaryPipeType PrimaryType { get; set; }
    public SecondaryPipeType SecondaryType { get; set; }
	// Use this for initialization
	void Start () {
	    GenerateRandomPipe();
	}

    void GenerateRandomPipe()
    {
        float random = Random.Range(0f, 100f);
        if (random <= 70)
        {
            this.PrimaryType = PrimaryPipeType.Normal;
        }
        else if (random >= 70) //&& random <= 80)
        {
            this.PrimaryType = PrimaryPipeType.Increaser;
        }
        random = Random.Range(0f, 100f);
        if(random >= 80)
            this.SecondaryType = SecondaryPipeType.Plus3;
        //else if (random >= 80)
        //{
        //    this.PrimaryType = PrimaryPipeType.Decreaser;
        //}
        //random = Random.Range(0f, 100f);
        //if (random <= 60)
        //{
        //    this.SecondaryType = SecondaryPipeType.Normal;
        //}
        //else
        //{
        //    this.SecondaryType = SecondaryPipeType.Plus3;
        //}

        SpriteRenderer[] spriteRenderers = this.GetComponentsInChildren<SpriteRenderer>();
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i].name == "PipeSprite")
            {
                spriteRenderers[i].sprite = GetPrimarySprite(this.PrimaryType);
            }
            else if (spriteRenderers[i].name == "PipeText")
            {
                spriteRenderers[i].sprite = GetSecondarySprite(this.SecondaryType);
            }
        }
    }

    private Sprite GetPrimarySprite(PrimaryPipeType type)
    {
        if (type == PrimaryPipeType.Increaser)
            return PrimaryInverse;
        return PrimaryNormal;
    }

    private Sprite GetSecondarySprite(SecondaryPipeType type)
    {
        if (type == SecondaryPipeType.Plus3)
            return SecondaryPlus3;
        return null;
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
