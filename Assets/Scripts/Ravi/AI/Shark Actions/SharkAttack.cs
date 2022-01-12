using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkAttack : Action
{
    public bool inAction;
    float attackRange = 2;
    float followSpeed = 8f;

    float cabinRange, attackPosRange;

    Animator anim;
    GameObject player, attackPos, cartObj;
    SharkCart cart;
    GreatWhiteAI GWAI;

    private void Update()
    {
        cabinRange = Vector3.Distance(transform.position, player.transform.position);
        attackPosRange = Vector3.Distance(transform.position, attackPos.transform.position);
    }

    public void SetParams(Animator anim, SharkCart cart, GameObject cartObj, GreatWhiteAI GWAI) // TODO make abstract shark class
    {
        player = GameObject.FindGameObjectWithTag("Player");
        attackPos = GameObject.FindGameObjectWithTag("PlayerAttack");
        this.anim = anim;
        this.cartObj = cartObj;
        this.cart = cartObj.GetComponent<SharkCart>();
        this.GWAI = GWAI;
    }

    public override bool PerformAction()
    {
        if (!inAction) StartCoroutine(Attacking());
        return false;
    }

    protected override void Reset()
    {
        inAction = false;
        GWAI.ExitAttack();
    }
    
    IEnumerator Attacking()
    {
        inAction = true;
        int waitCount = 0;

        while (inAction)
        {
            if (cabinRange < attackPosRange)
            {
                followSpeed = 5;

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
            else
            {
                followSpeed = 8;

                if (Vector3.Distance(transform.position, attackPos.transform.position) < attackRange)
                {
                    GameObject.FindGameObjectWithTag("SubMarine").GetComponent<SubController>().currentDiveSpeed = 0;

                    Vector3 targetDir = player.transform.position - transform.position;
                    float step = followSpeed * Time.deltaTime;
                    Vector3 newDir = Vector3.RotateTowards(transform.forward, targetDir, step, 0.0f);
                    transform.rotation = Quaternion.LookRotation(newDir);
                    waitCount++;

                    if (waitCount > 30) // wait 30 updates before attacking player
                    {
                        anim.SetTrigger("Attack");
                        EventSystem<int>.InvokeEvent(EventType.DAMAGE_PLAYER, 1);
                        yield return new WaitForSeconds(1);
                        Reset();
                    }
                }
                else
                {
                    // move towards submarine attack point
                    Vector3 targetDir = attackPos.transform.position - transform.position;
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
            }

            yield return new WaitForEndOfFrame();
        }

        yield return new WaitForEndOfFrame();
    }
}
