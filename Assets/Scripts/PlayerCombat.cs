using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCombat : MonoBehaviour
{
    public InputAction attackAction;

    public Animator anim;
    private int queuedAttack = -1;
    private int currentAttack = -1;
    private bool canStartAttack = true;
    private bool isGrounded = true;

    void OnEnable()
    {
        attackAction.Enable();
    }

    void OnDisable()
    {
        attackAction.Disable();
    }

    void Update()
    {
        if (attackAction.WasPressedThisFrame())
        {

            if (currentAttack == -1)
            {
                if (canStartAttack)
                {
                    StartAttack(0);
                }
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
        anim.SetBool("CanStartAttack", false);
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
            anim.SetBool("CanStartAttack", true);
        }
    }

    private void StartAttack(int index)
    {
        currentAttack = index;
        anim.SetInteger("AttackIndex", index);
        anim.SetTrigger("Action1");
    }
    public void SetGrounded(bool grounded)
    {
        isGrounded = grounded;
        anim.SetBool("Falling", !grounded);
    }
}
