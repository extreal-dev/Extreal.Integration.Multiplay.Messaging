using System;
using Cysharp.Threading.Tasks;
using Extreal.Core.Logging;
using Extreal.Integration.Messaging;
using UnityEngine;

namespace Extreal.Integration.Multiplay.Messaging.Test
{
    public class MockMessagingClient : MessagingClient
    {
        private readonly string localUserId = Guid.NewGuid().ToString();
        private readonly string otherUserId = Guid.NewGuid().ToString();
        private GameObject objectPrefab;
        private NetworkObjectInfo networkObjectInfo;

        private static readonly ELogger Logger = LoggingManager.GetLogger(nameof(MockMessagingClient));

        public MockMessagingClient() : base()
        {
        }

#pragma warning disable CS1998
        protected override async UniTask DoJoinAsync(MessagingJoiningConfig connectionConfig)
#pragma warning restore CS1998
        {
            if (connectionConfig.GroupName == "JoiningApprovalReject")
            {
                FireOnJoiningApprovalRejected();
                return;
            }

            SetJoiningGroupStatus(true);
            FireOnJoined(localUserId);
        }

#pragma warning disable CS1998
        protected override async UniTask DoLeaveAsync()
#pragma warning restore CS1998
            => SetJoiningGroupStatus(false);

#pragma warning disable CS1998
        protected override async UniTask DoSendMessageAsync(string message, string to)
#pragma warning restore CS1998
        {
            if (Logger.IsDebug())
            {
                Logger.LogDebug($"{nameof(SendMessageAsync)}: message={message}");
            }

            if (message.Contains($"\"command\":{(int)MultiplayMessageCommand.CreateExistedObject}"))
            {
                var returnMessage = JsonUtility.ToJson(new MultiplayMessage(MultiplayMessageCommand.UserInitialized));
                FireOnMessageReceived(otherUserId, returnMessage);
            }
        }

        public void FireOnUnexpectedLeft()
            => FireOnUnexpectedLeft("unknown");

        public void FireOnUserJoined()
            => FireOnUserJoined(otherUserId);

        public void FireOnUserLeaving()
            => FireOnUserLeaving(otherUserId);

        public void FireOnMessageReceived(string message)
        {
            var messageJson = JsonUtility.ToJson(new MultiplayMessage(MultiplayMessageCommand.Message, message: message));
            FireOnMessageReceived(otherUserId, messageJson);
        }

        public void SpawnObjectFromOthers(GameObject objectPrefab)
        {
            this.objectPrefab = objectPrefab;
            var gameObjectHash = MultiplayUtil.GetGameObjectHash(this.objectPrefab);
            networkObjectInfo = new NetworkObjectInfo(gameObjectHash, default, default);
            var messageJson = JsonUtility.ToJson(new MultiplayMessage(MultiplayMessageCommand.Create, networkObjectInfo: networkObjectInfo));
            FireOnMessageReceived(otherUserId, messageJson);
        }

        public void UpdateObjectFromOthers()
        {
            var go = new GameObject();
            go.transform.position = Vector3.forward;
            networkObjectInfo.GetTransformFrom(go.transform);
            UnityEngine.Object.Destroy(go);

            if (objectPrefab.TryGetComponent(out PlayerInput input))
            {
                networkObjectInfo.GetValuesFrom(in input);
            }

            var message = JsonUtility.ToJson(new MultiplayMessage(MultiplayMessageCommand.Update, networkObjectInfos: new NetworkObjectInfo[] { networkObjectInfo }));
            FireOnMessageReceived(otherUserId, message);
        }

        public void FireCreateExistedObjectFromOthers(GameObject objectPrefab)
        {
            var gameObjectHash = MultiplayUtil.GetGameObjectHash(objectPrefab);
            var networkObjectInfo = new NetworkObjectInfo(gameObjectHash, default, default);
            var networkObjectInfos = new NetworkObjectInfo[] { networkObjectInfo };
            var message = JsonUtility.ToJson(new MultiplayMessage(MultiplayMessageCommand.CreateExistedObject, networkObjectInfos: networkObjectInfos));
            FireOnMessageReceived(otherUserId, message);
        }

        protected override UniTask<GroupListResponse> DoListGroupsAsync() => throw new NotImplementedException();
        protected override UniTask<CreateGroupResponse> DoCreateGroupAsync(GroupConfig groupConfig) => throw new NotImplementedException();
        public override UniTask DeleteGroupAsync(string groupName) => throw new NotImplementedException();
    }
}
