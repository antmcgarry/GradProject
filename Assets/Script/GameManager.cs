using UnityEngine;
using System.Collections.Generic;


public class GameManager : MonoBehaviour {

    //singleton pattern only allows one instance of the object running
    public static GameManager instance;


    public MatchSettings matchSettings;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("More Than one GameManager in the scene.");
        }
        instance = this;
    }

    #region Player tracking

    private const string PLAYER_ID_PREFIX = "Player ";
   

    private static Dictionary<string, Player> players = new Dictionary<string, Player>();

    public static void RegisterPlayer (string _netID, Player _player) // net ID = Server ID Number 
    {
        string _PlayerID = PLAYER_ID_PREFIX  + _netID;
        players.Add(_PlayerID, _player);
        _player.transform.name = _PlayerID;
    }



    public static void UnRegisterPlayer (string _playerID)
    {
        players.Remove(_playerID);
    }


    public static Player GetPlayer (string _playerID)
    {
        return players[_playerID];
    }

    //void OnGUI()
    //{
    //    GUILayout.BeginArea(new Rect(200, 200, 200, 500));
    //    GUILayout.BeginVertical();

    //    foreach (string _playerID in players.Keys)
    //    {
    //       GUILayout.Label( _playerID + "  -  " + players[_playerID].transform.name);
    //    }

    //    GUILayout.EndVertical();
    //    GUILayout.EndArea();
    //}

    #endregion

}
