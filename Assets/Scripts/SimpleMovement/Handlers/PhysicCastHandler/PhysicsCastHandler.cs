using System;
using UnityEngine;

namespace SimpleMovement.Handlers.PhysicCastHandler
{
    [Serializable]
    public class PhysicsCastHandler : PhysicsCastHandlerBase<RaycastHit>
    {
        protected override bool RayCast()
        {
            return Physics.Raycast(_castParameters.Origin,
                _castParameters.Direction, out _hit, _castParameters.Length, _mask);
        }
        
        protected override RaycastHit[] RayCastAll()
        {
            return Physics.RaycastAll(_castParameters.Origin, 
                _castParameters.Direction, _castParameters.Length, _mask);
        }

        protected override bool SphereCast()
        {
            return Physics.SphereCast(_castParameters.Origin, _castParameters.Radius,
                _castParameters.Direction, out _hit, _castParameters.Length,_mask);
        }

        protected override RaycastHit[] SphereCastAll()
        {
            return Physics.SphereCastAll(_castParameters.Origin, _castParameters.Radius,
                _castParameters.Direction, _castParameters.Length, _mask);
        }
    }
}
