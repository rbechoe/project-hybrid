using UnityEngine;

public class TurnToPlayer : Action
{
    [SerializeField] private Transform target;
    [SerializeField] private float turnSpeed;
    
    public override bool PerformAction()
    {
        var targetDir = target.position - transform.position;
        var step = turnSpeed * Time.deltaTime;
        var newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        return Vector3.Angle(transform.forward, targetDir) < 1;
    }

    protected override void Reset()
    {

    }
}
