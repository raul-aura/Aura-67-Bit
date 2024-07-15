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
    protected SkinnedMeshRenderer[] meshes;

    protected override void Start()
    {
        base.Start();
        meshes = GetComponentsInChildren<SkinnedMeshRenderer>();
    }

    public override void Upgrade()
    {
        if (playerMoneyRef.RemoveMoney(upgradeValue))
        {
            playerGrabRef.IncreaseMaxGrab(upgradeGrabAmount);
            foreach (SkinnedMeshRenderer mesh in meshes)
            {
                mesh.material.color = upgradeColor;
            }
        }
    }
}
