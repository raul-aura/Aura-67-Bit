using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Class References")]
    [SerializeField] private PlayerAnimation playerAnimationRef;

    private Rigidbody playerRigidbody;

    [Header("Input References")]
    [SerializeField] private RectTransform inputPanel;
    [SerializeField] private Transform inputButton;

    private Vector2 inputButtonInitialPos;
    private Vector2 inputButtonLimit;
    private bool isPlayerTouch = false;
    private Vector2 touchStart;
    private Vector2 touchEnd;

    [Header("Parameters")]
    [SerializeField] private float movementSpeed = 8f;
    [SerializeField] private float rotationSpeed = 8f;

    private void Awake()
    {
        if(inputPanel)
        {
            inputButtonLimit = new Vector2(inputPanel.rect.width / 2, inputPanel.rect.height / 2);
        }
        if (inputButton)
        {
            inputButtonInitialPos = inputButton.position;
        }
    }

    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        if (playerRigidbody)
        {
            playerRigidbody.freezeRotation = true;
        }
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
            Vector2 touchButton = Vector2.ClampMagnitude(touchDistance, inputButtonLimit.magnitude);
            Move(new Vector3(direction.x, 0, direction.y));
            if (playerAnimationRef)
            {
                playerAnimationRef.SetIsMoving(true);
            }
            if (inputButton)
            {
                inputButton.position = new Vector2(inputButtonInitialPos.x + touchButton.x, inputButtonInitialPos.y + touchButton.y);
            }
        }
        else
        {
            if (playerAnimationRef)
            {
                playerAnimationRef.SetIsMoving(false);
            }
            if (inputButton)
            {
                inputButton.position = inputButtonInitialPos;
            }
        }
    }

    void Move(Vector3 direction)
    {
        Vector3 moveDirection = movementSpeed * Time.deltaTime * direction.normalized;
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection);
        if (moveDirection != Vector3.zero)
        {
           Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
           playerRigidbody.MoveRotation(Quaternion.Slerp(playerRigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime));
        }
    }
}
