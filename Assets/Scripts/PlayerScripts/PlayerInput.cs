using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement characterMovement;
    private Vector3 moveVector;
    
    private void Awake()
    {
        characterMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");

        characterMovement.AddMoveVectorInput(moveVector.normalized);
    }
}
