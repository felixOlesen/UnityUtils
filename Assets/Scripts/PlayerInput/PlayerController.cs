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

    private void OnMoveEvent(UnityEngine.InputSystem.InputValue inputValue)
    {
        movementInput = inputValue.Get<Vector2>();
        Debug.Log($"Received movement input: {movementInput} at position {playerTransform.position}");

        switch (movementInput)
        {
            case Vector2 v when v == Vector2.up:
                Debug.Log("Moving Up");
                playerTransform.Translate(new Vector3(-isometricBlockOffset_X, isometricBlockOffset_Y, 0));
                break;
            case Vector2 v when v == Vector2.down:
                Debug.Log("Moving Down");
                playerTransform.Translate(new Vector3(isometricBlockOffset_X, -isometricBlockOffset_Y, 0));
                break;
            case Vector2 v when v == Vector2.left:
                Debug.Log("Moving Left");
                playerTransform.Translate(new Vector3(-isometricBlockOffset_X, -isometricBlockOffset_Y, 0));
                break;
            case Vector2 v when v == Vector2.right:
                Debug.Log("Moving Right");
                playerTransform.Translate(new Vector3(isometricBlockOffset_X, isometricBlockOffset_Y, 0));
                break;
            default:
                Debug.Log("No movement input detected");
                break;
        }
    }
}
