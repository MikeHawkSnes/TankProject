using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour {
    //gameObjects
    LineRenderer lineVector;
    public GameObject bulletShot;

    //controller states
    private bool selectMove = false;
    public bool selectShoot = false;
    //private bool selectEnd = false;
    private bool shotPhase = false;
    private bool playerMoveFinalized;

    private int listIterator;

    private bool mouseIsPressed = false;
    private Vector2 mousePosition;
    [SerializeField]
    private List<Vector2> wayPoints;

    public float speed;
    public float lineTolerance;
    Quaternion shotQuaternion;



	// Use this for initialization
	void Start ()
    {
        lineVector = gameObject.GetComponent<LineRenderer>();
        listIterator = 0;
	}

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
            for(int i = 0; i < lineVector.positionCount; i++)
            {
                wayPoints.Add(lineVector.GetPosition(i));
            }
        }
        //while the mouse buttton is pressed we take input form the mouse position
        if (mouseIsPressed)
        {

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //notice this is nested in 
            if (!wayPoints.Contains(mousePosition))//if my list does not contain the current mouse postion
            {
                //this if loop is to attach the first point of the line to the player
                if(wayPoints.Count == 0)
                {
                    //we set the vector count to 1(unity keeps bitching)
                    lineVector.positionCount = 1;
                    //we set the position of the transform to the first index
                    lineVector.SetPosition(0, transform.position);
                    //then we add the transform to the waypointlist, this also gets us out of the loop
                    wayPoints.Add(transform.position);
                }
                //add the current mouse position
                wayPoints.Add(mousePosition);
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            listIterator = 0;
            //turn on the loop to move the player along the line
            playerMoveFinalized = true;
            //clear the line
            lineVector.positionCount = 0;
            //turn off this loop to exit and allow other input
            selectMove = false;
        }


    }  //line Renderer

    void playerMoveOnLine() 
    {
        transform.position = Vector2.MoveTowards(transform.position,
            wayPoints[listIterator], speed * Time.deltaTime);
        if ((Vector2)transform.position == wayPoints[listIterator])
        {
            listIterator++;
        }
        if ((Vector2)transform.position == wayPoints[wayPoints.Count-1])
        {
            playerMoveFinalized = false;
        }
    }

    public void playerShootVector() //modified playermovement mouse control for single line vector
    {
        Debug.Log("Shoot");
        if (Input.GetMouseButtonDown(0))
        {
            mouseIsPressed = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            mouseIsPressed = false;
            lineVector.Simplify(1);
        }
        //while the mouse buttton is pressed we take input form the mouse position
        if (mouseIsPressed)
        {
            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineVector.positionCount = 2;
            lineVector.SetPosition(0, transform.position);
            lineVector.SetPosition(1,mousePosition);
        }
        //now we add a controller function to accept changes and make the player move
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shotQuaternion = playerShotAngle(lineVector.GetPosition(0), lineVector.GetPosition(1));
            lineVector.positionCount = 0;
            selectShoot = false;
            shotPhase = true;
            
        }
    }
    public void shootbutton()
    {
        Debug.Log("shootbutton");
        selectShoot = true;
    }

    private Quaternion playerShotAngle(Vector2 initialPosition, Vector2 endPosition)//returns the quaternion of an line from point A to point B
    {
        //calculate the angle using mathf functions and the shotvector which is transform of the line end - transform of the player
        Vector2 shotVector = new Vector2(initialPosition.x - endPosition.x, endPosition.y - initialPosition.y);
        float shotAngle = Mathf.Atan2(shotVector.x, shotVector.y) * Mathf.Rad2Deg;
        //shot angle is then converted into shotangle quaternion so we can use it on other things like instantiate
        Quaternion finalQuaternion = Quaternion.AngleAxis(shotAngle, Vector3.forward);
        return finalQuaternion;
    }
	
	// Update is called once per frame
	void Update ()
    {
        //quick menu selections
        if (Input.GetKeyDown(KeyCode.A))
        {
            selectMove = true;
        }  //selectMove = True
        if (Input.GetKeyDown(KeyCode.S))
        {
            selectShoot = true;
        } //selectShoot = True
        if (Input.GetKeyDown(KeyCode.D))
        {
            //selectEnd = true;
        }//quick menu selections end

        if(selectMove == true)
        {
            playerMovementSelectLine();
        } //playerMovementSelectLine()
        if(playerMoveFinalized == true)
        {
            playerMoveOnLine();
        } //playerMoveOnLine()

        if(selectShoot == true)
        {
            playerShootVector();
        } //playerShootVector()
        if(shotPhase == true)
        {
            Instantiate(bulletShot, transform.position, shotQuaternion);
            shotPhase = false;
        }


	}
}

