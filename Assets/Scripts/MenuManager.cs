using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    private GameObject moveMenu;
    private Canvas moveMenuCanvas;
    [SerializeField]
    private GameObject confirmMenu;
    private Canvas confirmMenuCanvas;
    [SerializeField]
    private bool moveMenuActive = false;
    [SerializeField]
    private bool confirmMenuActive = false;
    [SerializeField]
    private GameObject activeObject;

    movementLineMaker movementLine;
    [SerializeField]
    private GameObject masterLineRender;

    //instantiate menus
    public void instanceMoveMenu(GameObject objectReference)
    {
        moveMenuCanvas.enabled = true;
        activeObject = objectReference;
        moveMenuActive = true;

    } 
    public void instanceConfirmMenu()
    {
        confirmMenuCanvas.enabled = true;
        confirmMenuActive = true;
    }
    //destroy menus
    public void deactiveMoveMenu()
    {
        moveMenuCanvas.enabled = false;
        activeObject = null;
        moveMenuActive = false;
    }
    public void deactiveConfirmMenu()
    {
        confirmMenuCanvas.enabled = false;
        confirmMenuActive = false;
    }
    

    //setter
    public void setMoveMenuStatus(bool status)
    {
        moveMenuActive = status;
    }
    public void setConfirmMenuStatus(bool status)
    {
        confirmMenuActive = status;
    }
    public void setActiveObject(GameObject objectReference)
    {
        activeObject = objectReference;
    }
    //getters
    public bool getMoveMenuStatus()
    {
        return moveMenuActive;
    }
    public bool getconfirmMenuStatus()
    {
        return confirmMenuActive;
    }
    public GameObject getActiveObject()
    {
        return activeObject;
    }


    public void createMovementLine()
    {
        movementLine.moveAction(activeObject);
    }


    private void Start()
    {
        moveMenuCanvas = moveMenu.GetComponent<Canvas>();
        confirmMenuCanvas = confirmMenu.GetComponent<Canvas>();
        movementLine = masterLineRender.GetComponent<movementLineMaker>();
        confirmMenuCanvas.enabled = false;
        moveMenuCanvas.enabled = false;
    }

}
