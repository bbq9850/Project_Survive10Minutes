using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelUpEffect : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] particles;
    

    public void Play()
    {
        gameObject.SetActive(true);

        foreach (var ps in particles)
        {
            if (ps == null) continue;

            ps.gameObject.SetActive(true);
            ps.Play();
        }
    }
}
