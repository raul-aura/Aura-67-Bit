using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    private Animator playerAnimator;

    private string paramIsMoving = "is Moving";
    private string paramIsAttacking = "is Punching";

    void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    public void SetIsMoving(bool isMoving)
    {
        playerAnimator.SetBool(paramIsMoving, isMoving);
    }

    public void SetIsAttacking(bool isAttacking)
    {
        playerAnimator.SetBool(paramIsAttacking, isAttacking);
    }
}
