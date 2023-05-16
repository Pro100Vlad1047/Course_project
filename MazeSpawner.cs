using UnityEngine;

public class MazeSpawner : MonoBehaviour {

    [Header("Setting")]
    [SerializeField] public int WidthMaze = 27;
    [SerializeField] public int HeightMaze = 15;

    [Header("Setting Cell")]
    [SerializeField] public float sizeX = 1;
    [SerializeField] public float sizeY = 1;

    [Header("Prefab")]
    [SerializeField] public GameObject CellPrefab;
    [SerializeField] public Transform _player;

    [Header("Line")]
    [SerializeField] public LineHelp lineHelp;

    public UISetting set;
    private GameObject _parentCell = null;

    public void CreateSettingAndMaze(UISetting set) {
        WidthMaze = set.size_Weight;
        HeightMaze = set.size_Height;

        this.set = set;
        CreateMazeAll();
    }

    public void CreateMazeAll() {
        if (_parentCell != null) {
            Destroy(_parentCell);
        }

        if (gameObject.activeSelf == false) {
            gameObject.SetActive(true);
            lineHelp._lineRender.positionCount = 0;
            gameObject.SetActive(false);
        } else {
            lineHelp._lineRender.positionCount = 0;
        }



        CreateParentsCell();
        _player.transform.position = new Vector3(sizeX * set.startCellX, sizeY * set.startCellY, 0);

        MazeMatch generator = new MazeMatch(WidthMaze, HeightMaze, set.typeGeneration, set.startCellX, set.startCellY);
        MazeGeneratorCell[,] maze = generator.GenerationMaze();


        for (int x = 0; x < WidthMaze; x++) {
            for (int y = 0; y < HeightMaze; y++) {
                Cell c = Instantiate(CellPrefab, new Vector2(x * sizeX, y * sizeY), Quaternion.identity).GetComponent<Cell>();

                c.wall_L.SetActive(maze[x, y].WallLeft);
                c.wall_B.SetActive(maze[x, y].WallBottom);

                c.gameObject.transform.SetParent(_parentCell.transform);
            }
        }

        lineHelp.DrawPath(generator.finish, maze);
    }

    private void CreateParentsCell() {
        _parentCell = GameObject.CreatePrimitive(PrimitiveType.Cube);
        _parentCell.GetComponent<Collider>().enabled = false;
        _parentCell.GetComponent<MeshRenderer>().enabled = false;
        _parentCell.transform.position = Vector3.zero;
        _parentCell.name = "Maze";
    }

}
