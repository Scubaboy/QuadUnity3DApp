namespace Assets.UnitTests.SignalRClients.Editor
{
    using Services.SignalR.MsgToClass;
    using NUnit.Framework;
    using Services.SignalR.MsgParser;
    using System.Collections.Generic;
    using System;
    using Services.SignalR.Client;
    using NSubstitute;
    using Services.Interfaces;
    using Services.SignalR.MsgParser.JsonParser;
    using Services.Models;

    [TestFixture]
    internal class SignalRHubToClientMsgCallbackActivationTests
    {
        [Test]
        public void RegsiteredCallbackActivatedWhenMatchingHubToClientMsgArrives()
        {
            var hubToClientMsg = "{\"C\":\"d-115B33FD-K,0|Z,1|a,0\",\"M\":[{\"H\":\"ActiveQuadsHub\",\"M\":\"ActiveQuads\",\"A\":[[{\"Id\":3,\"QuadId\":\"yy\",\"SupportedComms\":0,\"SupportedIMU\":0,\"SupportGPS\":0,\"SupportedAlt\":0,\"InUse\":false}]]}]}";

            var theCallback = Substitute.For<Action<List<ActiveQuad>>>();
         

            var hubMethodTypeMapping = new Dictionary<Type, string>();
            var clientMethodTypeMapping = new Dictionary<string, ISignalRMsgParserJson>
            {
                { "ActiveQuads", new HubToClientMsgParserJson<List<ActiveQuad>>()}
            };

            var signalRClient = new SignalRClient()
            {
                HubMethodTypeMapping = hubMethodTypeMapping,
                ClientMethodTypeMapping = clientMethodTypeMapping,
                HubConnectionParams = new HubConnectionParams("ActiveQuadsHub", "myserer", false)
            };
               // msgParser,
               // jsonMsgToClass, 
               // hubMethodTypeMapping, 
               // clientMethodTypeMapping, 
               // new HubConnectionParams("ActiveQuadHub","myserer",false)
               // );

            signalRClient.Register<List<ActiveQuad>>("ActiveQuads",theCallback);

            signalRClient.MsgRcved(new List<ReceivedSignalRMsg> { new ReceivedSignalRMsg(DateTime.Now, hubToClientMsg) });
            theCallback.Received()(Arg.Any<List<ActiveQuad>>());
        }
    }
}
