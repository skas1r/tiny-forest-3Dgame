using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private CharacterMovement characterMovement;
    private Vector3 moveVector;
    
    private void Awake()
    {
        characterMovement = GetComponent<CharacterMovement>();
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
