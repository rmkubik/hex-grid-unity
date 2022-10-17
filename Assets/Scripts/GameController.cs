using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private Camera mainCamera;
  private GameState gameState;

  // Start is called before the first frame update
  void Start()
  {
    // GameState
    // - tiles in deck
    // - bank slots
    // - calculated population - this is just derived state
    gameState = new GameState();

    gameState.AddTileToDeck("test");
  }

  // Update is called once per frame
  void Update()
  {

  }

  void AdjustCamera(Camera camera)
  {
    float height = 2f * camera.orthographicSize;
    float width = height * camera.aspect;

    camera.backgroundColor = new Color32(69, 69, 69, 255);
  }
}
