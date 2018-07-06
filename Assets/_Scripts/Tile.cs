using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField]
    private List<Material> materials = new List<Material>();
    private Material material;
    [SerializeField] private bool selected = false;
    Tile[] tiles;

    public bool selectable = false;
    [SerializeField] int moveCost = 1;

    public Material originalMaterial;

    public MeshRenderer meshRenderer;
    private void Awake() {
        tiles = FindObjectsOfType<Tile>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
        originalMaterial = material;
    }
    //private void OnMouseOver() {
    //    meshRenderer.material = materials[1];
    //}

    //private void OnMouseExit() {
    //    if(!selected)
    //        meshRenderer.material = materials[0];
    //}

    public void SelectTile() {
        selected = !selected;
        if(selected)
            meshRenderer.material = materials[1];
    }

    public void ClearSelected() {
        
        foreach (Tile tile in Utilities.Tiles) {
            tile.selected = false;
            tile.selectable = false;
            meshRenderer.material = materials[0];
        }
    }

    public void Selectable() {
        this.selectable = true;
        meshRenderer.material = materials[2];
    }
}
