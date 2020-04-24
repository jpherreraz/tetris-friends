using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class GameOver : MonoBehaviourPunCallbacks
{

  public Text gameOver;

  public void PlayAgain() {
    PhotonNetwork.LoadLevel("Launcher");
  }

  public void updateText() {
    // if(PhotonNetwork.LocalPlayer.GetScore() == 1){
    if(TetrisBlock.loser == true){
      gameOver.text = "You lose!";
    }
    else {
      gameOver.text = "You win!";
    }


  }

  void Start() {
    updateText();
    Debug.Log(PhotonNetwork.LocalPlayer.GetScore());
    PhotonNetwork.Disconnect();
  }


}
