using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Class References")] //These references are not necessary for movement to work, therefore, no debug is needed.
    [SerializeField] protected PlayerAnimation playerAnimationRef;
    [SerializeField] protected PlayerGrab playerGrabRef;

    protected Rigidbody playerRigidbody;

    [Header("Input References")] //These references are not necessary for movement to work, therefore, no debug is needed.
    [SerializeField] protected RectTransform inputPanel;
    [SerializeField] protected Transform inputButton;
    [SerializeField] protected float limitOffset = 75f;

    protected Vector2 inputButtonInitialPos;
    protected Vector2 inputButtonLimit;
    protected bool isPlayerTouch = false;
    protected Vector2 touchStart;
    protected Vector2 touchEnd;

    [Header("Parameters")]
    [SerializeField] protected float movementSpeed = 8f;
    [SerializeField] protected float rotationSpeed = 8f;

    virtual protected void Awake()
    {
        if (inputPanel)
        {
            inputButtonLimit = new Vector2(inputPanel.rect.width / 2, inputPanel.rect.height / 2);
        }
        if (inputButton)
        {
            inputButtonInitialPos = inputButton.position;
        }
    }

    virtual protected void Start()
    {
        playerRigidbody = GetComponent<Rigidbody>();
        if (!CheckRigidbody())
        {
            return;
        }
        playerRigidbody.freezeRotation = true;
    }

    virtual protected void Update()
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

    virtual protected void FixedUpdate()
    {
        if (isPlayerTouch)
        {
            Vector2 touchDistance = touchEnd - touchStart;
            if (touchDistance != Vector2.zero)
            {
                Vector2 direction = Vector2.ClampMagnitude(touchDistance, 1.0f);
                Vector2 touchButton = Vector2.ClampMagnitude(touchDistance, inputButtonLimit.magnitude - limitOffset);
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

    virtual protected void Move(Vector3 direction)
    {
        Vector3 moveDirection = movementSpeed * Time.deltaTime * direction.normalized;
        if (!CheckRigidbody())
        {
            return;
        }
        playerRigidbody.MovePosition(playerRigidbody.position + moveDirection);
        if (moveDirection != Vector3.zero)
        {
           Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
           playerRigidbody.MoveRotation(Quaternion.Slerp(playerRigidbody.rotation, targetRotation, rotationSpeed * Time.deltaTime));
           if (playerGrabRef)
           {
                playerGrabRef.MoveGrabbed(moveDirection);
                playerGrabRef.RotateGrabbed(moveDirection);
           }
        }
    }

    protected bool CheckRigidbody()
    {
        if(!playerRigidbody)
        {
            Debug.LogError("[PlayerMovement] No Rigidbody detected, movement WILL NOT work.");
            return false;
        }
        return true;
    }
}
