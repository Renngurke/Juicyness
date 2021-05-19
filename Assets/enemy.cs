using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public ParticleSystem damage_effect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void TakeDamage()
    {
        Debug.Log("hier");
        damage_effect.Play();
    }
}
