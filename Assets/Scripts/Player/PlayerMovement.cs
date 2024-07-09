using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float rotationSpeed = 10f;

    private bool isPlayerTouch = false;
    private Vector2 touchStart;
    private Vector2 touchEnd;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
        }
        if(Input.GetMouseButton(0))
        {
            touchEnd = Input.mousePosition;
            isPlayerTouch = true;
        }
        else
        {
            isPlayerTouch = false;
        }
    }

    private void FixedUpdate()
    {
        if (isPlayerTouch)
        {
            Vector2 touchDistance = touchEnd - touchStart;
            Vector2 direction = Vector2.ClampMagnitude(touchDistance, 1.0f);
            Move(new Vector3(direction.x, 0, direction.y));
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 moveDirection = movementSpeed * Time.deltaTime * direction.normalized;
        transform.Translate(moveDirection);
        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotationSpeed);
        }
    }
}
