using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerControllerVisual : MonoBehaviour{
    [SerializeField]Tilemap targetTilemap;
    [SerializeField]TileBase tile;
    [SerializeField] private Player player;
    private Vector3Int markedCellPosition;
    private Vector3Int oldCellPosition;

    private void Update()
    {
        markedCellPosition = player.GetMouseGridPosition();
        targetTilemap.SetTile(oldCellPosition, null);
        targetTilemap.SetTile(markedCellPosition, tile);
        oldCellPosition = markedCellPosition;
    }
}
