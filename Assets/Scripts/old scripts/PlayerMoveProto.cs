using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveProto : MonoBehaviour
{
    int listIterator = 0;
    public bool playerMoveOnLine(List<Vector2> wayPoints, int speed)
    {
        this.transform.position = Vector2.MoveTowards(this.transform.position,
            wayPoints[listIterator], speed * Time.deltaTime);
        if ((Vector2)this.transform.position == wayPoints[listIterator])
        {
            listIterator++;
        }
        if ((Vector2)this.transform.position == wayPoints[wayPoints.Count - 1])
        {
            wayPoints.RemoveRange(0, wayPoints.Count);
            listIterator = 0;
            return false;

        }
        
        return true;
    }
}
