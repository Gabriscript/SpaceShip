using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
   public static AudioManager instance {  get; private set; }

    private void Awake()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);

        } else
        {
            instance = this;
        }
    }
    public AudioSource ShootingFX;
    public AudioSource ObstacleFX;
    public AudioSource shipExplosionFX;

    public void PlayShooting()
    {

        ShootingFX.Play();
    }

    public void PlayObstacles()
    {

        ObstacleFX.Play();
    }
    public void PlayshipExplsosion()
    {

       shipExplosionFX.Play();
    }
}
