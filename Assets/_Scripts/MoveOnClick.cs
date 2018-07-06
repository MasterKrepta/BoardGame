using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnClick : MonoBehaviour {

    int tilesize = 2; // The width of each tile

    public Tile startingTile;
    [SerializeField] Tile selectedTile;
    Tile oldSelected;
    [SerializeField] Tile currentTile;
    [SerializeField] LayerMask tileLayer;
    List<Tile> tilesInRange = new List<Tile>();
    DisplayStats displayStats;

    private void Awake() {
        Utilities.Tiles = FindObjectsOfType<Tile>();
        currentTile = startingTile;
        selectedTile = currentTile;
        displayStats = GetComponent<DisplayStats>();
        

    }

    private void Start() {
        Move(currentTile);
    }

    void DrawMovementRange() {
        Collider[] tilesInRange = Physics.OverlapSphere(transform.position, displayStats.stats.Movement * tilesize,tileLayer);
        foreach (Collider tile in tilesInRange) {
            tile.GetComponentInParent<Tile>().Selectable();
        }
        
    }
    private void Update() {
        
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100)) {

            Tile tileHit = hit.collider.GetComponentInParent<Tile>();
            if(tileHit != null && tileHit.selectable) {
                //tileHit.GetComponentInChildren<MeshRenderer>().material = tileHit.GetComponentInChildren<Tile>().materials[1];
                if (Input.GetMouseButtonUp(0) && tileHit.selectable) {
                    if (oldSelected != null)
                        oldSelected.SelectTile();
                    selectedTile = tileHit;
                    selectedTile.SelectTile();
                    oldSelected = selectedTile;
                }
            }
        }

        if (Input.GetMouseButtonUp(1)) {
            Move(selectedTile);
        }
    }

    void Move(Tile tileToMoveTo) {
        this.transform.position = tileToMoveTo.transform.position;
        currentTile = tileToMoveTo;
        tileToMoveTo.ClearSelected();
        //Utilities.ClearSelected();
        DrawMovementRange();
    }

    //private void OnDrawGizmosSelected() {
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, displayStats.stats.Movement * tilesize);

    //}
}
