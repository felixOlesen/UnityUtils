using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System;

[Serializable]
public struct TilemapLayer
{
    public string layerName;
    public Tilemap tilemap;
    public int TilemapLayerIndex; // Representative of Depth: Higher index == higher in the stack
}

public class GridManager : MonoBehaviour
{
    public static GridManager Singleton { get; private set; }

    public enum TileType
    {
        Grass,
        Aqueduct,
        Building,
        Water,
        // Add more tile types as needed
    }

    [Header("Tilemap Layers")]
    [SerializeField]
    public List<TilemapLayer> tilemapLayers;

    [Header("Tile Type Mappings")]
    [SerializeField]
    public List<TileTypeMapping> tileTypeMappings;

    public Dictionary<TileType, HashSet<string>> tileTypeToSpriteNames = new Dictionary<TileType, HashSet<string>>();
    
    public Dictionary<Vector3Int, GridNode> gridNodes = new Dictionary<Vector3Int, GridNode>();

    void Awake() {
        // Create Singleton instance
        if (Singleton == null) {
            Singleton = this;
        } else {
            Debug.LogWarning("[WARNING] Multiple instances of GridManager detected. Destroying duplicate.");
            Destroy(gameObject);
            return;
        }
        // Initialize tile type mappings
        PopulateTileTypeMappings();

        // Scan the tilemaps for populate grid cells
        ScanTilemaps();
    }

    private void PopulateTileTypeMappings() {
        foreach(TileTypeMapping mapping in tileTypeMappings) {
            if(mapping.SpriteNames == null || mapping.SpriteNames.Count == 0) {
                Debug.LogWarning($"[WARNING] TileTypeMapping for '{mapping.TileType}' has no associated sprite names.");
                continue;
            }
            tileTypeToSpriteNames[mapping.TileType] = mapping.GetSpriteNameHashset();
        }
    }

    private void ScanTilemaps() {
        if(tilemapLayers == null || tilemapLayers.Count == 0) {
            Debug.LogWarning("[WARNING] No tilemap layers assigned to GridManager.");
            return;
        }
        foreach(TilemapLayer layer in tilemapLayers) {
            if(layer.tilemap == null) {
                Debug.LogWarning($"[WARNING] Tilemap for layer '{layer.layerName}' is not assigned.");
                continue;
            }
            foreach (Vector3Int localPos in layer.tilemap.cellBounds.allPositionsWithin) {
                if (layer.tilemap.HasTile(localPos)) {
                    TileBase tile = layer.tilemap.GetTile(localPos);
                    Sprite sprite = layer.tilemap.GetSprite(localPos);
                    if (tile != null && sprite != null) {
                        // Process the tile and populate grid cells as needed
                        
                        if(!gridNodes.ContainsKey(localPos)) {
                            gridNodes[localPos] = new GridNode(localPos);
                        }
                        gridNodes[localPos].AddCellLayer(GetTileTypesForSprite(sprite.name), layer.TilemapLayerIndex);
                    }
                }
            }
        }
    }

    private List<TileType> GetTileTypesForSprite(string spriteName) {
        List<TileType> matchingTileTypes = new List<TileType>();
        foreach(var kvp in tileTypeToSpriteNames) {
            if(kvp.Value.Contains(spriteName)) {
                matchingTileTypes.Add(kvp.Key);
            }
        }
        return matchingTileTypes;
    }
}
