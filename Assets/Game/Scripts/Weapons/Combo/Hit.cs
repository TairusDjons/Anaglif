using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Hit {
    public int Damage;
    public int AnimationId;
    public float startAttackWindow;
    public float endAttackWindow;
    public float delayAttackWindow; //Not implemented properly, used with delayed combos
    [SerializeField]
    public Hit nextAttack;
    [SerializeField]
    public Hit delayNextAttack; //Not implemented properly, used with delayed combos

    public Hit(int dmg, int animId, float startWindow, float endWindow, float delayAttack)
    {
        Damage = dmg;
        AnimationId = animId;
        startAttackWindow = startWindow;
        endAttackWindow = endWindow;
        delayAttackWindow = delayAttack;
    }
}
