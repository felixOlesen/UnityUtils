using UnityEngine;
using System;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "TileTypeMapping", menuName = "Scriptable Objects/TileTypeMapping")]
public class TileTypeMapping : ScriptableObject
{
    [Header("Associated Sprite Names")]
    [SerializeField]
    public List<string> SpriteNames;

    [Header("TileType Enum")]
    [SerializeField]
    public GridManager.TileType TileType;

    public HashSet<string> GetSpriteNameHashset() {
        return new HashSet<string>(SpriteNames);
    }

}
