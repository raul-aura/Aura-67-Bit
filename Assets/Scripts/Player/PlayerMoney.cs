using UnityEngine;
using UnityEngine.UI;

public class PlayerMoney : MonoBehaviour
{
    [SerializeField] protected Text moneyText;

    [SerializeField] protected float initialMoney;
    [SerializeField] protected float maxMoney = 9999;
    protected float money;

    virtual protected void Start()
    {
        money = initialMoney;
        UpdateText();
    }

    virtual public void AddMoney(float value)
    {
        if (value < 0)
        {
            value *= -1f;
            Debug.LogWarning("[PlayerMoney] Detected negative value input in AddMoney(), converting to positive value.");
        }
        money += value;
        money = Mathf.Clamp(money, 0, maxMoney);
        UpdateText();
    }

    virtual public bool RemoveMoney(float value)
    {
        if (value < 0)
        {
            value *= -1f;
            Debug.LogWarning("[PlayerMoney] Detected negative value input in AddMoney(), converting to positive value.");
        }
        if (money < value)
        {
            return false;
        }
        money -= value;
        money = Mathf.Clamp(money, 0, maxMoney);
        UpdateText();
        return true;
    }

    virtual protected void UpdateText()
    {
        if (!moneyText)
        {
            Debug.LogWarning("[PlayerMoney] Reference of moneyText is EMPTY. Returning UpdateText() function.");
            return;
        }
        moneyText.text = string.Concat("$" + money.ToString());
    }
}
