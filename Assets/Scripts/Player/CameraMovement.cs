using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] protected Transform player;
    [SerializeField] protected Vector3 cameraOffset;

    virtual protected void LateUpdate()
    {
        if (player)
        {
            Vector3 newPosition = player.position + cameraOffset;
            transform.position = newPosition;
        }
    }
}
