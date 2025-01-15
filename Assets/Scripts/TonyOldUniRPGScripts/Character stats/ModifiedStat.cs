using System.Collections.Generic;

public class ModifiedStat{
	private List<ModifyingAttribute> _mods;	//List of attributes that modify stats
	private int _modValue;					//Amount added to the baseValue from the modifiers
	
	public ModifiedStat(){
		_mods = new List<ModifyingAttribute>();
		_modValue = 0;
	}
	
	public void AddModifier(ModifyingAttribute mod){
		_mods.Add (mod);
	}

	private void CalculateModValue() {
		_modValue = 0;
		
		if(_mods.Count > 0)
			foreach(ModifyingAttribute att in _mods)
				_modValue += (int)(att.attribute.AdjustBaseValue * att.ratio);
	}
	
	public void Update() {
		CalculateModValue ();
	}
}

public struct ModifyingAttribute{
	public Attribute attribute;
	public float ratio;
}
