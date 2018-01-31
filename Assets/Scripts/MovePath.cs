using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePath : MonoBehaviour {
    //object references
    [SerializeField]
    private LineRenderer lineVector;
    [SerializeField]
    private Transform Player;
    [SerializeField]
    private int speed;
    private PlayerMoveProto movePlayer;
    

    //fields
    public List<Vector2> wayPoints;
    public float lineTolerance;
    //control states
    private bool mouseIsPressed;
    private bool selectMove = false;
    private bool moveObject = false;


    void playerMovementSelectLine()
    {
        //this is the part that sets the line for the player to follow
        if (Input.GetMouseButtonDown(0))
        {
            //events for when the mouse is pressed
            mouseIsPressed = true;
            //when the mouse button is pressed down the first thing
            //that happens is everything is set back to 0
            lineVector.positionCount = 0;
            wayPoints.RemoveRange(0, wayPoints.Count);
        }
        //once the mouse button is released the line gets simplified
        if (Input.GetMouseButtonUp(0))
        {
            mouseIsPressed = false;
            lineVector.Simplify(lineTolerance);
            wayPoints.RemoveRange(0, wayPoints.Count);
            for (int i = 0; i < lineVector.positionCount; i++)
            {
                wayPoints.Add(lineVector.GetPosition(i));
            }
        }
        //while the mouse buttton is pressed we take input form the mouse position
        if (mouseIsPressed)
        {

            //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //notice this is nested in 
            if (!wayPoints.Contains(Camera.main.ScreenToWorldPoint(Input.mousePosition)))//if my list does not contain the current mouse postion
            {
                //this if loop is to attach the first point of the line to the player
                if (wayPoints.Count == 0)
                {
                    //we set the vector count to 1(unity keeps bitching)
                    lineVector.positionCount = 1;
                    //we set the position of the transform to the first index
                    lineVector.SetPosition(0, Player.transform.position);
                    //then we add the transform to the waypointlist, this also gets us out of the loop
                    wayPoints.Add(Player.transform.position);
                }
                //add the current mouse position
                wayPoints.Add(Camera.main.ScreenToWorldPoint(Input.mousePosition));
                //set the index size of the line renderer to the index size of the list
                lineVector.positionCount = wayPoints.Count;
                //set the input from the list to the line renderer 
                //besure to cast the point being added as vector 2
                //syntax: set value(listindex-1, (cast)list[indexlast - 1])
                lineVector.SetPosition(wayPoints.Count - 1,
                    (Vector2)wayPoints[wayPoints.Count - 1]);
            }
        }
        //now we add a controller function to accept changes and make the player move
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            //listIterator = 0;
            //turn on the loop to move the player along the line
            //playerMoveFinalized = true;
            //clear the line
            lineVector.positionCount = 0;
            moveObject = true;
            selectMove = false;
            //turn off this loop to exit and allow other input
        }*/

    } 


    public void moveAction()
    {
        wayPoints.RemoveRange(0, wayPoints.Count);
        lineVector.positionCount = 0;
        selectMove = true;
    }

    private void Start()
    {
        movePlayer = this.GetComponent<PlayerMoveProto>();
    }
    
    void Update ()
    {
        if (selectMove)
        {
            playerMovementSelectLine();
        }
        /*if (moveObject)
        {
            moveObject = movePlayer.playerMoveOnLine(wayPoints, speed);
        }*/
        
	}
}
