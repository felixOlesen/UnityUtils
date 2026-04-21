using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Transform playerTransform;
    private Vector2 movementInput;
    private float isometricBlockOffset_X = 0.5f; // Adjust this value based on your isometric tile size
    private float isometricBlockOffset_Y = 0.25f; // Adjust this value based on your isometric tile size

    void Awake()
    {
        playerTransform = GetComponent<Transform>();

        // Subscribe to the movement event from the InputDispatcher
        InputDispatcher.OnMoveEvent += OnMoveEvent; 
    }

    void OnMoveEvent(UnityEngine.InputSystem.InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        Debug.Log($"Received movement input: {movementInput} at position {playerTransform.position}");
        Vector3 targetPosition = playerTransform.position; // Start with current position
        switch (movementInput)
        {
            case Vector2 v when v == Vector2.up:
                Debug.Log("Moving Up");
                targetPosition += new Vector3(-isometricBlockOffset_X, isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    playerTransform.Translate(new Vector3(-isometricBlockOffset_X, isometricBlockOffset_Y, 0));
                }
                break;
            case Vector2 v when v == Vector2.down:
                Debug.Log("Moving Down");
                targetPosition += new Vector3(isometricBlockOffset_X, -isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    playerTransform.Translate(new Vector3(isometricBlockOffset_X, -isometricBlockOffset_Y, 0));
                }
                break;
            case Vector2 v when v == Vector2.left:
                Debug.Log("Moving Left");
                targetPosition += new Vector3(-isometricBlockOffset_X, -isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    playerTransform.Translate(new Vector3(-isometricBlockOffset_X, -isometricBlockOffset_Y, 0));
                }
                break;
            case Vector2 v when v == Vector2.right:
                Debug.Log("Moving Right");
                targetPosition += new Vector3(isometricBlockOffset_X, isometricBlockOffset_Y, 0);
                if (CheckTileMapBounds(targetPosition))
                {
                    playerTransform.Translate(new Vector3(isometricBlockOffset_X, isometricBlockOffset_Y, 0));
                }
                break;
            default:
                Debug.Log("No movement input detected");
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
}
