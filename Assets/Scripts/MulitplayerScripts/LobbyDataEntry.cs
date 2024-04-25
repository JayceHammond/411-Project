using UnityEngine;
using Steamworks;
using TMPro;

public class LobbyDataEntry : MonoBehaviour
{
    //Data
    public CSteamID lobbyID;
    public string lobbyName;
    public TextMeshProUGUI lobbyNameText;


    public void SetLobbyData(){
        if(lobbyName == ""){
            lobbyNameText.text = "Unamed Session";
        }else{
            lobbyNameText.text = lobbyName;
        }
    }

    public void JoinLobby(){
        SteamLobby.Instance.JoinLobby(lobbyID);
    }
}
