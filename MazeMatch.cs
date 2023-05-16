using System;
using System.Collections.Generic;
using UnityEngine;

public class MazeGeneratorCell {

    public int X;
    public int Y;

    public bool WallLeft = true;
    public bool WallBottom = true;

    public bool use = false;

    public int LengthAtStart = 0;
}


public class MazeMatch : MonoBehaviour {

    public int width = 0;
    public int height = 0;

    private int typeCreate = 0;
    private int startX = 0;
    private int startY = 0;

    public Vector2Int finish = Vector2Int.zero;

    public MazeMatch(int W = 0, int H = 0, int type = 0, int startX = 0, int startY = 0) {
        width = W;
        height = H;

        typeCreate = type;
        this.startX = startX;
        this.startY = startY;
    }

    public MazeGeneratorCell[,] GenerationMaze() {

        MazeGeneratorCell[,] maze = new MazeGeneratorCell[width, height];

        for (int x = 0; x < width; x++) {
            for (int y = 0; y < height; y++) {
                maze[x, y] = new MazeGeneratorCell { X = x, Y = y };
            }
        }

        MatchWall_Algorithm(maze);

        //Delet last wall
        for(int x = 0; x < width; x++) {
            maze[x, height - 1].WallLeft = false;
        }

        for(int y = 0; y < height; y++) {
            maze[width - 1, y].WallBottom = false;
        }

        CreateExitMaze(maze);

        return maze;
    }

    private void MatchWall_Algorithm(MazeGeneratorCell[,] maze) {

        MazeGeneratorCell currentCell = maze[startX, startY];
        currentCell.use = true;
        currentCell.LengthAtStart = 0;

        Stack<MazeGeneratorCell> stack = new Stack<MazeGeneratorCell>();

        int tmp = 1;
        do {
            List<MazeGeneratorCell> dontUse = new List<MazeGeneratorCell>();

            int x = currentCell.X;
            int y = currentCell.Y;

            if (typeCreate == 0) {
                //Left
                if (x > 0 && !maze[x - 1, y].use) { dontUse.Add(maze[x - 1, y]); tmp = 1; }
                //Bottom
                if (y > 0 && !maze[x, y - 1].use) { dontUse.Add(maze[x, y - 1]); tmp = 2; }
                //Right + 1 side
                if (x < width - 2 && !maze[x + 1, y].use) { dontUse.Add(maze[x + 1, y]); tmp = 3; }
                //Top + 1 side
                if (y < height - 2 && !maze[x, y + 1].use) { dontUse.Add(maze[x, y + 1]); tmp = 4; }
            } else {

                switch(tmp) {
                    case 1:
                        if (y > 0 && !maze[x, y - 1].use) { dontUse.Add(maze[x, y - 1]); tmp = 2; }
                        //Right + 1 side
                        if (x < width - 2 && !maze[x + 1, y].use) { dontUse.Add(maze[x + 1, y]); tmp = 3; }
                        //Top + 1 side
                        if (y < height - 2 && !maze[x, y + 1].use) { dontUse.Add(maze[x, y + 1]); tmp = 4; }
                        break;
                    case 2:
                        //Left
                        if (x > 0 && !maze[x - 1, y].use) { dontUse.Add(maze[x - 1, y]); tmp = 1; }
                        //Right + 1 side
                        if (x < width - 2 && !maze[x + 1, y].use) { dontUse.Add(maze[x + 1, y]); tmp = 3; }
                        //Top + 1 side
                        if (y < height - 2 && !maze[x, y + 1].use) { dontUse.Add(maze[x, y + 1]); tmp = 4; }
                        break;
                    case 3:
                        //Left
                        if (x > 0 && !maze[x - 1, y].use) { dontUse.Add(maze[x - 1, y]); tmp = 1; }
                        //Bottom
                        if (y > 0 && !maze[x, y - 1].use) { dontUse.Add(maze[x, y - 1]); tmp = 2; }
                        //Top + 1 side
                        if (y < height - 2 && !maze[x, y + 1].use) { dontUse.Add(maze[x, y + 1]); tmp = 4; }
                        break;
                    case 4:
                        //Left
                        if (x > 0 && !maze[x - 1, y].use) { dontUse.Add(maze[x - 1, y]); tmp = 1; }
                        //Bottom
                        if (y > 0 && !maze[x, y - 1].use) { dontUse.Add(maze[x, y - 1]); tmp = 2; }
                        //Right + 1 side
                        if (x < width - 2 && !maze[x + 1, y].use) { dontUse.Add(maze[x + 1, y]); tmp = 3; }
                        break;
                }
            }

            if (dontUse.Count > 0) {

                MazeGeneratorCell chosenCell = dontUse[UnityEngine.Random.Range(0, dontUse.Count)];
                RemoveWall(currentCell, chosenCell);

                chosenCell.use = true;
                stack.Push(chosenCell);
                chosenCell.LengthAtStart = currentCell.LengthAtStart + 1;
                currentCell = chosenCell;

            } else {
                currentCell = stack.Pop();
            }

        } while (stack.Count > 0);
    }

    private void RemoveWall(MazeGeneratorCell currentCell, MazeGeneratorCell chosenCell) {

        if (currentCell.X == chosenCell.X) {
            if (currentCell.Y > chosenCell.Y) currentCell.WallBottom = false;
            else chosenCell.WallBottom = false;

        } else {
            if (currentCell.X > chosenCell.X) currentCell.WallLeft = false;
            else chosenCell.WallLeft = false;
        }
    }


    private void CreateExitMaze(MazeGeneratorCell[,] maze) {
        MazeGeneratorCell select = maze[0, 0];

        for (int x = 0; x < width; x++) {
            if (maze[x, height - 2].LengthAtStart > select.LengthAtStart) select = maze[x, height - 2];
            if (maze[x, 0].LengthAtStart > select.LengthAtStart) select = maze[x, 0];
        }

        for (int y = 0; y < height; y++) {
            if (maze[width - 2, y].LengthAtStart > select.LengthAtStart) select = maze[width - 2, y];
            if (maze[0, y].LengthAtStart > select.LengthAtStart) select = maze[0, y];
        }


        if (select.X == 0) select.WallLeft = false;
        else if (select.Y == 0) select.WallBottom = false;
        else if (select.X == width - 2) maze[select.X + 1, select.Y].WallLeft = false;
        else if (select.Y == height - 2) maze[select.X, select.Y + 1].WallBottom = false;

        finish = new Vector2Int(select.X, select.Y);
    }
}
