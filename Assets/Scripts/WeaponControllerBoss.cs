using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponControllerBoss : MonoBehaviour
{
	public GameObject shot;
    public GameObject beam;
    public Transform[] shotSpawn;
	public float fireRate;
    public float delay;
	public bool aim;


    private Vector3 direction;
	private GameObject target;
	private AudioSource audioSource;

	void Start()
	{
		audioSource = GetComponent<AudioSource>();


        InvokeRepeating("FireFast", delay, fireRate*2);
        InvokeRepeating("Fire", delay, fireRate/5);
    }

	void Fire()
	{
			if (aim)
			{
				target = GameObject.FindWithTag("Player");
				if (target != null)
				{
					shotSpawn[4].LookAt(target.transform);
				}
            }
        Instantiate(beam, shotSpawn[4].position, shotSpawn[4].rotation);
		audioSource.Play();
	}

	void FireFast()
	{
		Instantiate(shot, shotSpawn[0].position, shotSpawn[0].rotation);
        Instantiate(shot, shotSpawn[1].position, shotSpawn[1].rotation);
		Instantiate(shot, shotSpawn[2].position, shotSpawn[2].rotation);
        Instantiate(shot, shotSpawn[3].position, shotSpawn[3].rotation);

        audioSource.Play();
	}
}
