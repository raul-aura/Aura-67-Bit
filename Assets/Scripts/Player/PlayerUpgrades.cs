using UnityEngine;
using UnityEngine.UI;

public class PlayerUpgrades : MonoBehaviour
{
    [Header("Base Upgrade")]
    [SerializeField] protected Text upgradeText;
    [SerializeField] protected uint upgradeValue;

    virtual protected void Start()
    {
        upgradeText.text = string.Concat("Upgrade $" + upgradeValue.ToString());
    }

    virtual protected void Update() {}

    virtual public void Upgrade() {}
}
