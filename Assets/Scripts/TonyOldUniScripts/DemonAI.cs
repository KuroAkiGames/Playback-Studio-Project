using UnityEngine;
using System.Collections;

public class DemonAI : MonoBehaviour {
	public Transform target;
	public int moveSpeed;
	public int rotationSpeed;
	
	private Transform myTransform;
	
	void Awake() {
		myTransform = transform;
	}
	
	// Use this for initialization
	void Start () {
		GameObject go = GameObject.FindGameObjectWithTag("Avatar");
		
		target = go.transform;
	}
	
	// Update is called once per frame
	void Update () {
		Debug.DrawLine(target.position, myTransform.position, Color.red);
		
		//Look at Avatar
		myTransform.rotation = Quaternion.Slerp(myTransform.rotation, Quaternion.LookRotation(target.position - myTransform.position), rotationSpeed * Time.deltaTime);
	
		//Move towards Avatar
		myTransform.position += myTransform.forward * moveSpeed * Time.deltaTime;
	}
}
