using UnityEngine;

namespace Extreal.Integration.Multiplay.Messaging
{
    /// <summary>
    /// Class that handles player input.
    /// </summary>
    public class PlayerInput : MonoBehaviour
    {
        /// <summary>
        /// Player input values to be synchronized among all users in the same group.
        /// </summary>
        public virtual PlayerInputValues Values => values;
        private readonly PlayerInputValues values = new PlayerInputValues();

        /// <summary>
        /// Sets move value.
        /// </summary>
        /// <param name="newMoveDirection">Move direction to be set.</param>
        public void SetMove(Vector2 newMoveDirection)
            => Values.SetMove(newMoveDirection);

        /// <summary>
        /// Applies values from other users to local objects.
        /// </summary>
        /// <param name="synchronizedValues">Values sent from other user.</param>
        public virtual void ApplyValues(PlayerInputValues synchronizedValues)
            => SetMove(synchronizedValues.Move);
    }
}
