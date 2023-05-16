using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineHelp : MonoBehaviour {

    [SerializeField] public MazeSpawner mazeSpawner;

    public LineRenderer _lineRender;

    private void Awake() {
        _lineRender = gameObject.GetComponent<LineRenderer>();
    }

    public void DrawPath(Vector2Int finish, MazeGeneratorCell[,] maze) {
        int x = finish.x;
        int y = finish.y;
        List<Vector3> positions = new List<Vector3>();

        Vector3 oldVec = new Vector3(x,y,0);
        //(x != 0 || y != 0) && positions.Count < 1000
        while (oldVec != mazeSpawner._player.transform.position) {
            if (positions.Count > 500) break;
            oldVec = new Vector3(x * mazeSpawner.sizeX, y * mazeSpawner.sizeY, 0);
            positions.Add(oldVec);

            MazeGeneratorCell currentCell = maze[x, y];

            if (x > 0 && !currentCell.WallLeft && maze[x - 1, y].LengthAtStart < currentCell.LengthAtStart) {
                x--;
            } else if (y > 0 &&
                  !currentCell.WallBottom &&
                  maze[x, y - 1].LengthAtStart < currentCell.LengthAtStart) {
                y--;
            } else if (x < maze.GetLength(0) - 1 &&
                  !maze[x + 1, y].WallLeft &&
                  maze[x + 1, y].LengthAtStart < currentCell.LengthAtStart) {
                x++;
            } else if (y < maze.GetLength(1) - 1 &&
                  !maze[x, y + 1].WallBottom &&
                  maze[x, y + 1].LengthAtStart < currentCell.LengthAtStart) {
                y++;
            }
        }


        if (gameObject.activeSelf == false) {
            gameObject.SetActive(true);
            _lineRender.positionCount = positions.Count;
            _lineRender.SetPositions(positions.ToArray());
            gameObject.SetActive(false);
        }else {
            _lineRender.positionCount = positions.Count;
            _lineRender.SetPositions(positions.ToArray());
        }
    }
}
