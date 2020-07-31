using System;
using UnityEngine;

namespace SimpleMovement.Handlers.PhysicCastHandler
{
    [Serializable]
    public class PhysicsCastHandler2D : PhysicsCastHandlerBase<RaycastHit2D>
    {
        protected override bool RayCast()
        {
            _hit = Physics2D.Raycast(_castParameters.Origin,
                _castParameters.Direction, _castParameters.Length, _mask);
            
            return _hit;
        }

        protected override RaycastHit2D[] RayCastAll() =>
            Physics2D.RaycastAll(_castParameters.Origin,
                _castParameters.Direction, _castParameters.Length, _mask);

        protected override bool SphereCast() => throw new NotImplementedException();

        protected override RaycastHit2D[] SphereCastAll() => throw new NotImplementedException();
    }
}