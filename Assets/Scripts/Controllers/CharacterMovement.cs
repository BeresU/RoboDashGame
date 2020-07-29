using SimpleMovement.Handlers;
using UnityEngine;

namespace RoboDash.Controllers
{
    public class CharacterMovement : MonoBehaviour
    {
        [SerializeField] private Basic3DInputMovementHandler _movementHandler;
        
        private void Awake()
        {
            _movementHandler.Init();        
        }

        // Update is called once per frame
        private void Update()
        {
            _movementHandler.OnUpdate();
        }

        private void FixedUpdate()
        {
            _movementHandler.OnFixedUpdate();
        }
    }
}
