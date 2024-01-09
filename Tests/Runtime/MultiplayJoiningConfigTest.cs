using System;
using NUnit.Framework;

namespace Extreal.Integration.Multiplay.Messaging.Test
{
    public class MultiplayJoiningConfigTest
    {
        [Test]
        public void NewMultiplayJoiningConfigWithMessagingJoiningConfigNull()
            => Assert.That(() => new MultiplayJoiningConfig(null),
                Throws.TypeOf<ArgumentNullException>()
                    .With.Message.Contains("messagingJoiningConfig"));
    }
}
