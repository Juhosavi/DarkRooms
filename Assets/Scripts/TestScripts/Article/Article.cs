using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Article : MonoBehaviour
{
    
    public GameObject closedDoor;
    public GameObject brokenDoor;
    public GameObject player;
    public GameObject articleText;

    private float distanceToClue;
    private bool touchedClue = false;
    public bool pickUpFirstTime = false;


    private void Start() 
    {
        // closedDoor= GameObject.FindGameObjectWithTag("ClosedDoor");
        //brokenDoor = Resources.FindObjectsOfTypeAll<BrokenDoor>();
        player = GameObject.FindGameObjectWithTag("Player");
        // articleText = GameObject.FindGameObjectWithTag("ArticleText");
        pickUpFirstTime = false;

        touchedClue = false;
        
    }

    private void OnMouseDown() 
    {

    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        distanceToClue = Vector3.Distance(this.gameObject.transform.position, player.transform.position);


        if (distanceToClue < 2.5f && Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Article" && touchedClue == false)
        {
            articleText.SetActive(true);
            touchedClue = true;
            Time.timeScale = 0f;

        }
        else if (distanceToClue < 2.5f && Physics.Raycast(ray, out hit) && hit.collider.gameObject.tag == "Article" && touchedClue == true)
        {
            pickUpFirstTime = true;
            Time.timeScale = 1f;
            articleText.SetActive(false);
            touchedClue = false;
            closedDoor.SetActive(false);
            brokenDoor.SetActive(true);
            
        }
        
    }

    private void Update() 
    {
        if (touchedClue == true && Input.GetKey(KeyCode.Escape))
        {
            pickUpFirstTime = true;
            articleText.SetActive(false);
            touchedClue = false;
            closedDoor.SetActive(false);
            brokenDoor.SetActive(true);

        }

        if (touchedClue == true && Input.GetKey(KeyCode.E))
        {
            pickUpFirstTime = true;
            articleText.SetActive(false);
            touchedClue = false;
            closedDoor.SetActive(false);
            brokenDoor.SetActive(true);
            Time.timeScale = 1f;

        }
    }


}
