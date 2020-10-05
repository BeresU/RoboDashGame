using RoboDash.Controllers.Battle;
using UnityEngine;

namespace RoboDash.Controllers
{
    public class EndGameActivator : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            var battleHandler = other.GetComponent<IBattleHandler>();
            if(battleHandler == null) return;
            battleHandler.PlayerFail();
        }
    }
}
