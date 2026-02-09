using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] float speed = 3f;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;

        Vector3 steppedposition = Vector3.Lerp(transform.position, desiredPosition, speed * Time.deltaTime);

        transform.position = steppedposition;
    }
}
