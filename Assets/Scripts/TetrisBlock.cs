using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TetrisBlock : MonoBehaviour {
    public Vector3 rotationPoint;
    private float previousTime;
    public float idleFallTime = .8f;
    public static int height = 20;
    public static int width = 10;
    public static Transform[,] grid = new Transform[width, height];

    // Start is called before the first frame update
    void Start() {
    }

    public void GameOver() {
      SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update() {

        float fallTime = Input.GetKey(KeyCode.DownArrow) ? idleFallTime / 10 : idleFallTime;
        float blocksDown = transform.position.y;

        void CheckForLines() {
        // This function checks for rows that are completed
            for (int i = height-1; i >= 0; i--) {
            // Go through entire game grid and delete any lines
                if (HasLine(i))
                {
                    DeleteLine(i);
                    RowDown(i);
                }
            }
        }

        bool HasLine(int i) {
            for (int j = 0; j < width; j++) {
                if (grid[j, i] == null) {
                    return false;
                }
            }
            return true;
        }

        void DeleteLine(int i) {
            for (int j = 0; j < width; j++) {
                Destroy(grid[j, i].gameObject);
                grid[j, i] = null;
            }
        }

        void RowDown(int i) {
            for (int y = i; y < height; y++) {
                for (int j = 0; j < width; j++) {
                    if (grid[j,y] != null) {
                        grid[j, y - 1] = grid[j, y];
                        grid[j, y] = null;
                        grid[j, y - 1].transform.position += new Vector3(0, -1, 0);
                    }
                }
            }
        }

        void AddToGrid() {
            foreach (Transform children in transform) {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);

                grid[roundedX, roundedY] = children;
            }
        }
        bool ValidMove() {
            foreach (Transform children in transform) {
                int roundedX = Mathf.RoundToInt(children.transform.position.x);
                int roundedY = Mathf.RoundToInt(children.transform.position.y);

                if (roundedX < 0 || roundedX >= width || roundedY < 0 || roundedY >= 20) {
                    return false;
                }

                if (grid[roundedX, roundedY] != null) {
                    return false;
                }
            }
            return true;
        }
        bool AboveGame() {
            foreach (Transform children in transform) {
                int roundedY = Mathf.RoundToInt(children.transform.position.y);

                if (roundedY >= 20) {
                    return true;
                }
            }
            return false;
        }

        if (AboveGame()) {
          GameOver();
          enabled = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow)) {
            transform.position += new Vector3(-1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow)) {
            transform.position += new Vector3(1, 0, 0);
        }
        if (Input.GetKeyDown(KeyCode.Space)) {
            idleFallTime = 0f;
            for (int i = 0; i < 20; i++){
              transform.position += new Vector3(0,-1,0);
              if (!ValidMove()) {
                transform.position += new Vector3(0,1,0);
                return;
              }
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.C)) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
        }
        if (Input.GetKeyDown(KeyCode.X)) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }

        Vector3 previousPosition = transform.position;

        if (!ValidMove()){
          transform.position += new Vector3(0, 1, 0);
          if (AboveGame()){
            GameOver();
          }
          transform.position = previousPosition;
        }
        for (int i = 0; i < 3; i++) {
            if (!ValidMove()) {
                transform.position += new Vector3(1, 0, 0);
            }
        }
        if (!ValidMove()) {
            transform.position = previousPosition;
        }
        for (int i = 0; i < 3; i++) {
            if (!ValidMove()) {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if (!ValidMove()) {
            transform.position = previousPosition;
        }
        if (!ValidMove()) {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
        }
        if (Time.time - previousTime > fallTime) {
            transform.position += new Vector3(0, -1, 0);
            previousTime = Time.time;
            if (!ValidMove()) {
                transform.position += new Vector3(0, 1, 0);
                AddToGrid();
                CheckForLines();
                this.enabled = false;
                FindObjectOfType<Spawner>().NewTetrimino();
            }
        }
    }
}
