using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotVector : MonoBehaviour
{
    //object references
    [SerializeField]
    private LineRenderer lineVector;
    [SerializeField]
    private Transform Player;
    //fields
    [SerializeField]
    private GameObject bulletShot;    //change to send the code to the player script(quaternion) so the player will be the one to shoot
    public Quaternion shotQuaternion;
    //controller states
    private bool mouseIsPressed;
    private bool selectShoot = false;
 

    public Quaternion returnAngle()
    {
        return shotQuaternion;
    }
    private void playerShootVector() //modified playermovement mouse control for single line vector
    {
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
            //mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lineVector.positionCount = 2;
            lineVector.SetPosition(0, Player.transform.position);
            lineVector.SetPosition(1, Camera.main.ScreenToWorldPoint(Input.mousePosition));// mousePosition);
        }
        //now we add a controller function to accept changes and make the player move
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shotQuaternion = playerShotAngle(lineVector.GetPosition(0), lineVector.GetPosition(1));
            Debug.Log(shotQuaternion);
            lineVector.positionCount = 0;
            //Instantiate(bulletShot, Player.transform.position, shotQuaternion);
            selectShoot = false;
        }
    }

    private Quaternion playerShotAngle(Vector2 initialPosition, Vector2 endPosition)//returns the quaternion of an line from point A to point B
    {
        //calculate the angle using mathf functions and the shotvector which is transform of the line end - transform of the player            
        float shotAngle = Mathf.Atan2((initialPosition.x - endPosition.x), (endPosition.y - initialPosition.y)) * Mathf.Rad2Deg;
    
        //shot angle is then converted into shotangle quaternion so we can use it on other things like instantiate
        Quaternion finalQuaternion = Quaternion.AngleAxis(shotAngle, Vector3.forward);
        return finalQuaternion;
    }

    public void shootAction()
    {
        selectShoot = true;
    }
    public void shootbulletTest(Quaternion bulletangle)
    {
        Instantiate(bulletShot, Player.transform.position, bulletangle);
    }

    private void Update()
    {
        if (selectShoot)
        {
            playerShootVector();
        }
    }
}