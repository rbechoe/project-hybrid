using UnityEngine;

public interface IState
{
    void Update(FiniteStateMachine fsm, GameObject gameObject);
}
