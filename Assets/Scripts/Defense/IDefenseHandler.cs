using System;

namespace RoboDash.Defense
{
    public interface IDefenseHandler 
    { 
        bool IsDefending { get; }
        bool IsReflecting { get; }
        float DefenseTime { get; }
        
        event Action DefenseStarted;
    }
}
