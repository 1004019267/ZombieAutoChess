using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public enum Role_State
{
	None,
	idle,
	dead,
	move,
	attack,
	hit,
	Repelling,//击退
	vertigo,//眩晕
	decelerate,//减速
	Frozen,//冰封
}

public interface BaseState
{
	void loop();
	void OnEnter();
    Role_State Role_Type();
}


public class idle_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;
    public idle_State(GameObject role)
	{
		_role = role;
        _type = Role_State.idle;
    }

    public Role_State Role_Type()
    {
        return _type;
    }
	
	
	public void OnEnter()
	{

        
    }

	public void loop()
	{
        //if (Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    _zom.m_state = new move_State(_zom.gameObject);
        //    _zom.m_state.OnEnter();
        //}   
	}
	
}


public class move_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;
    float m_MoveSpeed;
    public move_State(GameObject role)
	{
		_role = role;
        _type = Role_State.move;
	}
	
	
	public void OnEnter()
	{
    }

    public Role_State Role_Type()
    {
        return _type;
    }


    public void loop()
	{
       
        _role.transform.Translate(Vector2.right * m_MoveSpeed * Time.deltaTime);
	}
	
}


public class dead_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public dead_State(GameObject role)
	{
		_role = role;
        _type = Role_State.dead;
	}
	
	public void OnEnter()
	{
		
	}

    public Role_State Role_Type()
    {
        return _type;
    }

    public void loop()
	{
		
	}
	
}

public class attack_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public attack_State(GameObject role)
	{
		_role = role;
        _type = Role_State.attack;
    }

    public Role_State Role_Type()
    {
        return _type;
    }

    public void OnEnter()
	{
		
	}

	public void loop()
	{
		
	}
	
}

public class hit_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public hit_State(GameObject role)
	{
		_role = role;
        _type = Role_State.hit;
    }

    public Role_State Role_Type()
    {
        return _type;
    }

    public void OnEnter()
	{
		
	}

	public void loop()
	{
		
	}
	
}

public class Repelling_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public Repelling_State(GameObject role)
	{
		_role = role;
        _type = Role_State.Repelling;
    }

    public Role_State Role_Type()
    {
        return _type;
    }

    public void OnEnter()
	{
		
	}

	public void loop()
	{
		
	}
	
}

public class vertigo_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public vertigo_State(GameObject role)
	{
		_role = role;
        _type = Role_State.vertigo;
    }

    public Role_State Role_Type()
    {
        return _type;
    }

    public void OnEnter()
	{
		
	}

	public void loop()
	{
		
	}
	
}

public class decelerate_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public decelerate_State(GameObject role)
	{
		_role = role;
        _type = Role_State.decelerate;
    }

    public Role_State Role_Type()
    {
        return _type;
    }

    public void OnEnter()
	{
		
	}

	public void loop()
	{
		
	}
	
}

public class Frozen_State  :BaseState
{
	private GameObject _role;
    private Role_State _type;

    public Frozen_State(GameObject role)
	{
		_role = role;
        _type = Role_State.Frozen;
	}

    public Role_State Role_Type()
    {
        return _type;
    }

    public void OnEnter()
	{
		
	}

	public void loop()
	{
		
	}
	
}
