using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateFactory 
{
    PlayerStateManager _context;

    public PlayerStateFactory(PlayerStateManager currentContext)
    {
        _context = currentContext;
    }

    public RunState Run()
    {
        return new RunState();
    }

    public JumpState Jump()
    {
        return new JumpState();
    }

    public GrindState Grind()
    {
        return new GrindState();
    }

    public TurnState Turn()
    {
        return new TurnState();
    }

    public RollState Roll()
    {
        return new RollState();
    }

}
