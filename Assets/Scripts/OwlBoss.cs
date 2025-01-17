using UnityEngine;

public class OwlBoss : MonoBehaviour
{
    public void Die()
    {
        // Perform boss death animation and logic here...

        // Unlock Super Jump when the boss dies
        PlayerProgress.UnlockSuperJump();
    }
}