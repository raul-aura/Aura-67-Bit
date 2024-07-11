using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;

    private string paramIsMoving = "is Moving";

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void SetIsMoving(bool isMoving)
    {
        playerAnimator.SetBool(paramIsMoving, isMoving);
    }
}
