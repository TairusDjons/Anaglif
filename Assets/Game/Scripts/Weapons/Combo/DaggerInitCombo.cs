﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DaggerInitCombo : MonoBehaviour {

    public WeaponController controller;


    private void Awake()
    {
        Hit initHit = new Hit(12, 1, 0.3f, 0.5f, 0);
        initHit.nextAttack = new Hit(13, 2, 0.30f, 0.5f, 0);
        initHit.nextAttack.nextAttack = new Hit(20, 3, 0.3f, 0.7f, 0);
        controller.comboChains = initHit;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
