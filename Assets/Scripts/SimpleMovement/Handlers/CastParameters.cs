using UnityEngine;
using System;

[Serializable]
public class CastParameters
{
    [SerializeField] private Transform _originRef;
    [SerializeField] private Transform _directionRef;
    [SerializeField] private float _length;
    [SerializeField] private float _radius;

    // TODO: Editor script that hide refs when thoes bools enable
    [SerializeField] private bool _customOrigin;       
    [SerializeField] private bool _customDirection;

    private Vector3 _origin;
    private Vector3 _direction;

    private Func<Vector3> _directionMethod;
    private Func<Vector3> _originMethod;

    public void Init()
    {
        _originMethod = _customOrigin ? () => _origin : (Func<Vector3>)GetOriginRef;

        _directionMethod = (_customDirection || _customOrigin) ?
            () => _direction : (Func<Vector3>)GetDirectionRef;
    }

    public bool CustomOrigin
    {
        get { return _customOrigin; }
        set
        {
            _customOrigin = value;
            Init();
        }
    }

    public bool CustomDirection
    {
        get { return _customDirection; }
        set
        {
            _customDirection = value;
            Init();
        }
    }

    public Transform OriginRef
    {
        get { return _originRef; }
        set { _originRef = value; }
    }

    public Transform DirectionRef
    {
        get { return _directionRef; }
        set { _directionRef = value; }
    }

    public Vector3 Origin
    {
        get { return _originMethod(); }
        set { _origin = value; }
    }

    public Vector3 Direction
    {
        get{ return _directionMethod();}
        set { _direction = value;}
    }

    public float Length
    {
        get { return _length; }
        set { _length = value; }
    }

    public float Radius
    {
        get { return _radius; }
        set { _radius = value; }
    }

    private Vector3 GetDirectionRef()
    {
        return (_directionRef.position - GetOriginRef()).normalized;
    }

    private Vector3 GetOriginRef()
    {
        return _originRef.position;
    }
}
