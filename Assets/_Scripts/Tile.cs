using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour {

    [SerializeField]
    List<Material> materials = new List<Material>();
    private Material material;
    [SerializeField] private bool selected = false;
    Tile[] tiles;

    [SerializeField] int moveCost = 1;

    private Material originalMaterial;

    MeshRenderer meshRenderer;
    private void Awake() {
        tiles = FindObjectsOfType<Tile>();
        meshRenderer = GetComponentInChildren<MeshRenderer>();
        material = meshRenderer.material;
        originalMaterial = material;
    }
    private void OnMouseOver() {
        meshRenderer.material = materials[1];
    }

    private void OnMouseExit() {
        if(!selected)
            meshRenderer.material = materials[0];
    }

    public void SelectTile() {
        selected = !selected;
    }

    public void ClearSelected() {
        foreach (Tile tile in tiles) {
                tile.selected = false;
                meshRenderer.material = materials[0];
        }
    }
}
