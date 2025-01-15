public class Attribute : BaseStat {

	public Attribute() {
		ExpToLevel = 50;
		Levelmodifier = 1.05f;
	}
}

public enum AttributeName{
	Object_Control_Authority,	//Ability to control an object
	System_Command_Authority,	//Ability to use System Commands
	Transgression_Quotient,		//Obedience to the Taboo Index
	Durability,					//Life value the HP of a character
	Strength,
	Evasion,
	Agility,
	Incarnation,				//Willpower the ability to project an inner image
	Charisma					//Players ability to influence NPC's
}