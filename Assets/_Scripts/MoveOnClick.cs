using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveOnClick : MonoBehaviour {

    public Tile startingTile;
    [SerializeField] Tile selectedTile;
    Tile oldSelected;
    [SerializeField] Tile currentTile;

    List<Tile> tilesInRange = new List<Tile>();
    DisplayStats stats;

    private void Awake() {
        currentTile = startingTile;
        selectedTile = currentTile;
        stats = GetComponent<DisplayStats>();
    }

    void DrawMovementRange() {

    }
    private void Update() {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 100)) {

            Tile tileHit = hit.collider.GetComponentInParent<Tile>();
            if (tileHit != null && Input.GetMouseButtonUp(0)) {
                if (oldSelected != null)
                    oldSelected.SelectTile();
                selectedTile = tileHit;
                selectedTile.SelectTile();
                oldSelected = selectedTile;
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
    }

}
