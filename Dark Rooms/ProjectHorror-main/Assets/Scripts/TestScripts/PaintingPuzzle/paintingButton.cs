using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.VisualScripting;
using UnityEngine;

public class paintingButton : MonoBehaviour
{
    public int correctSequence = 514632;
    public int playerClicked;
    public int wrongClicks;

    public List<GameObject> paintings;
    public List<Sprite> eyesClosed;
    public List<Sprite> eyesOpen;
    public List<Sprite> scream;
    private SpriteRenderer rend;



    private void Start() 
    {
        playerClicked = 0;
        wrongClicks = 0;
    }


    public void ResetButton()
    {
        playerClicked = 0;
        wrongClicks = 0;

        for (int i = 0; i < paintings.Count; i++)
        {
            rend = paintings[i].GetComponent<SpriteRenderer>();
            rend.sprite = eyesOpen[i];
        }
    }

    public void button1Method()
    {
        int i = 0;
        rend = paintings[i].GetComponent<SpriteRenderer>();

        if (playerClicked == 5 && wrongClicks != 0)
        {
            rend.sprite = eyesClosed[i];
            playerClicked = 51;
        }

        if (playerClicked == 5 && wrongClicks == 0)
        {
            playerClicked = 51;
            rend.sprite = eyesClosed[i];
        }
        else if (playerClicked != 5 || wrongClicks != 0)
        {
            wrongClicks = wrongClicks + 1;
        }

        if (wrongClicks == 6)
        {
            WrongSequence();
        }
    }

    public void button2Method()
    {
        int i = 1;
        rend = paintings[i].GetComponent<SpriteRenderer>();

        if (playerClicked == 51463 && wrongClicks != 0)
        {
            rend.sprite = eyesClosed[i];
        }

        if (playerClicked == 51463 && wrongClicks == 0)
        {
            playerClicked = 514632;
            rend.sprite = eyesClosed[i];
        }
        else if (playerClicked != 51463 || wrongClicks != 0)
        {
            wrongClicks = wrongClicks + 1;
        }

        if (playerClicked == 514632)
        {
            CorrectSequenceMethod();
        }

        if (wrongClicks == 6)
        {
            WrongSequence();
        }
    }

    public void button3Method()
    {
        int i = 2;
        rend = paintings[i].GetComponent<SpriteRenderer>();

        if (playerClicked == 5146 && wrongClicks != 0)
        {
            rend.sprite = eyesClosed[i];
            playerClicked = 51463;
        }

        if (playerClicked == 5146 && wrongClicks == 0)
        {
            playerClicked = 51463;
            rend.sprite = eyesClosed[i];
        }
        else if (playerClicked != 5146 || wrongClicks != 0)
        {
            wrongClicks = wrongClicks + 1;
        }

        if (wrongClicks == 6)
        {
            WrongSequence();
        }
    }

    public void button4Method()
    {
        int i = 3;
        rend = paintings[i].GetComponent<SpriteRenderer>();

        if (playerClicked == 51 && wrongClicks != 0)
        {
            rend.sprite = eyesClosed[i];
            playerClicked = 514;
        }

        if (playerClicked == 51 && wrongClicks == 0)
        {
            playerClicked = 514;
            rend.sprite = eyesClosed[i];
        }
        else if (playerClicked != 51 || wrongClicks != 0)
        {
            wrongClicks = wrongClicks + 1;
        }

        if (wrongClicks == 6)
        {
            WrongSequence();
        }
    }

    public void button5Method()
    {
        int i = 4;
        rend = paintings[i].GetComponent<SpriteRenderer>();


        if (playerClicked == 0 && wrongClicks != 0)
        {
            rend.sprite = eyesClosed[i];
            playerClicked = 5;
        }

        if (playerClicked == 0 && wrongClicks == 0)
        {
            playerClicked = 5;
            rend.sprite = eyesClosed[i];
        }
        else if (playerClicked != 0 || wrongClicks != 0)
        {
            wrongClicks = wrongClicks + 1;
        }

        if (wrongClicks == 6)
        {
            WrongSequence();
        }
    }

    public void button6Method()
    {
        int i = 5;
        rend = paintings[i].GetComponent<SpriteRenderer>();

        if (playerClicked == 514 && wrongClicks != 0)
        {
            rend.sprite = eyesClosed[i];
            playerClicked = 5146;
        }

        if (playerClicked == 514 && wrongClicks == 0)
        {
            playerClicked = 5146;
            rend.sprite = eyesClosed[i];
        }
        else if (playerClicked != 514 || wrongClicks != 0)
        {
            wrongClicks = wrongClicks + 1;
        }

        if (wrongClicks == 6)
        {
            WrongSequence();
        }
    }

    private void CorrectSequenceMethod()
    {
        playerClicked = 0;
        wrongClicks = 0;

        Debug.Log("congrats, you got it pal");
    }

    private void WrongSequence()
    {
        for (int i = 0; i < paintings.Count; i++)
        {
            rend = paintings[i].GetComponent<SpriteRenderer>();
            rend.sprite = scream[i];
        }

        Debug.Log("you in trouble now..");
    }
}
