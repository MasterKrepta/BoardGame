using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMove : TacticsMove {

    GameObject target;
    // Use this for initialization
    void Start () {
          Init();
    }
	
	// Update is called once per frame
	void Update () {
        if (!turn) {
            return;
        }


        if (!moving) {
            FindNearestTarget();
            //Debug.Log("got our player");
            CalculatePath();
            //Debug.Log("got our path");
            FindSelectableTiles();
            //Debug.Log("got our selectables");
            actualTargetTile.target = true;
        }
        else {
            Move();
        }

    }
    private void FindNearestTarget() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        GameObject nearest = null;

        float distance = Mathf.Infinity;

        foreach (GameObject obj in targets) {
            float d = Vector3.Distance(transform.position, obj.transform.position);

            if (d < distance) {
                distance = d;
                nearest = obj;
            }
        }

        target = nearest;
        //target = GameObject.FindGameObjectWithTag("Player");
    }
    private void CalculatePath() {
        tutTile targetTile = GetTargetTile(target);
        FindPath(targetTile);
    }

    
}

