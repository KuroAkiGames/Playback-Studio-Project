using UnityEngine;
using System.Collections;

public class DayNightCycle : MonoBehaviour {
	public enum TimeOfDay{
		Idle,
		SunRise,
		SunSet
	}

	public Transform [] sun;						//An Array that holds all the 'suns'
	public float dayCycleInMinutes =1;				//How many real minutes it will take for a full game day eg. 24min = 1 day

	public float sunRise;							//The time of day that the sun rises
	public float sunSet;							//The time of day that we start sunset
	public float skyboxBlendModifier;				//The speed in which the skyboxes will blend

	private SunMoon[]_sunScript;					//An array that holds the SunMoon.cs scripts that are attach to the suns
	private float _degreeRotation;					//Degree of rotation over time
	private float _timeOfDay;						//Tracking the passage of time throughout the day

	private float _dayCycleInSeconds;				//Number of real life seconds it takes in a game time

	private const float SECOND = 1;					//Constant for 1 second
	private const float MINUTE = 60 * SECOND;		//Constant for 1 minute
	private const float HOUR = 60 * MINUTE;			//Constant for 1 hour
	private const float DAY = 24 * HOUR;			//constant for 1 day
	//private const float YEAR = 365 * DAY;			//Constant for 1 year... in theory 325.25 could be used to calculate leap years...

	private const float DEGREES_PER_SECOND = 360 / DAY;

	private TimeOfDay _tod;

	//Use this for initialization
	void Start () {
		_tod = TimeOfDay.Idle;

		_dayCycleInSeconds = dayCycleInMinutes * MINUTE;

		RenderSettings.skybox.SetFloat ("_Blend", 0);

		_sunScript = new SunMoon[sun.Length];
		for(int cnt = 0; cnt < sun.Length; cnt++){
			SunMoon temp = sun[cnt].GetComponent<SunMoon>();

			if(temp == null) {
				Debug.LogWarning ("Sun Script Not found.Equals Adding it.");
				sun[cnt].gameObject.AddComponent<SunMoon>();
				temp = sun[cnt].GetComponent<SunMoon>();
			}
			_sunScript[cnt] = temp;
		}

		_timeOfDay = 0;
		_degreeRotation = DEGREES_PER_SECOND * DAY / (_dayCycleInSeconds);

		sunRise *= _dayCycleInSeconds;
		sunSet *= _dayCycleInSeconds;
	}
	
	//Update is called once per frame
	void Update (){
		//For multiple Suns
		//for(int cnt = 0; cnt < Sun.Length; cnt++)
		sun [0].Rotate (new Vector3(_degreeRotation, 0, 0) * Time.deltaTime);
		
		_timeOfDay += Time.deltaTime;

		if(_timeOfDay > _dayCycleInSeconds)
			_timeOfDay -= _dayCycleInSeconds;

		//Debug.Log (_timeOfDay);

		if(_timeOfDay > sunRise && _timeOfDay < sunSet && RenderSettings.skybox.GetFloat("_Blend")<1){
			_tod = DayNightCycle.TimeOfDay.SunRise;
			BlendSkybox();
		}
		else if(_timeOfDay > sunSet && RenderSettings.skybox.GetFloat ("_Blend")>0){
			_tod = DayNightCycle.TimeOfDay.SunSet;
			BlendSkybox ();
		}
		else{
			_tod = DayNightCycle.TimeOfDay.Idle;
		}
	}
	private void BlendSkybox(){
		float temp = 0;	

		switch(_tod){
		case TimeOfDay.SunRise:
			temp = (_timeOfDay - sunRise) / _dayCycleInSeconds * skyboxBlendModifier;
					break;
		case TimeOfDay.SunSet:
			temp = (_timeOfDay - sunSet) / _dayCycleInSeconds * skyboxBlendModifier;
			temp = 1 - temp;
					break;
			}

			RenderSettings.skybox.SetFloat ("_Blend", temp);

		//Debug.Log (temp);
	}
}