using UnityEngine;
using UnityEngine.UI;

public class UpgradeColorGrab : PlayerUpgrades
{
    [Header("Class References")]
    [SerializeField] protected PlayerGrab playerGrabRef;
    [SerializeField] protected PlayerMoney playerMoneyRef;

    [Header("Upgrade")]
    [SerializeField] protected uint upgradeGrabAmount = 2;
    [SerializeField] protected Color upgradeColor = Color.white;
    protected SkinnedMeshRenderer mesh;

    protected override void Start()
    {
        base.Start();
        mesh = GetComponentInChildren<SkinnedMeshRenderer>();
    }

    public override void Upgrade()
    {
        if (playerMoneyRef.RemoveMoney(upgradeValue))
        {
            playerGrabRef.IncreaseMaxGrab(upgradeGrabAmount);
            mesh.material.color = upgradeColor;
        }
    }
}
