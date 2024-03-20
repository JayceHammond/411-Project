using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Steamworks;
using TMPro;
using UnityEngine;

public class LobbyController : MonoBehaviour
{
    public static LobbyController Instance;

    //UI Elements
    public TextMeshProUGUI LobbyNameText;

    //Player Data
    public GameObject PlayerListViewContent;
    public GameObject PlayerListItemPrefab;
    public GameObject LocalPlayerObject;

    //Other Data
    public ulong CurrentLobbyID;
    public bool PlayerItemCreated = false;
    private List<PlayerListItem> PlayerListItems = new List<PlayerListItem>();
    public PlayerObjectController LocalPlayerController;

    //Manager
    private CustomNetworkManager manager;

    private CustomNetworkManager Manager{
        get{
            if(manager != null){
                return manager;
            }
            return manager = CustomNetworkManager.singleton as CustomNetworkManager;
        }
    }

    public void Awake(){
        if(Instance == null){Instance = this;}
    }

    public void UpdateLobbyName(){
        CurrentLobbyID = Manager.GetComponent<SteamLobby>().CurrentLobbyID;
        LobbyNameText.text = SteamMatchmaking.GetLobbyData(new CSteamID(CurrentLobbyID), "name");
    }

    public void UpdatePlayerList(){
        if(!PlayerItemCreated){CreateHostPlayerItem();} //Host
        if(PlayerListItems.Count < Manager.GamePlayers.Count) {CreateClientPlayerItem();}
        if(PlayerListItems.Count > Manager.GamePlayers.Count) {RemovePlayerItem();}
        if(PlayerListItems.Count == Manager.GamePlayers.Count) {UpdatePlayerItem();}
    }

    public void FindLocalPlayer(){
        LocalPlayerObject = GameObject.Find("LocalGamePlayer");
        LocalPlayerController = LocalPlayerObject.GetComponent<PlayerObjectController>();
    }

    public void CreateHostPlayerItem(){
        foreach(PlayerObjectController player in Manager.GamePlayers){
            GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
            PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

            NewPlayerItemScript.PlayerName = player.PlayerName;
            NewPlayerItemScript.ConnectionID = player.ConnectionID;
            NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
            NewPlayerItemScript.SetPlayerValues();


            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
        }
        PlayerItemCreated = true;
    }


    public void CreateClientPlayerItem(){
        foreach(PlayerObjectController player in Manager.GamePlayers){
            if(!PlayerListItems.Any(x => x.ConnectionID == player.ConnectionID)){
                GameObject NewPlayerItem = Instantiate(PlayerListItemPrefab) as GameObject;
                PlayerListItem NewPlayerItemScript = NewPlayerItem.GetComponent<PlayerListItem>();

                NewPlayerItemScript.PlayerName = player.PlayerName;
                NewPlayerItemScript.ConnectionID = player.ConnectionID;
                NewPlayerItemScript.PlayerSteamID = player.PlayerSteamID;
                NewPlayerItemScript.SetPlayerValues();


                NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
                NewPlayerItem.transform.localScale = Vector3.one;

                PlayerListItems.Add(NewPlayerItemScript);
            }
        }
    }

    public void UpdatePlayerItem(){
        foreach(PlayerObjectController player in Manager.GamePlayers){
            foreach(PlayerListItem PlayerListItemScript in PlayerListItems){
                if(PlayerListItemScript.ConnectionID == player.ConnectionID){
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.SetPlayerValues();
                }
            }
        }
    }

    public void RemovePlayerItem(){
        List<PlayerListItem> playerListItemToRemove = new List<PlayerListItem>();

        foreach(PlayerListItem playerlistItem in playerListItemToRemove){
            if(!Manager.GamePlayers.Any(x => x.ConnectionID == playerlistItem.ConnectionID)){
                playerListItemToRemove.Add(playerlistItem);
            }
        }
        if(playerListItemToRemove.Count > 0){
            foreach(PlayerListItem playerItemToRemove in playerListItemToRemove){
                GameObject ObjectToRemove = playerItemToRemove.gameObject;
                PlayerListItems.Remove(playerItemToRemove);
                Destroy(ObjectToRemove);
                ObjectToRemove = null;
            }
        }
    }

}
