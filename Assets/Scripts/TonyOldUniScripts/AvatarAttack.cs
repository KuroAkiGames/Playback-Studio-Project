using UnityEngine;
using System.Collections;

public class AvatarAttack : MonoBehaviour {
	public GameObject target;
	
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
		if(Input.GetButtonDown("Fire1")) {
			Attack();

		}
		
	}
		private void Attack() {
			float distance = Vector3.Distance(target.transform.position, transform.position);
		
			Vector3 dir = (target.transform.position - transform.position).normalized;
		
			float direction = Vector3.Dot(dir, transform.forward);
			
			Debug.Log(direction);	
		
			if(distance < 40f) {
				if(direction > 0) {
				DemonHealth eh = (DemonHealth)target.GetComponent("DemonHealth");
				eh.AddjustCurrentHealth(-10);
		}
	}
}
}