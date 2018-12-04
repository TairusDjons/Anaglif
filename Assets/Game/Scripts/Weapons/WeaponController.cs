using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void WeaponEvent(Hit attack);
public delegate void WeaponEventEnd();
public enum WeaponType
{
    Dagger,
    Sword
}
public class WeaponController : MonoBehaviour
{
    public WeaponType type;
    public Hit comboChains;
    public Collider2D weaponCollider;
    public event WeaponEvent OnHit;
    public event WeaponEventEnd EndCombo;

    private Hit currentHit;
    private List<Collider2D> hitedEnemys;
    private bool comboStarted;
    private float attackTimeWindow = 0f;

    private void Start()
    {
        weaponCollider.enabled = false;
        currentHit = comboChains;
    }

    void Update ()
    {
        if (currentHit != null)
        {
            if (Time.time - attackTimeWindow > currentHit.endAttackWindow && comboStarted)
            {
                comboStarted = false;
                currentHit = comboChains;
                weaponCollider.enabled = false;
                EndCombo();
            }

            if (Time.time - attackTimeWindow > currentHit.startAttackWindow)
                weaponCollider.enabled = false;
            if (Input.GetKeyDown(KeyCode.J))
                Attack();
        }
	}

    public void Attack()
    {
        float timedif = Time.time - attackTimeWindow;
        if (comboStarted)
        {
            if (timedif < currentHit.endAttackWindow)
            {
                if (timedif > currentHit.delayAttackWindow && currentHit.delayAttackWindow != 0)
                {
                    OnHit(currentHit.delayNextAttack);
                    currentHit = currentHit.delayNextAttack;
                    attackTimeWindow = Time.time;
                    weaponCollider.enabled = true;
                }
                else if (timedif > currentHit.startAttackWindow)
                {
                    OnHit(currentHit.nextAttack);
                    currentHit = currentHit.nextAttack;
                    attackTimeWindow = Time.time;
                    weaponCollider.enabled = true;
                }
            }
        }
        else
        {
            comboStarted = true;
            OnHit(currentHit);
            attackTimeWindow = Time.time;
            weaponCollider.enabled = true;
        }
    }
}
