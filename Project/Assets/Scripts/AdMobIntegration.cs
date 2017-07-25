using UnityEngine;
using System.Collections;

public class AdMobIntegration : MonoBehaviour
{
#if UNITY_IPHONE	
	void Start () 
	{
		AdMobBinding.setTestDevices( new string[] { "a36f6a18e79b86938f1e5e57b9605c3686457ae9" } );
		AdMobBinding.init( "pub-2383405661596622", false );

		if( UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad1Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad2Gen
			&& UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad3Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPad4Gen && UnityEngine.iOS.Device.generation != UnityEngine.iOS.DeviceGeneration.iPadMini1Gen )
		{
			AdMobBinding.createBanner( "ca-app-pub-2383405661596622/2638952592", AdMobBannerType.iPhone_320x50, AdMobAdPosition.BottomRight );
		}
		else
		{
			// AdMobBinding.createBanner( "ca-app-pub-2383405661596622/5699631794", AdMobBannerType.iPad_320x250, AdMobAdPosition.TopCenter );
		}
	}
#endif

}