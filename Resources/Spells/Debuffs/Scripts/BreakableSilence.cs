using UnityEngine;
using System.Collections;

public class BreakableSilence : Debuff, IBreakable
{
	private SpellManager spellManager;
	private Player playerScript;
    void Start()
    {
		
		spellManager = transform.GetComponent<SpellManager> ();
		playerScript = transform.GetComponent<Player> ();
		playerScript.debuffDictionary.Add("BreakableSilence", this);
		spellManager.silenced = true;
        timeAppeared = Time.time;
    }

	public BreakableSilence(int _duration) : base(_duration)
    {
        duration = _duration;
    }
    void LateUpdate()
    {
		spellManager.silenced = true;
        if(Time.time > timeAppeared + duration)
        {
			playerScript.debuffDictionary.Remove("BreakableSilence");
			spellManager.silenced = false;
            Destroy(this);
        }
    }


    public override void Break()
    {
		playerScript.debuffDictionary.Remove("BreakableSilence");
		spellManager.silenced = false;
        Destroy(this);
    }

    public override void Refresh()
    {
        timeAppeared = Time.time;
    }


}
