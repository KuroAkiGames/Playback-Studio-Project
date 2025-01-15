using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour {
	public GameObject Target;
	public float AttackTimer;
	public float CoolDown;
	
	// Use this for initialization
	void Start () {
		AttackTimer = 0;
		CoolDown = 4.0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (AttackTimer > 0)
			AttackTimer -= Time.deltaTime;
		
		if (AttackTimer <0)
			AttackTimer = 0;
		
		if(AttackTimer == 0){
			Attack();
			AttackTimer = CoolDown;
	}
}
	private void Attack(){
		float distance = Vector3.Distance(Target.transform.position, transform.position);
		
		Vector3 dir = (Target.transform.position - transform.position);
		
		float direction = Vector3.Dot (dir, transform.forward);
		
		Debug.Log (direction);
		
		if(distance <2f){
			if(direction >0){
				PlayerHealth eh = (PlayerHealth)Target.GetComponent("PlayerHealth");
				eh.AddjustCurrentHealth(-13);
			}
		}
	}
}