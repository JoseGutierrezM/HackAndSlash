using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{

    public State currentState;
    protected State defaultState;
    protected State previousState;

    public virtual void SetNewState(State state)
    {
        if (state != currentState)
        {
            if (currentState != null)
            {
                currentState.Exit();
                previousState = currentState;
            }
            currentState = state;
            currentState.Enter();
        }
    }

    public void SetDefaultState()
    {
        SetNewState(defaultState);
    }

    public void SetPreviousState()
    {
        if (previousState != null)
            SetNewState(previousState);
        else
            SetNewState(defaultState);
    }
}