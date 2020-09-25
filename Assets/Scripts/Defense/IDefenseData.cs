using System;

namespace RoboDash.Defense
{
    public interface IDefenseData 
    { 
        bool IsDefending { get; }
        float DefenseTime { get; }
        
        event Action DefenseStarted;
    }
}
