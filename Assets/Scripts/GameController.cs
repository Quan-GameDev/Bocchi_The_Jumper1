using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject camera;
    public GameObject player;

    private PlayerMovement playerMovement;
    private Vector3 cameraPosition;
    private Vector3 playerPosition;
    // Start is called before the first frame update
    void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        cameraPosition = camera.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        camera.transform.position = new Vector3(playerPosition.x, playerPosition.y, -10);
    }
}
