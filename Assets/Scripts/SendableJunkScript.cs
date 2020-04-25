using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Photon.Pun;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

public class SendableJunkScript : MonoBehaviour {
    public GameObject[] SendableJunk;
    public static int linesCleared = 1;

    public void NewLine() {
        int count = 0;
        int randomNumber = Random.Range(0, SendableJunk.Length);
        for (int i = 0; i < linesCleared; i++) {
            Instantiate(SendableJunk[randomNumber], new Vector3(0, TetrisBlock.bottom + count, 0), Quaternion.identity);
            count += 1;
        }
    }

    void ReceiveJunk() {
        NewLine();
    }
    private void Update() {
        if (TetrisBlock.blockIsDown == true) {
            linesCleared = TetrisBlock.linesCleared;
            NewLine();
            TetrisBlock.blockIsDown = false;
            TetrisBlock.linesCleared = 0;
            linesCleared = 0;
        }    
    }

    [PunRPC]
    void LinesCleared() {
        //ReceiveJunk();
    }
}
