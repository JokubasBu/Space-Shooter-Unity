using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
	public GameObject shot;
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

		InvokeRepeating("Fire", delay, fireRate);
	}

	void Fire()
	{
		foreach (Transform shotSpawnSpot in shotSpawn)
		{
			if (aim)
			{
				target = GameObject.FindWithTag("Player");
				if (target != null)
				{
					shotSpawnSpot.LookAt(target.transform);
				}
            }
        Instantiate(shot, shotSpawnSpot.position, shotSpawnSpot.rotation);
		}

		audioSource.Play();
	}
}
