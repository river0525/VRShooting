using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatricleController : MonoBehaviour
{
    [SerializeField] ParticleSystem[] particles;
    public void PlayParticle(int index)
    {
        particles[index].Play();
    }
}
