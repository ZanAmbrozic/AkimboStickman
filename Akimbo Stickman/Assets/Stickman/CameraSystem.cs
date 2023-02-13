using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    private GameObject _player;
    public float xMin;
    public float xMax;
    public float yMin;
    public float yMax;
    public bool enableCameraMovement = true;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        enabled = enableCameraMovement;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_player == null)
            _player = GameObject.FindGameObjectWithTag("Player");

        float x = Mathf.Clamp(_player.transform.position.x, xMin, xMax);
        float y = Mathf.Clamp(_player.transform.position.y, yMin, yMax);
        gameObject.transform.position = new Vector3(x, y, gameObject.transform.position.z);
    }
}
