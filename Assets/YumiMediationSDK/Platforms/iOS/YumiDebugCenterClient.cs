﻿#if UNITY_IOS
using System;
using YumiMediationSDK.Common;

namespace YumiMediationSDK.iOS
{
    public class YumiDebugCenterClient  : IYumiDebugCenterClient
    {

        public void CallYumiMediationDebugCenter(string bannerPlacementID, string interstitialPlacementID, string videoPlacementID , string nativePlacementID, string channelID, string versionID){
            YumiExterns.PresentDebugCenter(bannerPlacementID,interstitialPlacementID,videoPlacementID, nativePlacementID, channelID,versionID);
        }
        public void ChangeToTestServer(){
            YumiExterns.EnableTestMode();
        }
    }
}
#endif