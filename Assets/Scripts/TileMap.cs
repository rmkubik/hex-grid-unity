using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileMap : MonoBehaviour
{
  public GameObject hexPrefab;
  public GameObject tilePrefab;
  private Object[] forestTextures;
  private Object[] locationTextures;
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
  public bool renderHexBorders = false;

  // Start is called before the first frame update
  void Start()
  {
    forestTextures = Resources.LoadAll("Hexes/Tiles Forests", typeof(Texture2D));
    locationTextures = Resources.LoadAll("Tiles/Locations 134x134", typeof(Texture2D));

    grid = new HexGrid<GameObject>(width, height, (Location location) =>
    {
      var cell = Instantiate(
        hexPrefab,
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
  }

  // Update is called once per frame
  void Update()
  {
    grid.ForEach((GridCell<GameObject> cell, GridCell<GameObject>[] cells) =>
    {
      cell.value.transform.position = CalculatePositionFromLocation(cell.location);

      var lineRenderer = cell.value.GetComponent<LineRenderer>();
      lineRenderer.positionCount = 7;

      if (renderHexBorders)
      {
        var collider = cell.value.GetComponent<PolygonCollider2D>();

        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.005f;

        var colliderPoints = collider.points.Select(point => new Vector3(point.x + cell.value.transform.position.x, point.y + cell.value.transform.position.y, 0));
        colliderPoints = colliderPoints.Append(colliderPoints.First());
        lineRenderer.SetPositions(colliderPoints.ToArray());
      }
      else
      {
        lineRenderer.positionCount = 0;
      }
    });

    // TODO: We're going to replace any Input handling with Rewired
    // Double checking this is what I should use first though.
    if (Input.GetButtonDown("Fire1"))
    {
      Vector3 mousePosition = Input.mousePosition;

      var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

      grid.ForEach((GridCell<GameObject> cell, GridCell<GameObject>[] cells) =>
      {
        var collider = cell.value.GetComponent<PolygonCollider2D>();

        if (collider.OverlapPoint(mouseWorldPosition))
        {
          var tileOffset = new Vector3(0, -0.13f, 0);

          var tile = Instantiate(
            tilePrefab,
            CalculatePositionFromLocation(cell.location) + tileOffset,
            Quaternion.identity
          );

          var spriteRenderer = tile.GetComponent<SpriteRenderer>();
          spriteRenderer.sortingOrder = GetSortingOrderFromLocation(cell.location) + 1;

          Texture2D tileTexture = (Texture2D)locationTextures[Random.Range(0, locationTextures.Length)];

          spriteRenderer.sprite = Sprite.Create(
            tileTexture,
            new Rect(0, 0, tileTexture.width, tileTexture.height),
            new Vector2(0.5f, 0.5f),
            TILE_WIDTH
          );
        }
      });

      Debug.Log($"{mousePosition.x}:{mousePosition.y}");
    }
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
