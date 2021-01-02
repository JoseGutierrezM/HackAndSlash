using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffect : MonoBehaviour
{
    ParticleSystem effect;

    void Awake()
    {
        effect = GetComponent<ParticleSystem>();
    }

    void OnEnable()
    {
        effect.Play();
    }
}