using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutTile : MonoBehaviour {

    public bool walkable = true;
    public bool current = false;
    public bool target = false;
    public bool selectable = false;

    public List<tutTile> neighbors = new List<tutTile>();
    public bool visited = false;
    public tutTile parent = null;
    public int distance = 0;

    Vector3 raycastOffset = new Vector3(0, 0.25f, 0);
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (current) {
            GetComponentInChildren<Renderer>().material.color = Color.magenta;
        } else if (target) {
            GetComponentInChildren<Renderer>().material.color = Color.green;
        } else if (selectable) {
            GetComponentInChildren<Renderer>().material.color = Color.red;
        }
        else {
            GetComponentInChildren<Renderer>().material.color = Color.white;
        }
	}

    public void Reset() {
        neighbors.Clear();
        walkable = true;
        current = false;
        target = false;
        selectable = false;
        visited = false;
        parent = null;
        distance = 0;
    }

    public void FindNeighbors(float jumpheight) {
        //TODO we may use the jumpheight to set tiles that are simply so high they are unwalkable to use as walls(seems silly but perhaps??)
        Reset();
        //TODO figure out diaganals
        CheckTile(Vector3.forward * 2, jumpheight);
        CheckTile(-Vector3.forward * 2, jumpheight);
        CheckTile(Vector3.right * 2, jumpheight);
        CheckTile(-Vector3.right * 2, jumpheight);
    }

    public void CheckTile(Vector3 dir, float jumpHeight) {
        Vector3 half = new Vector3(.25f, (1 + jumpHeight) / 2, 0.25f);
        Collider[] colliders = Physics.OverlapBox(transform.position + dir, half);

        foreach (Collider item in colliders) {
            tutTile tile = item.GetComponentInParent<tutTile>();
            //Debug.Log(tile.name + " is the tile");
            if(tile  != null && tile.walkable) {
                RaycastHit hit;
                if (Physics.Raycast(tile.transform.position + raycastOffset, Vector3.up, out hit, 1)) { // if nothing is on Tile
                    Debug.Log(hit.collider.name + " was hit");
                }

                    if (!Physics.Raycast(tile.transform.position + raycastOffset, Vector3.up, out hit, 1)) { // if nothing is on Tile
                    neighbors.Add(tile);
                }
                else {
                    //Debug.Log(hit.collider.name);
                    
                }
                
            }
        }
    }
    //private void OnDrawGizmos() {
    //    //Physics.Raycast(target.transform.position, -Vector3.up, out hit, 1)
        
    //    Debug.DrawRay(gameObject.transform.position + raycastOffset, Vector3.up, Color.red, 1);
    //}
}


