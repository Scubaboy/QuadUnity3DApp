using Assets.Services.SignalR.MsgParser.jsonParser;
using Newtonsoft.Json;
using Assets.Services.SignalR.Interfaces;

namespace Assets.Services.SignalR.MsgParser.JsonParser
{
    public class HubToClientMsgParserJson<T> : ISignalRMsgParserJson
    {
        private HubMessageWrapper<T> helper;

        public object Parameters
        {
            get
            {
                return helper.M[0].A[0];
            }
        }

        public void ParseHubToClientMsg(string hubMsg)
        {
            helper =  JsonConvert.DeserializeObject<HubMessageWrapper<T>>(hubMsg);
        }
    }
}
