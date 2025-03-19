using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Examine : MonoBehaviour
{
    Camera mainCam;//Camera Object Will Be Placed In Front Of
    GameObject clickedObject;//Currently Clicked Object
    public PauseAndInventoryMenu pauseAndInventoryMenu;

    //Holds Original Postion And Rotation So The Object Can Be Replaced Correctly
    Vector3 originaPosition;
    Vector3 originalRotation;

    //If True Allow Rotation Of Object
    public bool examineMode;
    // Controls the speed of the zoom
    private float scrollSpeed = 10f;
    

    void Start()
    {
        mainCam = Camera.main;// Get the main camera in the scene
        examineMode = false;// Set examine mode to false at the start
        pauseAndInventoryMenu = FindObjectOfType<PauseAndInventoryMenu>();
    }

    private void Update()
    {

        //ClickObject();//Decide What Object To Examine

        TurnObject();//Allows Object To Be Rotated

        ExitExamineMode();//Returns Object To Original Postion

        ZoomCamera();// Zoom camera
    }

    void ZoomCamera()
    {
        if (examineMode == true)  // Only zoom if in examine mode
        {
            if (mainCam.orthographic)// If the camera is in orthographic mode
            {
                mainCam.orthographicSize -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;// Zoom in/out based on scroll wheel input
            }
            else 
            {
                mainCam.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed; // Zoom in/out based on scroll wheel input
            }
  
        }
    }

    void ClickObject()
    {
        if (Input.GetKeyDown(KeyCode.F) && examineMode == false)// Check if F key is pressed and not already in examine mode
        {
            // Find the center of the screen in world space
            Vector3 screenCenter = new Vector3(mainCam.pixelWidth / 2f, mainCam.pixelHeight / 2f, 1f);
            Vector3 worldCenter = mainCam.ScreenToWorldPoint(screenCenter);

            // Cast a ray from the center of the screen forward
            RaycastHit hit;
            Ray ray = new Ray(worldCenter, mainCam.transform.forward);
            //Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);

            // If the ray hits an object with the "Item" tag
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.tag == "Item")
                {

                    //ClickedObject Will Be The Object Hit By The Raycast
                    clickedObject = hit.transform.gameObject;

                    //Save The Original Postion And Rotation
                    originaPosition = clickedObject.transform.position;
                    originalRotation = clickedObject.transform.rotation.eulerAngles;

                    //Now Move Object In Front Of Camera
                    clickedObject.transform.position = mainCam.transform.position + (transform.forward * 1f);

                    //Pause The Game
                    Time.timeScale = 0;

                    //Turn Examine Mode To True
                    examineMode = true;

                }

            }
        }
    }

    void TurnObject()
    {
        if (Input.GetMouseButton(0) && examineMode)// Check if left mouse button is held down and in examine mode
        {
            float rotationSpeed = 15;

            // Get mouse movement and rotate the object accordingly
            float xAxis = Input.GetAxis("Mouse X") * rotationSpeed;
            float yAxis = Input.GetAxis("Mouse Y") * rotationSpeed;

            clickedObject.transform.Rotate(Vector3.up, -xAxis, Space.World);
            clickedObject.transform.Rotate(Vector3.right, yAxis, Space.World);
        }
    }

    void ExitExamineMode()
    {
        // If the player is currently examining an object and there is no other object currently selected,
        // or if the right mouse button is being held down and the game is not paused, continue with the examine mode.
        if (examineMode && clickedObject == null || Input.GetMouseButton(1) && pauseAndInventoryMenu.gameIsPaused == false)
        {
            mainCam.fieldOfView = 60f;
            //Reset Object To Original Position

            if (clickedObject != null)
            {
                clickedObject.transform.position = originaPosition;
                clickedObject.transform.eulerAngles = originalRotation;
            }
           

            //Unpause Game
            Time.timeScale = 1;

            //Return To Normal State
            examineMode = false;

        }
    }
}
