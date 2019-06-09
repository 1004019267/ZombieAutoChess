using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using OfficeOpenXml.ConditionalFormatting;
using UnityEngine;
using Debug = System.Diagnostics.Debug;

public class StateSystem
{
    private  Dictionary<Role_State,BaseState> _states ;
    private GameObject _player;
    private BaseState curState = null;
    public StateSystem(GameObject player)
    {
        _states = new Dictionary<Role_State, BaseState>();
        _player = player;
        init();
    }

    void init()
    {
        move_State state = CreateState<move_State>();
        _states.Add(state.Role_Type(),state);
        dead_State  state1 = CreateState<dead_State>();
        _states.Add(state1.Role_Type(),state1);
        idle_State state_idle = CreateState<idle_State>();
        _states.Add(state_idle.Role_Type(),state_idle);
        attack_State state_attack = CreateState<attack_State>();
        _states.Add(state_attack.Role_Type(),state_attack);
        hit_State state_hit = CreateState<hit_State>();
        _states.Add(state_hit.Role_Type(),state_hit);
        Repelling_State state_Repelling = CreateState<Repelling_State>();
        _states.Add(state_Repelling.Role_Type(),state_Repelling);
        vertigo_State state_vertigo = CreateState<vertigo_State>();
        _states.Add(state_vertigo.Role_Type(),state_vertigo);
        decelerate_State state_decelerate = CreateState<decelerate_State>();
        _states.Add(state_decelerate.Role_Type(),state_decelerate);
        Frozen_State state_Frozen = CreateState<Frozen_State>();
        _states.Add(state_Frozen.Role_Type(),state_Frozen);
        curState = state_idle;
    }

    public static T CreateState<T>() where T:BaseState
    {
        T t = Activator.CreateInstance<T>();
        return t;
    }

    public bool changeState( Role_State state)
    {
        
        if (_states.ContainsKey(state))
        {
            if (curState.Role_Type() == state)
            {
                return false;
            }
            curState = _states[state];
            curState.OnEnter();
            return true;
        }
        return false;
    }


    public virtual void Loop()
    {
        if (curState != null)
        {
            curState.loop();
        }
    } 
    
    
        
}