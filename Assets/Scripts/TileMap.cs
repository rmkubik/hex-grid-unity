using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMap : MonoBehaviour
{
  public GameObject tilePrefab;
  private Object[] forestTextures;
  private GameObject tile;
  private GameObject[] tiles;
  private HexGrid<GameObject> grid;

  int TILE_WIDTH = 380;
  int TILE_HEIGHT = 380;
  public int Y_OFFSET = 260;
  public int X_OFFSET = -62;
  public int ROW_OFFSET = 238;
  public int height = 10;
  public int width = 10;
  public Vector2 gridOffset = new Vector2(-3, -2);

  // Start is called before the first frame update
  void Start()
  {
    forestTextures = Resources.LoadAll("Tiles/Tiles Forests", typeof(Texture2D));

    grid = new HexGrid<GameObject>(width, height, (Location location) =>
    {
      var cell = Instantiate(
        tilePrefab,
        CalculatePositionFromLocation(location),
        Quaternion.identity
      );

      var spriteRenderer = cell.GetComponent<SpriteRenderer>();
      spriteRenderer.sortingOrder = GetSortingOrderFromLocation(location);

      Texture2D forestTexture = (Texture2D)forestTextures[Random.Range(0, forestTextures.Length)];

      spriteRenderer.sprite = Sprite.Create(
        forestTexture,
        new Rect(0, 0, TILE_WIDTH, TILE_HEIGHT),
        new Vector2(0.5f, 0.5f),
        TILE_WIDTH
      );

      return cell;
    });

    // grid.ForEach((cell, cells) => Debug.Log(cell));

    // DrawTile();
  }

  // Update is called once per frame
  void Update()
  {
    grid.ForEach((GridCell<GameObject> cell, GridCell<GameObject>[] cells) =>
    {
      cell.value.transform.position = CalculatePositionFromLocation(cell.location);
    });

    if (Input.anyKeyDown)
    {
      //   DrawTile();
    }
  }

  void DrawTile()
  {
    if (!tile)
    {
      tile = Instantiate(tilePrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    Texture2D forestTexture = (Texture2D)forestTextures[Random.Range(0, forestTextures.Length)];

    var renderer = tile.GetComponent<SpriteRenderer>();
    renderer.sprite = Sprite.Create(
        forestTexture,
        new Rect(0, 0, TILE_WIDTH, TILE_HEIGHT),
        new Vector2(0.5f, 0.5f)
    );
  }

  Vector3 CalculatePositionFromLocation(Location location)
  {
    float yPosition = location.row;

    float yUnitOffset = (float)Y_OFFSET / (float)TILE_HEIGHT;
    bool isEvenCol = location.col % 2 == 0;
    if (isEvenCol)
    {
      yPosition -= (0.5f - yUnitOffset);
    }

    float rowUnitOffset = (float)ROW_OFFSET / (float)TILE_HEIGHT;
    yPosition -= rowUnitOffset * location.row;

    yPosition += gridOffset.y;

    float xPosition = location.col;

    float xUnitOffset = (float)X_OFFSET / (float)TILE_WIDTH;
    xPosition -= (0.25f - xUnitOffset) * location.col;

    xPosition += gridOffset.x;

    return new Vector3(xPosition, yPosition, 0);
  }

  int GetSortingOrderFromLocation(Location location)
  {
    int sortingOrder = (height - location.row) * 2;

    bool isEvenCol = location.col % 2 == 0;
    if (isEvenCol)
    {
      sortingOrder -= 1;
    }

    return sortingOrder;
  }
}
