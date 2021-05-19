using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunningParticleBehavior : MonoBehaviour
{
    public GameObject character;
    public ThirdPersonMovement characterMovement;
    public ParticleSystem particles;
    public float characterSpeed;
    private void Start()
    {
        characterMovement = character.GetComponent<ThirdPersonMovement>();
    }

    void Update()
    {
        characterSpeed = characterMovement.movespeed;
        if(characterSpeed < 7f && characterSpeed > 0f)
        {
            particles.Pause();
        }
        else if(characterSpeed > 7f && !particles.isEmitting)
        {
            particles.Play();
        }
    }
}
