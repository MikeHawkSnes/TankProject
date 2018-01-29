using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectedMoves : ScriptableObject{

    private List<Vector2> vectorReference;
    private string moveType;
    private int moveDelay = 0;//set it all to zero. may be usable in the future
    private Quaternion attackVector;

    public selectedMoves(string Type, List<Vector2> Reference, int delay)
    {
        moveType = Type;
        vectorReference = Reference;
        moveDelay = delay;        
    }
    public selectedMoves(string Type, Quaternion attack, int delay)
    {
        moveType = Type;
        attackVector = attack;
        moveDelay = delay;
    }

    //setters
    public void setType(string setter)
    {
        moveType = setter;
    }
    public void setDelay(int setter)
    {
        moveDelay = setter;
    }
    public void setMove(List<Vector2> setter)
    {
        vectorReference = setter;
    }
    public void setAttackAngle(Quaternion setter)
    {
        attackVector = setter;
    }

    //getters
    public string getType()
    {
        return moveType;
    }
    public List<Vector2> getVector()
    {
        return vectorReference;
    }
    public int getDelay()
    {
        return moveDelay;
    }
    public Quaternion getAngle()
    {
        return attackVector;
    }

	
}
