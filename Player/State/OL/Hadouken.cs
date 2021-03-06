using Godot;
using System;

public class Hadouken : State
{
    [Export]
    public int releaseFrame = 18;

    private PackedScene hadoukenScene;
    public override void _Ready()
    {
        base._Ready();
        hadoukenScene = (PackedScene)GD.Load("res://Hadouken/HadoukenPart.tscn");
    }

    public override void Enter()
    {
        base.Enter();
        owner.velocity.x = 0;
    }
    public override void FrameAdvance()
    {
        base.FrameAdvance();
        if (frameCount == releaseFrame)
        {
            owner.ScheduleEvent(EventScheduler.EventType.AUDIO);
            EmitHadouken();
        }
    }

    public override void AnimationFinished()
    {
        EmitSignal(nameof(StateFinished), "Idle");
    }

    private void EmitHadouken()
    {
        var h = hadoukenScene.Instance() as HadoukenPart;

        h.Spawn(owner.facingRight, owner.otherPlayer);
        owner.EmitHadouken(h);
        h.GlobalPosition = new Vector2(owner.Position.x, owner.Position.y + 5);
    }
}
