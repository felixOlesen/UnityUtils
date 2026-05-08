using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    private Vector2 movementInput;
    private float isometricBlockOffset_X = 0.5f; // Adjust this value based on your isometric tile size
    private float isometricBlockOffset_Y = 0.25f; // Adjust this value based on your isometric tile size
    public Vector3Int positionOnGrid;
    private float currentHeightOffset = 0.5f;
    void Awake()
    {
        // Subscribe to the movement event from the InputDispatcher
        InputDispatcher.OnMoveEvent += OnMoveEvent;
    }
    
    void Start() 
    {
            if(CheckTileMapBounds(transform.position)) {
                // Initialise position on grid
                positionOnGrid = GridManager.Singleton.GetPositionOnGrid(transform.position);
                transform.position = new Vector3(0f, currentHeightOffset, 0f);
            } else {
                // If position not valid, set to Vector3Int(0, 0, 0)
                Debug.LogWarning("[Player Controller] Invalid Start position for player.");
                transform.position = new Vector3(0f, currentHeightOffset, 0f);
                positionOnGrid = new Vector3Int(0, 0, 0);
            }
    }

    void OnMoveEvent(UnityEngine.InputSystem.InputValue inputValue)
    {
        // Update CellPosition as well
        // Right Arrow Key
        movementInput = inputValue.Get<Vector2>();
        // Debug.Log($"Received movement input: {movementInput} at position {transform.position}");
        Vector3 targetPosition = new Vector3(transform.position.x, transform.position.y - currentHeightOffset, 0f); // Start with current position
        float targetHeightOffset;
        float heightDifference;
        // Update cell position on grid
        switch (movementInput)
        {
            case Vector2 v when v == Vector2.up:
                Debug.Log("Moving Up");
                targetPosition += new Vector3(-isometricBlockOffset_X, isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    positionOnGrid += new Vector3Int((int)movementInput.x, (int)movementInput.y, 0);
                    targetHeightOffset = GetTopTileHeight(targetPosition);
                    transform.Translate(new Vector3(-isometricBlockOffset_X, isometricBlockOffset_Y, 0));
                    if(currentHeightOffset - targetHeightOffset != 0) {
                        heightDifference = targetHeightOffset - currentHeightOffset;
                        currentHeightOffset = targetHeightOffset;
                        Debug.Log($"HeighDiff: {heightDifference}");
                        transform.Translate(new Vector3(0f, heightDifference, 0f));
                    }
                }
                break;
            case Vector2 v when v == Vector2.down:
                Debug.Log("Moving Down");
                targetPosition += new Vector3(isometricBlockOffset_X, -isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    positionOnGrid += new Vector3Int((int)movementInput.x, (int)movementInput.y, 0);
                    targetHeightOffset = GetTopTileHeight(targetPosition);
                    transform.Translate(new Vector3(isometricBlockOffset_X, -isometricBlockOffset_Y, 0));
                    if(currentHeightOffset - targetHeightOffset != 0) {
                        heightDifference = targetHeightOffset - currentHeightOffset;
                        currentHeightOffset = targetHeightOffset;
                        transform.Translate(new Vector3(0f, heightDifference, 0f));
                    }
                }
                break;
            case Vector2 v when v == Vector2.left:
                Debug.Log("Moving Left");
                targetPosition += new Vector3(-isometricBlockOffset_X, -isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    positionOnGrid += new Vector3Int((int)movementInput.x, (int)movementInput.y, 0);
                    targetHeightOffset = GetTopTileHeight(targetPosition);
                    transform.Translate(new Vector3(-isometricBlockOffset_X, -isometricBlockOffset_Y, 0));
                    if(currentHeightOffset - targetHeightOffset != 0) {
                        heightDifference = targetHeightOffset - currentHeightOffset;
                        currentHeightOffset = targetHeightOffset;
                        transform.Translate(new Vector3(0f, heightDifference, 0f));
                    }
                }
                break;
            case Vector2 v when v == Vector2.right:
                Debug.Log("Moving Right");
                targetPosition += new Vector3(isometricBlockOffset_X, isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    positionOnGrid += new Vector3Int((int)movementInput.x, (int)movementInput.y, 0);
                    targetHeightOffset = GetTopTileHeight(targetPosition);
                    transform.Translate(new Vector3(isometricBlockOffset_X, isometricBlockOffset_Y, 0));
                    if(currentHeightOffset - targetHeightOffset != 0) {
                        heightDifference = targetHeightOffset - currentHeightOffset;
                        currentHeightOffset = targetHeightOffset;
                        transform.Translate(new Vector3(0f, heightDifference, 0f));
                    }
                }
                break;
            default:
                // Debug.Log("No movement input detected");
                break;
        }
    }

    bool CheckTileMapBounds(Vector3 targetPosition)
    {
        // Implement bounds checking logic here based on your tilemap dimensions
        // For example, you can check if the targetPosition is within the bounds of the tilemap
        // Change to world position if needed based on your tilemap setup
        
        return GridManager.Singleton.IsPositionValid(targetPosition); // Placeholder: Always return true for now

    }

    float GetTopTileHeight(Vector3 playerPos) {
        return GridManager.Singleton.FindTopTileHeightFromGridNode(playerPos);
    }    

}
