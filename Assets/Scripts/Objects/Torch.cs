using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Damageable))]
public class Torch : MonoBehaviour
{

    [SerializeField] ParticleSystem fireEffect;
    [SerializeField] GameObject fireLight;

    // Start is called before the first frame update
    void Start()
    {
        Damageable damageable = GetComponent<Damageable>();
        damageable.OnRecieveDamage += ActivateTorch;
        fireLight.SetActive(false);
    }

    private void ActivateTorch(int damage)
    {
        //включать огонь
        //GetComponent<MeshRenderer>().material.color = new Color(Random.value, Random.value, Random.value

        if (fireEffect.isPlaying)
        {
            fireEffect.Stop();
            fireLight.SetActive(false);
        }
        else
        {
            fireEffect.Play();
            fireLight.SetActive(true);
        }

    }

}
