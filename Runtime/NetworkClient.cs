using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Extreal.Integration.Multiplay.Messaging
{
    /// <summary>
    /// Class that holds users and the objects they own.
    /// </summary>
    public class NetworkClient
    {
        /// <summary>
        /// User ID.
        /// </summary>
        public string UserId { get; }

        /// <summary>
        /// Objects to be spawned.
        /// </summary>
        public IReadOnlyList<GameObject> NetworkObjects => networkObjects;
        private readonly List<GameObject> networkObjects = new List<GameObject>();

        /// <summary>
        /// Creates a new NetworkClient.
        /// </summary>
        /// <param name="userId">User ID.</param>
        [SuppressMessage("Usage", "CC0057")]
        public NetworkClient(string userId)
            => UserId = userId;

        internal void AddNetworkObject(GameObject networkObject)
            => networkObjects.Add(networkObject);
    }
}
