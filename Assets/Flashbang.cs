using UnityEngine;

public class Flashbang : BossAction
{
    public override bool PerformAction()
    {
        Debug.Log("Throwing a flashbang!!1!!");

        return true;
    }

    protected override void Reset()
    {

    }
}
