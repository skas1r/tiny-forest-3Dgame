using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private Vector3 moveVector;
    
    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        moveVector.x = Input.GetAxisRaw("Horizontal");
        moveVector.z = Input.GetAxisRaw("Vertical");

        playerMovement.AddMoveVectorInput(moveVector.normalized);
        playerMovement.AddRunningInput(Input.GetKey(KeyCode.LeftShift));
    }
}
