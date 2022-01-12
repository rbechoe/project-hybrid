using System;
using System.Collections;
using UnityEngine;

public class Bite : Action
{
    [SerializeField] private int damage = 10;
    [SerializeField] private float duration = 1f;
    [SerializeField] private AnimationCurve curve;

    [SerializeField] private Transform target;

    private bool isAnimating;
    private bool isDone;

    public override bool CanPerform()
    {
        var projectile = FindObjectOfType<Projectile>();

        return projectile == null;
    }

    public override bool PerformAction()
    {
        if (isDone) return true;
        
        if (!isAnimating)
        {
            StartCoroutine(Attack(transform.position, target.position));
        }

        return false;
    }
    
    protected override void Reset()
    {
        isDone = false;
    }
    
    private IEnumerator Attack(Vector3 origin, Vector3 target)
    {
        isAnimating = true;
        var hasAttacked = false;
        
        var journey = 0f;
        while (journey <= duration)
        {
            journey += Time.deltaTime;
            var percent = Mathf.Clamp01(journey / duration);
            
            var curvePercent = curve.Evaluate(percent);
            transform.position = Vector3.LerpUnclamped(origin, target, curvePercent);

            if (!hasAttacked)
            {
                if (Math.Abs(curve.Evaluate(percent) - 1) < 0.05f)
                {
                    EventSystem<int>.InvokeEvent(EventType.DAMAGE_PLAYER, damage);
                    
                    hasAttacked = true;
                }
            }

            yield return null;
        }

        isDone = true;
        isAnimating = false;
    }
}
