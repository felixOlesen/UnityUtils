using UnityEngine;
using System.Collections.Generic;

public struct CellLayer
{
    public List<GridManager.TileType> tileTypes;
    public int cellLayerIndex; // Representative of Depth: Higher index == higher in the stack

    public CellLayer(List<GridManager.TileType> tileTypes, int cellLayerIndex) {
        this.tileTypes = tileTypes;
        this.cellLayerIndex = cellLayerIndex; // Set based on the tile types or other logic
    }
}

public class GridNode
{
    public Vector3Int position; // Position in the grid
    private List<CellLayer> cellLayers;

    public GridNode(Vector3Int position)
    {
        this.position = position;
    }

    public void AddCellLayer(List<GridManager.TileType> tileTypes, int cellLayerIndex) {
        if (cellLayers == null)
        {
            cellLayers = new List<CellLayer>();
        }
        cellLayers.Add(new CellLayer(tileTypes, cellLayerIndex));
        Debug.Log($"Adding tile at {position} with cell layer count {cellLayers.Count}, with type count '{tileTypes.Count}' to cell layer index {cellLayerIndex}");
                    
    }

    public bool HasCellLayer() {
        return cellLayers != null && cellLayers.Count > 0;
    }
}
