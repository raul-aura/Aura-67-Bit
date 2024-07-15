using UnityEngine;

public class ObjectMoney : MonoBehaviour
{
    [SerializeField] protected float moneyAmount = 100;

    protected PlayerMoney playerMoneyRef;

    virtual protected void Start()
    {
        playerMoneyRef = GameObject.FindWithTag("Player").GetComponent<PlayerMoney>();
        CheckPlayerMoney();
    }

    protected void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy"))
        {
            if(!CheckPlayerMoney())
            {
                return;
            }
            playerMoneyRef.AddMoney(moneyAmount);
            Destroy(this.gameObject);
        }
    }

    protected bool CheckPlayerMoney()
    {
        if (!playerMoneyRef)
        {
            Debug.LogError("[ObjectMoney] Reference of playerMoney is EMPTY. Functions WILL NOT work.");
            return false;
        }
        return true;
    }
}
