using Godot;
using System;

public class Fall : State
{
    public override void _Ready()
    {
        base._Ready();
        loop = true;
    }
    public override void FrameAdvance()
    {
        base.FrameAdvance();
        if (owner.grounded && frameCount > 0)
        {
            owner.ForceEvent(EventScheduler.EventType.AUDIO, "Landing");
            EmitSignal(nameof(StateFinished), "Idle");
        }
        owner.CheckTurnAround();
        ApplyGravity();
    }

    public override void PushMovement(float _xVel)
    {
    }

    public override void HandleInput(char[] inputArr)
    {
        if (Globals.CheckKeyPress(inputArr, 'k'))
        {
            EmitSignal(nameof(StateFinished), "JumpKick");
        }
        else if (Globals.CheckKeyPress(inputArr, 'p'))
        {
            EmitSignal(nameof(StateFinished), "JumpPunch");
        }
        else if (Globals.CheckKeyPress(inputArr, 's'))
        {
            EmitSignal(nameof(StateFinished), "JumpSlash");
        }
    }
}
