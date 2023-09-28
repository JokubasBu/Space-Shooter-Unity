using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed;

	private Rigidbody rb;

	void Start()
	{
		rb = GetComponent<Rigidbody>();

		rb.velocity = transform.forward * speed;
	}

    void Update()
    {
        // Ensure the Y component of velocity remains zero.
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
    }

}
