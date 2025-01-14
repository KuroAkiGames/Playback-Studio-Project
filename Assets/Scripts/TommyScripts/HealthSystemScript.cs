using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystemScript : MonoBehaviour
{

    [Header("Health Settings")]
    [SerializeField] private float health;
    [SerializeField] private float initialHealth = 3;

    [Header("Damage Settings")]

    [SerializeField] private float damagevalue = 1;

    [Header("Related Scripts")]
    public PlayerController playerController;


    void Start()
    {
        //Call Health Setup
        SetupHealth();

        //Get the Player Controller
        playerController = GetComponent<PlayerController>();
        
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            DealDamage();

        }
    }

    void SetupHealth()
    {
        //SetupCharacterHealth
        health = initialHealth;
    }

    IEnumerator HealthCoroutine()
    {

        yield return new WaitForSeconds(2);
        
        SetupHealth();
    }


    void DealDamage()
    {
        //Call Take Damage, current implemenation will kill player.

        health = health - damagevalue;
        Debug.Log("Damage Dealt to Player: "+damagevalue+"Player Health: "+health);

        if(health <1)
        {
            playerController.TakeDamage();
            Debug.Log("Kill Player");
            


            StartCoroutine(HealthCoroutine());

        }
    }
}
