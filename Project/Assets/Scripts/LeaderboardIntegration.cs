﻿using UnityEngine;
using System.Collections.Generic;

public class LeaderboardIntegration : MonoBehaviour
{
#if UNITY_IPHONE
	public List<GameCenterLeaderboard> _leaderboards;
	// delegate void LeaderboardData( List<GameCenterLeaderboard> leaderboards );
	// LeaderboardData leaderboardData;
	
	void OnEnable()
	{
		GameCenterBinding.authenticateLocalPlayer();
		GameCenterManager.categoriesLoaded += GatherLeaderboardData;
	}

		void Start()
	{
		// Listens to all the GameCenter events.  All event listeners MUST be removed before this object is disposed!
		// Player events
		GameCenterManager.loadPlayerDataFailed += loadPlayerDataFailed;
		GameCenterManager.playerDataLoaded += playerDataLoaded;
		GameCenterManager.playerAuthenticated += playerAuthenticated;
		GameCenterManager.playerFailedToAuthenticate += playerFailedToAuthenticate;
		GameCenterManager.playerLoggedOut += playerLoggedOut;
		GameCenterManager.profilePhotoLoaded += profilePhotoLoaded;
		GameCenterManager.profilePhotoFailed += profilePhotoFailed;
		
		// Leaderboards and scores
		GameCenterManager.loadCategoryTitlesFailed += loadCategoryTitlesFailed;
		GameCenterManager.categoriesLoaded += categoriesLoaded;
		GameCenterManager.reportScoreFailed += reportScoreFailed;
		GameCenterManager.reportScoreFinished += reportScoreFinished;
		GameCenterManager.retrieveScoresFailed += retrieveScoresFailed;
		GameCenterManager.scoresLoaded += scoresLoaded;
		GameCenterManager.retrieveScoresForPlayerIdFailed += retrieveScoresForPlayerIdFailed;
		GameCenterManager.scoresForPlayerIdLoaded += scoresForPlayerIdLoaded;
	}
	
	
	void OnDisable()
	{
		// Remove all the event handlers
		// Player events
		GameCenterManager.loadPlayerDataFailed -= loadPlayerDataFailed;
		GameCenterManager.playerDataLoaded -= playerDataLoaded;
		GameCenterManager.playerAuthenticated -= playerAuthenticated;
		GameCenterManager.playerLoggedOut -= playerLoggedOut;
		GameCenterManager.profilePhotoLoaded -= profilePhotoLoaded;
		GameCenterManager.profilePhotoFailed -= profilePhotoFailed;
		
		// Leaderboards and scores
		GameCenterManager.loadCategoryTitlesFailed -= loadCategoryTitlesFailed;
		GameCenterManager.categoriesLoaded -= categoriesLoaded;
		GameCenterManager.reportScoreFailed -= reportScoreFailed;
		GameCenterManager.reportScoreFinished -= reportScoreFinished;
		GameCenterManager.retrieveScoresFailed -= retrieveScoresFailed;
		GameCenterManager.scoresLoaded -= scoresLoaded;
		GameCenterManager.retrieveScoresForPlayerIdFailed -= retrieveScoresForPlayerIdFailed;
		GameCenterManager.scoresForPlayerIdLoaded -= scoresForPlayerIdLoaded;
	}

	void GatherLeaderboardData( List<GameCenterLeaderboard> leaderboards )
	{
		_leaderboards = leaderboards;
	}

	public void SubmitNewScore( int newScore )
	{
		GameCenterBinding.reportScore( newScore, "001" );
	}

	public void OpenLeaderboardView()
	{
		GameCenterBinding.showGameCenterViewController( GameCenterViewControllerState.Leaderboards, GameCenterLeaderboardTimeScope.AllTime, "001" );
	}

	#region Player Events
	
	void playerAuthenticated()
	{
		Debug.Log( "playerAuthenticated" );
	}
	
	
	void playerFailedToAuthenticate( string error )
	{
		Debug.Log( "playerFailedToAuthenticate: " + error );
	}
	
	
	void playerLoggedOut()
	{
		Debug.Log( "playerLoggedOut" );
	}
	

	void playerDataLoaded( List<GameCenterPlayer> players )
	{
		Debug.Log( "playerDataLoaded" );
		foreach( GameCenterPlayer p in players )
			Debug.Log( p );
	}
	
	
	void loadPlayerDataFailed( string error )
	{
		Debug.Log( "loadPlayerDataFailed: " + error );
	}
	
	
	void profilePhotoLoaded( string path )
	{
		Debug.Log( "profilePhotoLoaded: " + path );
	}
	
	
	void profilePhotoFailed( string error )
	{
		Debug.Log( "profilePhotoFailed: " + error );
	}
	
	#endregion;
	
	
	
	#region Leaderboard Events
	
	void categoriesLoaded( List<GameCenterLeaderboard> leaderboards )
	{
		Debug.Log( "categoriesLoaded" );
		foreach( GameCenterLeaderboard l in leaderboards )
			Debug.Log( l );
	}
	
	
	void loadCategoryTitlesFailed( string error )
	{
		Debug.Log( "loadCategoryTitlesFailed: " + error );
	}
	
	#endregion;

	
	#region Score Events
	
	void scoresLoaded( List<GameCenterScore> scores )
	{
		Debug.Log( "scoresLoaded" );
		foreach( GameCenterScore s in scores )
			Debug.Log( s );
	}
	
	
	void retrieveScoresFailed( string error )
	{
		Debug.Log( "retrieveScoresFailed: " + error );
	}
	
	
	void retrieveScoresForPlayerIdFailed( string error )
	{
		Debug.Log( "retrieveScoresForPlayerIdFailed: " + error );
	}
	
	
	void scoresForPlayerIdLoaded( List<GameCenterScore> scores )
	{
		Debug.Log( "scoresForPlayerIdLoaded" );
		foreach( GameCenterScore s in scores )
			Debug.Log( s );
	}
	
	
	void reportScoreFinished( string category )
	{
		Debug.Log( "reportScoreFinished for category: " + category );
	}
	

	void reportScoreFailed( string error )
	{
		Debug.Log( "reportScoreFailed: " + error );
	}
	
	#endregion;
	
	
	#region Achievement Events

	void achievementMetadataLoaded( List<GameCenterAchievementMetadata> achievementMetadata )
	{
		Debug.Log( "achievementMetadatLoaded" );
		foreach( GameCenterAchievementMetadata s in achievementMetadata )
			Debug.Log( s );
	}
	
	
	void retrieveAchievementMetadataFailed( string error )
	{
		Debug.Log( "retrieveAchievementMetadataFailed: " + error );
	}
	
	
	void resetAchievementsFinished()
	{
		Debug.Log( "resetAchievmenetsFinished" );
	}
	
	
	void resetAchievementsFailed( string error )
	{
		Debug.Log( "resetAchievementsFailed: " + error );
	}
	
	
	void achievementsLoaded( List<GameCenterAchievement> achievements )
	{
		Debug.Log( "achievementsLoaded" );
		foreach( GameCenterAchievement s in achievements )
			Debug.Log( s );
	}
	

	void loadAchievementsFailed( string error )
	{
		Debug.Log( "loadAchievementsFailed: " + error );
	}
	
	
	void reportAchievementFinished( string identifier )
	{
		Debug.Log( "reportAchievementFinished: " + identifier );
	}
	
	
	void reportAchievementFailed( string error )
	{
		Debug.Log( "reportAchievementFailed: " + error );
	}
	
	#endregion;

#endif
}