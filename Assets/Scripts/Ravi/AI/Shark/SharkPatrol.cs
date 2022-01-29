using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkPatrol : Action
{
    private float followSpeed = 20f;

    private SharkCart cart;

    private void Patrolling()
    {
        Vector3 targetDir = cart.transform.position - transform.position;
        float step = followSpeed * Time.deltaTime;
        Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
        transform.rotation = Quaternion.LookRotation(newDir);

        // Move forward
        transform.position += transform.forward * followSpeed * Time.deltaTime;
        if (transform.position.y <= 1)
        {
            transform.position = new Vector3(transform.position.x, 1, transform.position.z);
        }
    }

    protected override void Reset() { }

    public void SetParams(Animator anim, SharkCart cart, GameObject cartObj)
    {
        this.cart = cartObj.GetComponent<SharkCart>();
    }

    // shark is never done patrolling
    public override bool PerformAction()
    {
        Patrolling();
        return false;
    }
}
