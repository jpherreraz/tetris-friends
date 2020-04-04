using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] Tetriminoes;

    // Start is called before the first frame update
    void Start()
    {
        NewTetrimino();
    }

    // Update is called once per frame
    public void NewTetrimino()
    {
        Instantiate(Tetriminoes[Random.Range(0, Tetriminoes.Length)], transform.position, Quaternion.identity);
    }
}
