using Godot;
using System;

public class Backdash: Walk
{
	[Export]
	public int len = 20;

	[Export]
	public int hopForce = 100;

	public override void Enter()
	{
		frameCount = 0;
		if (owner.CheckHeldKey('8'))
		{
			EmitSignal(nameof(StateFinished), "MovingJump");
		}

		owner.velocity.y = -1 * hopForce;
		owner.ScheduleEvent(EventScheduler.EventType.AUDIO);
		owner.grounded = false;
	}

	public override void HandleInput(char[] inputArr)
    {

    }

	public override void FrameAdvance()
	{
		frameCount++;
		if (frameCount == len)
		{
			EmitSignal(nameof(StateFinished), "Idle");
		}
		ApplyGravity();
	}
}
