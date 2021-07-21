using Godot;
using System;

public class AnimationPlayer : Godot.AnimationPlayer
{
    [Signal]
    public delegate void AnimationFinished();

    private int animationLength;
    public int cursor;
    
    public void NewAnimation(string animName) 
    {
        if (animName == AssignedAnimation) 
        {
            
            Seek(0, true);
            cursor = 0;
            GD.Print($"Restarting {animName} animation. Cursor = {cursor}, length = {animationLength}");
        }
        else
        {
            Play(animName);
            cursor = 0;
            animationLength = (int)CurrentAnimationLength; //Bad idea?
            Stop();
            Seek(0, true);
        }
    }

    public void FrameAdvance() 
    {
        if (cursor < animationLength)
        {
            cursor++;
            Seek(cursor, true);
        }
        else
        {
            EmitSignal(nameof(AnimationFinished), CurrentAnimation);
        }

        if (IsPlaying())
        {
            GD.Print("This SHOULD NOT BE CALLED");
        }
    }

    public void Restart() 
    {
        Seek(0, true);
        cursor = 0;
    }

    public void FirstFrame()
    {
        GD.Print("First Frame of Kick actions called");
    }
}