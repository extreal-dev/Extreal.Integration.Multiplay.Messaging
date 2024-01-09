using System;
using System.Diagnostics.CodeAnalysis;
using Extreal.Integration.Messaging;

namespace Extreal.Integration.Multiplay.Messaging
{
    /// <summary>
    /// Class that holds joining config.
    /// </summary>
    public class MultiplayJoiningConfig
    {
        /// <summary>
        /// Messaging joining config.
        /// </summary>
        public MessagingJoiningConfig MessagingJoiningConfig { get; private set; }

        /// <summary>
        /// Creates a new MultiplayJoiningConfig.
        /// </summary>
        /// <param name="messagingJoiningConfig">Messaging joining config.</param>
        /// <exception cref="ArgumentNullException">When messagingJoiningConfig is null.</exception>
        [SuppressMessage("Usage", "CC0057")]
        public MultiplayJoiningConfig(MessagingJoiningConfig messagingJoiningConfig)
        {
            if (messagingJoiningConfig == null)
            {
                throw new ArgumentNullException(nameof(messagingJoiningConfig));
            }

            MessagingJoiningConfig = messagingJoiningConfig;
        }
    }
}
