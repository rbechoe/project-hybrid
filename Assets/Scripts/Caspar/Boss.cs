using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Boss : MonoBehaviour
{
    private FiniteStateMachine stateMachine;

    private FiniteStateMachine.State idle;
    private FiniteStateMachine.State performAction;

    [SerializeField] private Action[] actionPattern;
    private readonly Queue<Action> actions = new();

    private void Start()
    {
        stateMachine = new FiniteStateMachine();

        CreateIdleState();
        CreateActionState();

        stateMachine.PushState(idle);

        foreach (var action in actionPattern)
        {
            actions.Enqueue(action);
        }
    }

    private void Update()
    {
        stateMachine.Update(gameObject);
    }

    private void CreateIdleState()
    {
        idle = (fsm, gameObj) =>
        {
            if (actions.Any())
            {
                fsm.PopState();
                fsm.PushState(performAction);
            }
        };
    }

    private void CreateActionState()
    {
        performAction = (fsm, gameObj) =>
        {
            var action = actions.Peek();
            
            var success = action.PerformAction();
            
            if (success)
            {
                action.DoReset();
                Transfer(action);

                fsm.PopState();
                fsm.PushState(idle);
            }
            //else
            //{
            //    fsm.PopState();
            //    fsm.PushState(idle);
            //    
            //    Debug.Log($"<color=red>Action {action} could not be completed.</color>");
            //}
        };
    }

    private void Transfer(Action action)
    {
        actions.Dequeue();
        actions.Enqueue(action);
    }
}
