using System;
using UnityEngine;

namespace Extreal.Integration.Multiplay.Messaging
{
    /// <summary>
    /// Class that holds player input values.
    /// </summary>
    [Serializable]
    public class PlayerInputValues
    {
        /// <summary>
        /// Move direction to be input.
        /// </summary>
        public Vector2 Move => move;
        [SerializeField] private Vector2 move;

        /// <summary>
        /// Sets move value.
        /// </summary>
        /// <param name="move">Move direction to be set.</param>
        public virtual void SetMove(Vector2 move)
            => this.move = move;

        /// <summary>
        /// Checks whether to send data to all other users.
        /// </summary>
        /// <returns>True if sending data, false otherwise.</returns>
        public virtual bool CheckWhetherToSendData()
            => true;
    }
}
