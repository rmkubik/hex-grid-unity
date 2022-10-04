using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  private Camera mainCamera;

  // Start is called before the first frame update
  void Start()
  {
    mainCamera = Camera.main;
    float height = 2f * mainCamera.orthographicSize;
    float width = height * mainCamera.aspect;

    mainCamera.backgroundColor = new Color32(69, 69, 69, 255);

    // Set lower left corner of mainCamera to (0,0)
    // mainCamera.transform.position = new Vector3(
    //   mainCamera.transform.position.x + width / 2,
    //   mainCamera.transform.position.y + height / 2,
    //   mainCamera.transform.position.z
    // );
  }

  // Update is called once per frame
  void Update()
  {

  }
}
