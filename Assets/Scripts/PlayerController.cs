using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
	public float fireRate = 0.5F;
	public float speed;
	public float tilt;
	public Boundary boundary;
	public GameObject shot;
	public Transform[] shotSpawns;

	private float myTime = 0.0F;
	private float nextFire = 0.5F;
	private Rigidbody rb;
	private AudioSource audioSource;

	void Start()
	{
		rb = GetComponent<Rigidbody>();
		audioSource = GetComponent<AudioSource>();
	}

	void Update()
	{
		myTime = myTime + Time.deltaTime;

		if (Input.GetButton("Fire1") && myTime > nextFire) {
			nextFire = myTime + fireRate;

            foreach (Transform shotSpawnSpot in shotSpawns)
                Instantiate(shot, shotSpawns[0].position, shotSpawnSpot.rotation);

			audioSource.Play();

			nextFire = nextFire - myTime;
			myTime = 0.0F;
		}
	}

	void FixedUpdate()
	{
		float moveHorizontal = Input.GetAxis("Horizontal");
		float moveVertical = Input.GetAxis("Vertical");

		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
		rb.velocity = movement * speed;

		rb.position = new Vector3(
			Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
			0.0f,
			Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax)
		);

		rb.rotation = Quaternion.Euler(
			0.0f,
			0.0f,
			rb.velocity.x * -tilt
		);
	}

	public void StartFireRateBoost()
	{
        StartCoroutine(FireRateBoost());
    }

    public void StartSpeedBoost()
    {
        StartCoroutine(SpeedBoost());
    }

    IEnumerator FireRateBoost()
    {
        fireRate /= 3;
        Transform newShotSpawn1 = new GameObject("ShotSpawn1").transform;
        newShotSpawn1.position = shotSpawns[0].position;
        newShotSpawn1.rotation = Quaternion.Euler(shotSpawns[0].rotation.eulerAngles.x, 30f, shotSpawns[0].rotation.eulerAngles.z);

        Transform newShotSpawn2 = new GameObject("ShotSpawn2").transform;
        newShotSpawn2.position = shotSpawns[0].position;
        newShotSpawn2.rotation = Quaternion.Euler(shotSpawns[0].rotation.eulerAngles.x, -30f, shotSpawns[0].rotation.eulerAngles.z);

        shotSpawns = new Transform[] { shotSpawns[0], newShotSpawn1, newShotSpawn2 };

        yield return new WaitForSeconds(10);
        Destroy(newShotSpawn1.gameObject);
        Destroy(newShotSpawn2.gameObject);
        shotSpawns = new Transform[] { shotSpawns[0] };
        fireRate *= 3;

    }

    IEnumerator SpeedBoost()
    {
        speed *= 2;
        yield return new WaitForSeconds(10);
        speed /= 2;
    }
}
