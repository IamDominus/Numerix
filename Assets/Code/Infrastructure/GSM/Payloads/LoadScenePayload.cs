using System;
using Code.Enums;

namespace Code.Infrastructure.GSM.Payloads
{
    public class LoadScenePayload
    {
        public SceneName SceneName { get; set; }
        public Action Callback { get; set; }
    }
}