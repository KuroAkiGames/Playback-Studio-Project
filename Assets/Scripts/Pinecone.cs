using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

using UnityEngine;

using UnityEngine;

using UnityEngine;

public class Pinecone : MonoBehaviour
{
    [Header("Pinecone Settings")]
    public float fallSpeed = 5f; // Speed at which the pinecone falls

    void Update()
    {
        // Move the pinecone down at the specified speed
        transform.Translate(Vector3.down * fallSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            // Destroy the pinecone when it hits the ground
            Destroy(gameObject);
        }
    }
}
