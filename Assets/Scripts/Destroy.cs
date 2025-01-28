using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class AnimationEventDestroyer : MonoBehaviour
{
    // This method will be called by the Animation Event
    public void DestroyObject()
    {
        Destroy(gameObject); // Destroy the object
    }
}

