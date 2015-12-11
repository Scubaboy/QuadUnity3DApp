namespace Assets.UnitTests.SignalRClients.Editor
{
    using Services.SignalR.MsgToClass;
    using NUnit.Framework;
    using Services.SignalR.MsgParser;
    using System.Collections.Generic;
    using System;
    using Services.SignalR.Client;
    using Services.SignalR.Models;
    using NSubstitute;
    using Services.Interfaces;

    [TestFixture]
    internal class SignalRHubToClientMsgCallbackActivationTests
    {
        [Test]
        public void RegsiteredCallbackActivatedWhenMatchingHubToClientMsgArrives()
        {
            var hubToClientMsg = "{\"C\":\"B,6 | O,0 | P,0 | Q,0\",\"M\":[{\"H\":\"ActiveQuadHub\",\"M\":\"clientMethodUpdate\",\"A\":[\"Param1\",\"Param2\"]}]}";
            var jsonMsgToClass = Substitute.For<ISignalRClientParamToClass>();
            var theCallback = Substitute.For<ISignalRCallbackAction>();
            jsonMsgToClass.Convert(Arg.Any<string>(), Arg.Any<Type>()).Returns(x =>
            {
                return new ActiveQuad()
                {
                    QuadId = "66",
                    SupportedAlt = AltimeterOptions.Altic,
                    SupportedComms = CommsOptions.Xbee,
                    SupportedIMU = IMUOpions.DCM,
                    SupportGPS = GPSOptions.MKV11,
                    InUse = false

                };
            });

            //ISignalRMsgParser
            var msgParser = Substitute.For<ISignalRMsgParser>();
            msgParser.ParseHubToClientMsg(Arg.Any<string>()).Returns( x =>
                {
                    return new HubClientMessageWrapper
                    {
                        Hubname = "ActiveQuadHub",
                        ClientMethodName = "ActiveQuad",
                        ClientMethodParameters = "[{\"QuadId\":\"1901\",\"SupportedComms\":\"0\",\"SupportedIMU\":\"0\",\"SupportGPS\":\"0\",\"SupportedAlt\":\"0\",\"InUse\":\"False\"}]"
                    };
                });

            var hubMethodTypeMapping = new Dictionary<Type, string>();
            var clientMethodTypeMapping = new Dictionary<string, Type>
            {
                {"ActiveQuad", typeof(ActiveQuad)}
            };

            var signalRClient = new SignalRClient()
            {
                HubToClientMsgParser = msgParser,
                ParamsToClass = jsonMsgToClass,
                HubMethodTypeMapping = hubMethodTypeMapping,
                ClientMethodTypeMapping = clientMethodTypeMapping,
                HubConnectionParams = new HubConnectionParams("ActiveQuadHub", "myserer", false)

            };
               // msgParser,
               // jsonMsgToClass, 
               // hubMethodTypeMapping, 
               // clientMethodTypeMapping, 
               // new HubConnectionParams("ActiveQuadHub","myserer",false)
               // );

            signalRClient.Register<ActiveQuad>("ActiveQuad",theCallback);

            signalRClient.MsgRcved(new List<ReceivedSignalRMsg> { new ReceivedSignalRMsg(DateTime.Now, hubToClientMsg) });
            theCallback.Received().Action<ActiveQuad>(Arg.Any<ActiveQuad>());
        }
    }
}
