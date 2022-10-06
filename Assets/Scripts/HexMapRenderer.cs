using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HexMapRenderer : MonoBehaviour
{
  public GameObject tilePrefab;
  public int tileWidth; // 340
  public int tileHeight; // 134 + 36 pixel offset from bottom of texture

  private Layout layout;
  private Texture2D[] forestTextures;

  private List<GameObject> tiles = new List<GameObject>();


  // Start is called before the first frame update
  void Start()
  {
    // Convert tile dimensions to hex size as per redblobgames spec
    var hexSize = new Point(tileWidth / 2, tileHeight / (float)Math.Sqrt(3));
    layout = new Layout(Layout.flat, hexSize, new Point(0, 0), tileWidth);
    forestTextures = Resources
      .LoadAll("Tiles/Tiles Forests", typeof(Texture2D))
      .Cast<Texture2D>()
      .ToArray();

    CreateTile(new Hex(0, 0));
    CreateTile(new Hex(0, 1));
    CreateTile(new Hex(1, 0));
    CreateTile(new Hex(1, 1));
  }

  // Update is called once per frame
  void Update()
  {

  }

  void CreateTile(Hex hex)
  {
    var tile = Instantiate(
        tilePrefab,
        layout.HexToUnit(hex),
        Quaternion.identity
      );

    var spriteRenderer = tile.GetComponent<SpriteRenderer>();
    spriteRenderer.sortingOrder = (int)-(tile.transform.position.y * 100);

    Texture2D forestTexture = forestTextures[UnityEngine.Random.Range(0, forestTextures.Length)];

    spriteRenderer.sprite = Sprite.Create(
      forestTexture,
      new Rect(0, 0, forestTexture.width, forestTexture.height),
      new Vector2(0.5f, 0.5f),
      forestTexture.width
    );

    var lineRenderer = tile.GetComponent<LineRenderer>();
    lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
    lineRenderer.widthMultiplier = 0.025f;

    var corners = layout.PolygonCorners(hex);
    corners.Add(corners[0]);
    lineRenderer.positionCount = 7;
    for (int i = 0; i < 7; i++)
    {
      Vector3 pos = corners[i]; // Positions of hex vertices
      lineRenderer.SetPosition(i, pos);
    }



    Debug.Log(tile.transform.position);

    tiles.Add(tile);
  }


}
