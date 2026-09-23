using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class gravityScale : MonoBehaviour
{
    public float gravityMultiplier = 2.0f;
    private Rigidbody rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rb.AddForce(Physics.gravity * (gravityMultiplier - 1f), ForceMode.Acceleration);
    }
}
