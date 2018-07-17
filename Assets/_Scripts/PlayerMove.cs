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
        FindSelectableTiles();
	}
}
