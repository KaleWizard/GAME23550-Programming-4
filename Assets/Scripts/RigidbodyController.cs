using UnityEngine;

public class RigidbodyController : MonoBehaviour
{
    [SerializeField] float moveForce = 5f;
    [SerializeField] float maxSpeed = 8f;

    private Rigidbody rb;

    private bool inputProvided = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (!inputProvided)
        {
            rb.linearVelocity *= 0.9f;
        }
        inputProvided = false;
    }

    // Simple rigidbody movement. To be called in FixedUpdate
    public void MoveBody(Vector3 input)
    {
        inputProvided = input != Vector3.zero;

        Vector3 movement = input.normalized * moveForce;
        rb.AddForce(movement);

        rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
    }
}
