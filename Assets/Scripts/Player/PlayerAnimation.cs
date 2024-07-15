using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    protected Animator playerAnimator;

    protected string paramIsMoving = "is Moving";
    protected string paramIsAttacking = "is Punching";

    virtual protected void Start()
    {
        playerAnimator = GetComponent<Animator>();
    }

    virtual public void SetIsMoving(bool isMoving)
    {
        if (!CheckAnimator())
        {
            return;
        }
        playerAnimator.SetBool(paramIsMoving, isMoving);
    }

    virtual public void SetIsAttacking(bool isAttacking)
    {
        if (!CheckAnimator())
        {
            return;
        }
        playerAnimator.SetBool(paramIsAttacking, isAttacking);
    }

    protected bool CheckAnimator()
    {
        if (!playerAnimator)
        {
            Debug.LogError("[PlayerAnimation] Reference of playerAnimator is EMPTY. Animations WILL NOT work.");
            return false;
        }
        return true;
    }
}
