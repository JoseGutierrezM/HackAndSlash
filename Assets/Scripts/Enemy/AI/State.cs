using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface State
{
    void Execute();
    void Enter();
    void Exit();
}