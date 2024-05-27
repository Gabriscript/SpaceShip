using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectRemoval : MonoBehaviour
{
    float selfRemovalTimer;
    // Start is called before the first frame update
    void Awake()
    {
        selfRemovalTimer = GetComponent<ParticleSystem>().main.duration;
    }

    // Update is called once per frame
    void Update()
    {
        selfRemovalTimer -= Time.deltaTime;
        if (selfRemovalTimer < 0) { 
        Destroy(gameObject);
        }
    }
}
