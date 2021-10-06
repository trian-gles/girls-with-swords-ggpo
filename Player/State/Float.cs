using Godot;
using System;

public class Float : HitStun
{

    protected override void EnterHitState(bool knockdown, bool launch)
    {
        EmitSignal(nameof(StateFinished), "Float");
    }

    public override void FrameAdvance()
    {
        frameCount++;
        if (owner.grounded)
        {
            EmitSignal(nameof(StateFinished), "Knockdown");
            owner.ResetCombo();
        }

        stunRemaining--;

        if (stunRemaining == 0)
        {
            owner.ResetCombo();
            EmitSignal(nameof(StateFinished), "Fall");
        }
        

        ApplyGravity();
    }
}
