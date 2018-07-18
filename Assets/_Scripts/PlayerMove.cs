using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : TacticsMove {

    DisplayStats displayStats;
    
    // Use this for initialization
    void Start () {
        Init();
        displayStats = GetComponent<DisplayStats>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!moving) {
            FindSelectableTiles();
            CheckMouse();
        }
        else {
            //TODO move()
        }
        
	}

    private void CheckMouse() {
        if (Input.GetMouseButtonUp(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if(Physics.Raycast(ray, out hit)) {
                if(hit.collider.GetComponentInParent<tutTile>()) {
                    tutTile t = hit.collider.GetComponentInParent<tutTile>();
                    if (t.selectable) {
                        MoveToTile(t);
                    }
                }
            }
        }
    }
}
