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


    public void SelectTile() {
        selected = !selected;

    }

    public void ClearSelected() {

        foreach (Tile tile in Utilities.Tiles) {
            tile.selected = false;
            tile.selectable = false;
            tile.meshRenderer.material = materials[0];
        }
    }

    public void Selectable() {
        this.selectable = true;
        meshRenderer.material = materials[2];
    }
}
