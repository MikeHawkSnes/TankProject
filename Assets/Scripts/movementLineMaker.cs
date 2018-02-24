using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movementLineMaker : MonoBehaviour
{
    //private fields
    [SerializeField]
    private LineRenderer lineVector;                            //add the line renderer here
    [SerializeField]
    private GameObject activePlayer;                            //this will be used to get the player reference so we can get the transform for the initial point
    [SerializeField]
    private List<Vector2> wayPoints;                            //the list that we will pass on as a return
    private bool mouseIsPressed;                                //check if mouse is pressed
    [SerializeField]
    private bool createLine = false;
    [SerializeField]
    private float lineTolerance;
    private Transform playerTransform;

    private void playerMovementLine()
    { 
        if (Input.GetMouseButtonDown(0))
        {
            mouseIsPressed = true;                              //when the mouse button is pressed down the first thing          
            lineVector.positionCount = 0;                       //that happens is everything is set back to 0
            wayPoints.RemoveRange(0, wayPoints.Count);
        }
        if (Input.GetMouseButtonUp(0))                          //once the mouse button is released the line gets simplified
        {
            mouseIsPressed = false;
            lineVector.Simplify(lineTolerance);
            wayPoints.RemoveRange(0, wayPoints.Count);
            for (int i = 0; i < lineVector.positionCount; i++)
            {
                wayPoints.Add(lineVector.GetPosition(i));
            }
            createLine = false;
        }
        if (mouseIsPressed)                                                                 //while the mouse buttton is pressed we take input form the mouse position
        {
            if (!wayPoints.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))   //translation: if my list does not contain the current mouse postion
            {
                if (wayPoints.Count == 0)                                                   //this if loop is to attach the first point of the line to the player
                {
                    lineVector.positionCount = 1;                                           //we set the vector count to 1(unity keeps bitching) 
                    lineVector.SetPosition(0, playerTransform.transform.position);          //we set the position of the transform to the first index
                    wayPoints.Add(playerTransform.transform.position);                      //then we add the transform to the waypointlist, this also gets us out of this block
                }
                wayPoints.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));         //add the current mouse position
                lineVector.positionCount = wayPoints.Count;                                 //set the index size of the line renderer to the index size of the list
                lineVector.SetPosition(wayPoints.Count - 1,
                    (Vector2)wayPoints[wayPoints.Count - 1]);
            }
        }
    }
    public void flushLineMaker()//flushes the line maker so it can start at 0 for the next time it's called
    {
        lineVector.positionCount = 0;
    }

    public void moveAction(GameObject playerRef)
    {
        playerTransform = playerRef.GetComponent<Transform>();//this function takes the playerRef parameter and uses it to make a reference to the object's transform for the initial point of the line
        Input.ResetInputAxes();
        createLine = true;
    }
    // Use this for initialization
    void Start(){}

    // Update is called once per frame
    void Update()
    {
        if (createLine)
            playerMovementLine();
    }
}