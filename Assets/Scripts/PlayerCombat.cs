using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public string attackButton = "Action1";

    public Animator anim;
    private int queuedAttack = -1;
    private int currentAttack = -1;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {

        if (Input.GetButtonDown(attackButton))
        {
            Debug.Log("Action1");
            if (currentAttack == -1)
            {
                StartAttack(0);
            }
            else
            {
                if (currentAttack == 0) queuedAttack = 1;
                if (currentAttack == 1) queuedAttack = 2;
            }
        }
    }

    public void AttackAnimationStart(int index)
    {
        currentAttack = index;
        queuedAttack = -1;
    }

    public void AttackAnimationEnd()
    {
        if (queuedAttack != -1)
        {
            StartAttack(queuedAttack);
        }
        else
        {
            currentAttack = -1;
            anim.SetInteger("AttackIndex", -1);
        }
    }

    private void StartAttack(int index)
    {
        currentAttack = index;
        anim.SetInteger("AttackIndex", index);
        anim.SetTrigger("AttackTrigger");
    }
}
