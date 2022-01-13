using UnityEngine;

public class Wait : Action
{
    [SerializeField] private float duration = 2;

    private float passed;

    public override bool PerformAction()
    {
        passed += Time.deltaTime;

        return passed > duration;
    }

    protected override void Reset()
    {
        passed = 0;
    }
}
