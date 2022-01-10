using UnityEngine;

public abstract class BossAction : MonoBehaviour
{
    public abstract bool PerformAction();

    public void DoReset()
    {
        Reset();
    }
    protected abstract void Reset();
}
