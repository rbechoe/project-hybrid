using UnityEngine;

public abstract class BossAction : MonoBehaviour
{
    public bool IsDone { get; protected set; }

    public abstract bool PerformAction();

    public void DoReset()
    {
        IsDone = false;
        Reset();
    }
    protected abstract void Reset();
}
