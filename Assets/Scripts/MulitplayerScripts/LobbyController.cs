using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Mirror.Examples.Basic;
using Steamworks;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    //Ready
    public Button StartGameButton;
    public TextMeshProUGUI ReadyButtonText;

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

    public void ReadyPlayer(){
        LocalPlayerController.ChangeReady();
    }

    public void UpdateButton(PlayerObjectController player){
        if(player.Ready){
            ReadyButtonText.text = "Unready";
        }else{
            ReadyButtonText.text = "Ready";
        }
    }

    public void CheckIfAllReady(){
        bool AllReady = false;
        foreach(PlayerObjectController player in Manager.GamePlayers){
            if(player.Ready){
                AllReady = true;
            }else{
                AllReady = false;
                break;
            }
        }
        if(AllReady){
            if(LocalPlayerController.PlayerIdNumber == 1){
                StartGameButton.interactable = true;
            }else{
                StartGameButton.interactable = false;
            }
        }else{
            StartGameButton.interactable = false;
        }
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
            NewPlayerItemScript.Ready = player.Ready;
            NewPlayerItemScript.SetPlayerValues();


            NewPlayerItem.transform.SetParent(PlayerListViewContent.transform);
            NewPlayerItem.transform.localScale = Vector3.one;

            PlayerListItems.Add(NewPlayerItemScript);
            Debug.Log(player.name);
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
                NewPlayerItemScript.Ready = player.Ready;
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
                    PlayerListItemScript.Ready = player.Ready;
                    PlayerListItemScript.PlayerName = player.PlayerName;
                    PlayerListItemScript.SetPlayerValues();
                   /* if(player == LocalPlayerController){
                        UpdateButton();
                    }*/
                    UpdateButton(player);
                }
            }
        }
        CheckIfAllReady();
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


//LOADS MULTIPLAYER TEST SCENE: WILL CHANGE
    public void StartGame(){
        SceneManager.LoadScene(2);
    }
}
