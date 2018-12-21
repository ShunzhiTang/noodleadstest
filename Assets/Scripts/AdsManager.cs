﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour {

	public Text videoText;
	bool videoAdAvailable;

#if UNITY_IOS
	const string IRONSOURCE_KEY = "7e2f58f5";
#elif UNITY_ANDROID
	const string IRONSOURCE_KEY = "7e2f927d";
#endif

	bool bannerShowing;

	// Use this for initialization
	void Start () {

		IronSource.Agent.init (IRONSOURCE_KEY);
		IronSource.Agent.validateIntegration();

		IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
    	IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; 
    	IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
    	IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
    	IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
    	IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
    	IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;

		IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
        IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;        
        IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; 
        IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; 
        IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
        IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
        IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;

		IronSourceEvents.onBannerAdLoadedEvent += BannerAdLoadedEvent;
		IronSourceEvents.onBannerAdLoadFailedEvent += BannerAdLoadFailedEvent;        
		IronSourceEvents.onBannerAdClickedEvent += BannerAdClickedEvent; 
		IronSourceEvents.onBannerAdScreenPresentedEvent += BannerAdScreenPresentedEvent; 
		IronSourceEvents.onBannerAdScreenDismissedEvent += BannerAdScreenDismissedEvent;
		IronSourceEvents.onBannerAdLeftApplicationEvent += BannerAdLeftApplicationEvent;

		IronSourceEvents.onOfferwallClosedEvent += OfferwallClosedEvent;
        IronSourceEvents.onOfferwallOpenedEvent += OfferwallOpenedEvent;
        IronSourceEvents.onOfferwallShowFailedEvent += OfferwallShowFailedEvent;
        IronSourceEvents.onOfferwallAdCreditedEvent += OfferwallAdCreditedEvent;
        IronSourceEvents.onGetOfferwallCreditsFailedEvent += GetOfferwallCreditsFailedEvent;
        IronSourceEvents.onOfferwallAvailableEvent += OfferwallAvailableEvent;

		IronSource.Agent.shouldTrackNetworkState (true);

		bannerShowing = false;
		videoAdAvailable = false;
		videoText.text = "Fetch Video";
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnApplicationPause(bool isPaused) {                 
  		IronSource.Agent.onApplicationPause(isPaused);
	}

	public void ShowOfferwall() {
		IronSource.Agent.showOfferwall();
	}

	public void ShowVideo() {
		if (IronSource.Agent.isInterstitialReady()) {
			IronSource.Agent.showInterstitial();
			videoAdAvailable = false;
			videoText.text = "Fetch Video";
		} else {
			IronSource.Agent.loadInterstitial();
		}
	}

	public void ShowRewarded() {
		if (IronSource.Agent.isRewardedVideoAvailable()) {
			IronSource.Agent.showRewardedVideo();
		}
	}

	public void ToggleBanner() {
		if (!bannerShowing) {
			IronSource.Agent.loadBanner(IronSourceBannerSize.BANNER, IronSourceBannerPosition.BOTTOM);
			bannerShowing = true;
		} else {
			IronSource.Agent.hideBanner();
			bannerShowing = false;
		}
	}



	//************CALLBACKS************//
	
	//Invoked when the RewardedVideo ad view has opened.
	//Your Activity will lose focus. Please avoid performing heavy 
	//tasks till the video ad will be closed.
	void RewardedVideoAdOpenedEvent() {
	}  
	//Invoked when the RewardedVideo ad view is about to be closed.
	//Your activity will now regain its focus.
	void RewardedVideoAdClosedEvent() {
	}
	//Invoked when there is a change in the ad availability status.
	//@param - available - value will change to true when rewarded videos are available. 
	//You can then show the video by calling showRewardedVideo().
	//Value will change to false when no videos are available.
	void RewardedVideoAvailabilityChangedEvent(bool available) {
		//Change the in-app 'Traffic Driver' state according to availability.
		bool rewardedVideoAvailability = available;
	}
	//  Note: the events below are not available for all supported rewarded video 
	//   ad networks. Check which events are available per ad network you choose 
	//   to include in your build.
	//   We recommend only using events which register to ALL ad networks you 
	//   include in your build.
	//Invoked when the video ad starts playing.
	void RewardedVideoAdStartedEvent() {
	}
	//Invoked when the video ad finishes playing.
	void RewardedVideoAdEndedEvent() {
	}
	//Invoked when the user completed the video and should be rewarded. 
	//If using server-to-server callbacks you may ignore this events and wait for the callback from the  ironSource server.
	//
	//@param - placement - placement object which contains the reward data
	//
	void RewardedVideoAdRewardedEvent(IronSourcePlacement placement) {
	}
	//Invoked when the Rewarded Video failed to show
	//@param description - string - contains information about the failure.
	void RewardedVideoAdShowFailedEvent (IronSourceError error){
	}


	//Invoked when the initialization process has failed.
	//@param description - string - contains information about the failure.
	void InterstitialAdLoadFailedEvent (IronSourceError error) {
	}
	//Invoked right before the Interstitial screen is about to open.
	void InterstitialAdShowSucceededEvent() {
	}
	//Invoked when the ad fails to show.
	//@param description - string - contains information about the failure.
	void InterstitialAdShowFailedEvent(IronSourceError error) {
	}
	// Invoked when end user clicked on the interstitial ad
	void InterstitialAdClickedEvent () {
	}
	//Invoked when the interstitial ad closed and the user goes back to the application screen.
	void InterstitialAdClosedEvent () {
	}
	//Invoked when the Interstitial is Ready to shown after load function is called
	void InterstitialAdReadyEvent() {
		videoText.text = "Show Video";
	}
	//Invoked when the Interstitial Ad Unit has opened
	void InterstitialAdOpenedEvent() {
	}


	//Invoked once the banner has loaded
	void BannerAdLoadedEvent() {
	}
	//Invoked when the banner loading process has failed.
	//@param description - string - contains information about the failure.
	void BannerAdLoadFailedEvent (IronSourceError error) {
	}
	// Invoked when end user clicks on the banner ad
	void BannerAdClickedEvent () {
	}
	//Notifies the presentation of a full screen content following user click
	void BannerAdScreenPresentedEvent () {
	}
	//Notifies the presented screen has been dismissed
	void BannerAdScreenDismissedEvent() {
	}
	//Invoked when the user leaves the app
	void BannerAdLeftApplicationEvent() {
	}


	/**
	* Invoked when there is a change in the Offerwall availability status.
	* @param - available - value will change to YES when Offerwall are available. 
	* You can then show the video by calling showOfferwall(). Value will change to NO when Offerwall isn't available.
	*/
	void OfferwallAvailableEvent(bool canShowOfferwall) {
	}
	/**
	* Invoked when the Offerwall successfully loads for the user.
	*/ 
	void OfferwallOpenedEvent() {
	}
	/**
	* Invoked when the method 'showOfferWall' is called and the OfferWall fails to load.  
	*@param desc - A string which represents the reason of the failure.
	*/
	void OfferwallShowFailedEvent(IronSourceError error) {
	}
	/**
	* Invoked each time the user completes an offer.
	* Award the user with the credit amount corresponding to the value of the ‘credits’ 
	* parameter.
	* @param dict - A dictionary which holds the credits and the total credits.   
	*/
	void OfferwallAdCreditedEvent(Dictionary<string,object> dict) {
	}
	/**
	* Invoked when the method 'getOfferWallCredits' fails to retrieve 
	* the user's credit balance info.
	* @param desc -string object that represents the reason of the  failure. 
	*/
	void GetOfferwallCreditsFailedEvent(IronSourceError error) {
	}
	/**
	* Invoked when the user is about to return to the application after closing 
	* the Offerwall.
	*/
	void OfferwallClosedEvent() {
	}

}
