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


    //instantiate menus
    public void instanceMoveMenu()
    {
        moveMenuCanvas.enabled = true;
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
    //getters
    public bool getMoveMenuStatus()
    {
        return moveMenuActive;
    }
    public bool getconfirmMenuStatus()
    {
        return confirmMenuActive;
    }

    private void Start()
    {
        moveMenuCanvas = moveMenu.GetComponent<Canvas>();
        confirmMenuCanvas = confirmMenu.GetComponent<Canvas>();

        confirmMenuCanvas.enabled = false;
        moveMenuCanvas.enabled = false;
    }

}
