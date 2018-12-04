using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimator : MonoBehaviour
{
    public Animator anima;
    public WeaponController weapon;
	void Start ()
    {
        weapon.OnHit += AttackAnimate;
        weapon.EndCombo += EndAnimating;
	}

    void AttackAnimate(Hit hit)
    {
        anima.SetInteger("CurrentAttack", hit.AnimationId);
        anima.SetTrigger("Attack");
    }
    void EndAnimating()
    {
        anima.SetInteger("CurrentAttack", 0);
    }
}
