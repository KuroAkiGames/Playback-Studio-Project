public class BaseStat {
	private int _BaseValue;			//base value of this stat
	private int _BuffValue;			//amount of the buff to this stat
	private int _ExpToLevel;		//total amount of exp needed to raise this skill
	private float _levelModifier;	//modifier applied to the exp needed to raise the skill
	
	public BaseStat(){
		_BaseValue = 0;
		_BuffValue = 0;
		_levelModifier = 1.1f;
		_ExpToLevel = 100;
	}
	
	# region Basic Setters and Getters
	public int BaseValue {
		get{ return _BaseValue; }
		set{ _BaseValue = value; }
	}
	
	public int BuffValue {
		get{ return _BuffValue; }
		set{ _BuffValue = value; }
	}
	
	public int ExpToLevel {
		get{ return _ExpToLevel; }
		set{ _ExpToLevel = value; }
	}
	
	public float Levelmodifier {
		get{ return _levelModifier; }
		set{ _levelModifier = value; }
	}
	#endregion
	
	private int CalculateExpToLevel(){
		return (int)(_ExpToLevel * _levelModifier);
	}
	
	public void LevelUp(){
		_ExpToLevel= CalculateExpToLevel();
		_BaseValue++;
	}
	
	public int AdjustBaseValue{
		get{
			return BaseValue + BuffValue;
		}
	}
}
