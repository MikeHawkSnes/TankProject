using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankObjectManager : MonoBehaviour {
    [SerializeField]
    private GameObject GMreference;
    private MenuManager Menu;

    private void OnMouseDown()
    {
        if (!Menu.getMoveMenuStatus())
        {
            Menu.instanceMoveMenu();
        }

        
    }
    // Use this for initialization
    void Start()
    {
        Menu = GMreference.GetComponent<MenuManager>();

	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
