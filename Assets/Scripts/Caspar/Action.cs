using UnityEngine;

public abstract class Action : MonoBehaviour
{
    public abstract bool PerformAction();

    public void DoReset()
    {
        Reset();
    }
    protected abstract void Reset();
}
