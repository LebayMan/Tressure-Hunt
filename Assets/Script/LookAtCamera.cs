using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private Transform cam;

    void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        // Make the object look at the camera
        transform.LookAt(cam);

        // Flip the object if needed (so it doesn't look backward/mirrored)
        Vector3 euler = transform.eulerAngles;
        euler.y += 180f;
        transform.rotation = Quaternion.Euler(euler);
    }
}
