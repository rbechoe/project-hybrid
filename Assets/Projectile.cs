using UnityEngine;

public class Projectile : BossAction
{
    public override bool PerformAction()
    {
        Debug.Log("Ik heb nu een cool projectielbarfje gelegd!!!");

        return true;
    }

    protected override void Reset()
    {

    }
}
