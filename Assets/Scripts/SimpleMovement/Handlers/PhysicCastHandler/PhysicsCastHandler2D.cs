using System;
using UnityEngine;

namespace SimpleMovement.Handlers.PhysicCastHandler
{
    [Serializable]
    public class PhysicsCastHandler2D : PhysicsCastHandlerBase<RaycastHit2D>
    {
        protected override bool RayCast()
        {
            return _hit = Physics2D.Raycast(_castParameters.Origin, _castParameters.Direction, _castParameters.Length,
                _mask);
        }

        protected override RaycastHit2D[] RayCastAll() =>
            Physics2D.RaycastAll(_castParameters.Origin,
                _castParameters.Direction, _castParameters.Length, _mask);

        protected override bool SphereCast() =>
            _hit = Physics2D.CircleCast(_castParameters.Origin, _castParameters.Radius, _castParameters.Direction);

        protected override RaycastHit2D[] SphereCastAll() => Physics2D.CircleCastAll(_castParameters.Origin,
            _castParameters.Radius,
            _castParameters.Direction, _castParameters.Length, _mask);
    }
}