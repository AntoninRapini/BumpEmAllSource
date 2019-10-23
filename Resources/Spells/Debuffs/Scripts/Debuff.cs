using UnityEngine;
using System.Collections;

public class Debuff : MonoBehaviour {


    public float duration;
    protected float timeAppeared;

    public Debuff(float _duration)
    {
        duration = _duration;
    }


    public virtual void Break()
    {
      
    }

    public virtual void Refresh()
    {

    }
}

interface IBreakable
{
	void Break ();
}