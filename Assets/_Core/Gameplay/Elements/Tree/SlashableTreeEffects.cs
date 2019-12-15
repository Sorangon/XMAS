﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlashableTreeEffects : MonoBehaviour
{
    public Animator _anim;
    public ParticleSystem _slashEffect = null;

    public void OnSlash() {
        //_anim.SetTrigger("Slash");
        Instantiate(_slashEffect, transform.position, Quaternion.identity);
    }
}
