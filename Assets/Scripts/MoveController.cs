using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveController : MonoBehaviour {
    bool beginTest;
    private ShotVector shot;
    private Quaternion angle;
    public GameObject playerref;

    private string type;
    private int delay;

    public void MovesSelected()
    {
        //selectedMoves moveOne = new selectedMoves();
    }

    public void test()
    {
        angle = shot.returnAngle();
        //selectedMoves moveOne = new selectedMoves("Shoot", angle, 0);
        selectedMoves moveOne = new selectedMoves("Shoot", angle, 0);
        if(moveOne.getType() == "Shoot")
        {
            beginTest = true;
            angle = moveOne.getAngle();
        }
    }
	
    public void startTest()
    {
        
    }
	void Start ()
    {
        shot = playerref.GetComponent<ShotVector>();
        //moveOne = new selectedMoves("Shoot", );
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (beginTest)
        {
            shot.shootbulletTest(angle);
            beginTest = false;
        }
	}
}
