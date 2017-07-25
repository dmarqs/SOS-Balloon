using UnityEngine;
using System.Collections.Generic;
using Prime31;


public class AdMobGUIManager : MonoBehaviourGUI
{
#if UNITY_IPHONE

	void OnGUI()
	{
		beginColumn();


		if( GUILayout.Button( "Initialize AdMob and Set Test Devices" ) )
		{
			// replace with your test devices and publisher ID!
			AdMobBinding.setTestDevices( new string[] { "079adeed23ef3e9a9ddf0f10c92b8e18", "2370bc487b3a1efb28baed63a6acbf20", "b4ac20fd299d84394886abd987f8786e" } );
			AdMobBinding.init( "a150630f69e2282", true );
		}


		if( GUILayout.Button( "Portrait Smart Banner (top right)" ) )
		{
			AdMobBinding.createBanner( AdMobBannerType.SmartBannerPortrait, AdMobAdPosition.TopRight );
		}


		if( GUILayout.Button( "Landscape Smart Banner (bottom)" ) )
		{
			AdMobBinding.createBanner( AdMobBannerType.SmartBannerLandscape, AdMobAdPosition.BottomCenter );
		}


		if( UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad1Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad2Gen
			&& UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad3Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad4Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPadMini1Gen )
		{
			if( GUILayout.Button( "320x50 Banner (bottom right)" ) )
			{
				// replace the adUnitId with your own!
				AdMobBinding.createBanner( "ca-app-pub-8386987260001674/2631573141", AdMobBannerType.iPhone_320x50, AdMobAdPosition.BottomRight );
			}
		}
		else
		{
			if( GUILayout.Button( "320x250 Banner (bottom)" ) )
			{
				// replace the adUnitId with your own!
				AdMobBinding.createBanner( "ca-app-pub-8386987260001674/2631573141", AdMobBannerType.iPad_320x250, AdMobAdPosition.BottomCenter );
			}


			if( GUILayout.Button( "468x60 Banner (top)" ) )
			{
				AdMobBinding.createBanner( AdMobBannerType.iPad_468x60, AdMobAdPosition.TopCenter );
			}


			if( GUILayout.Button( "728x90 Banner (bottom)" ) )
			{
				AdMobBinding.createBanner( AdMobBannerType.iPad_728x90, AdMobAdPosition.BottomCenter );
			}
		}


		if( GUILayout.Button( "Destroy Banner" ) )
		{
			AdMobBinding.destroyBanner();
		}


		endColumn( true );


		if( GUILayout.Button( "Request Interstitial" ) )
		{
			// replace the adUnitId with your own!
			AdMobBinding.requestInterstital( "ca-app-pub-8386987260001674/7061772743" );
		}


		if( GUILayout.Button( "Is Interstial Loaded?" ) )
		{
			Debug.Log( "is interstitial loaded: " + AdMobBinding.isInterstitialAdReady() );
		}


		if( GUILayout.Button( "Show Interstitial" ) )
		{
			AdMobBinding.displayInterstital();
		}

		endColumn();
	}
#endif
}
