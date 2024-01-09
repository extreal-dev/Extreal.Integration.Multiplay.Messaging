using System;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

namespace Extreal.Integration.Multiplay.Messaging
{
    public enum MultiplayMessageCommand
    {
        Create,
        Update,
        CreateExistedObject,
        UserInitialized,
        Message,
    };

    [Serializable]
    public class MultiplayMessage
    {
        public MultiplayMessageCommand Command => command;
        [SerializeField, SuppressMessage("Usage", "CC0052")] private MultiplayMessageCommand command;

        public NetworkObjectInfo NetworkObjectInfo => networkObjectInfo;
        [SerializeField, SuppressMessage("Usage", "CC0052")] private NetworkObjectInfo networkObjectInfo;

        public NetworkObjectInfo[] NetworkObjectInfos => networkObjectInfos;
        [SerializeField, SuppressMessage("Usage", "CC0052")] private NetworkObjectInfo[] networkObjectInfos;

        public string Message => message;
        [SerializeField, SuppressMessage("Usage", "CC0052")] private string message;

        public MultiplayMessage
        (
            MultiplayMessageCommand command,
            NetworkObjectInfo networkObjectInfo = default,
            NetworkObjectInfo[] networkObjectInfos = default,
            string message = default
        )
        {
            this.command = command;
            this.networkObjectInfo = networkObjectInfo;
            this.networkObjectInfos = networkObjectInfos;
            this.message = message;
        }
    }
}
