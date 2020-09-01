using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns : MonoBehaviour
{

    public float speed = 40;
    public GameObject bullet;
    public Transform barrel;
    public AudioSource audioSource;
    public AudioClip audioClip;


    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    private void Update()
    {
        if (Input.GetKeyDown("Fire"))
            audioSource.PlayOneShot(audioClip);
    }

    public void Fire()
    {
        GameObject spawnedBullet = Instantiate(bullet, barrel.position, barrel.rotation);
        spawnedBullet.GetComponent<Rigidbody>().velocity = speed * barrel.forward;
        audioSource.PlayOneShot(audioClip);
        Destroy(spawnedBullet, 2);
    }



}
