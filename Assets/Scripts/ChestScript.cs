using UnityEngine;

public class ChestScript : MonoBehaviour
{

    private Animator chestAnimator;
    private bool hasPlayerCollided = false;
    void Start()
    {
        chestAnimator = GetComponent<Animator>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // If the player collides with the chest
        if (collision.CompareTag("Player"))
        {
            hasPlayerCollided = true;
            Debug.Log("Player has collided with chest");


         
           
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hasPlayerCollided = false;
    }



    void Update()
    {
        if (hasPlayerCollided && Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Open Chest");

            chestAnimator.SetBool("bOpenChest", true);


        }
    }
}
