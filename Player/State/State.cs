using Godot;
using System;

public class State : Node
{
    public Player owner;
    public int frameCount
    { get; set; }
    [Signal]
    public delegate void StateFinished(string nextStateName);

    public int stunRemaining 
    { get; set; }
    public bool loop = false;

    public bool hitConnect = false;


    public override void _Ready()
    {
        owner = GetOwner<Player>();
    }

    public virtual void Enter() 
    {
        frameCount = 0;
    }

    public virtual void Exit()
    {

    }

    public virtual void AnimationFinished() 
    { 
    
    }

    public virtual void HandleInput(char[] inputArr)
    {
        GD.Print(inputArr);
    }

    public virtual void FrameAdvance()
    {
        frameCount++;
    }

    public virtual void PushMovement(float xVel) 
    {
        owner.velocity.x = xVel / 2;
    }

    public virtual void InHurtbox()
    {


    }

    public virtual void ReceiveHit(bool rightAttack, string height, Vector2 push)
    {
        GD.Print($"Received attack on side {rightAttack}");
        if (!rightAttack)
        {
            push.x *= -1;
        }
        owner.velocity = push;
        if (height == "high") 
        {
            if ((rightAttack && owner.CheckHeldKey('6')) || (!rightAttack && owner.CheckHeldKey('4')))
            {
                EmitSignal(nameof(StateFinished), "Block");
            }
            else
            {
                EmitSignal(nameof(StateFinished), "HitStun");
            }
        }
        else if (height == "low") 
        {
            GD.Print("Low hit");
        }
        else
        {
            if ((rightAttack && owner.CheckHeldKey('6')) || (!rightAttack && owner.CheckHeldKey('4'))) 
            {
                EmitSignal(nameof(StateFinished), "Block");
            }
            else 
            {
                EmitSignal(nameof(StateFinished), "HitStun");
            }
        }
    }

    public void receiveStun(int stun)
    {
        stunRemaining = stun;
    }

    public virtual void receiveDamage(int dmg)
    {
        owner.DeductHealth(dmg);
    }
}
