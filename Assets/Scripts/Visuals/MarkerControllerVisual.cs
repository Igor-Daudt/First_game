using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MarkerControllerVisual : MonoBehaviour{
    [SerializeField]Tilemap targetTilemap;
    [SerializeField]TileBase tile;
    private Vector3Int markedCellPosition;
    private Vector3Int oldCellPosition;

    private void Update()
    {
        markedCellPosition = TilemapController.instance.GetGridPosition(GameInput.instance.GetMouseCoordinates(), TilemapController.SCREEN_POSITION);
        targetTilemap.SetTile(oldCellPosition, null);
        targetTilemap.SetTile(markedCellPosition, tile);
        oldCellPosition = markedCellPosition;
    }
}
