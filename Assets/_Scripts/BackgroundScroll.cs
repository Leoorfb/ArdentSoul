using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [SerializeField] float scrollSpeedX = 1f;
    [SerializeField] bool isFollowCameraYOn = false;
    [Range(0f, 1f)]
    [SerializeField] float followSpeedY = 1f;
    float textureOffsetY = 0;

    private MeshRenderer _meshRenderer;
    private Transform _camera;
    private Vector3 _cameraStartPosition;

    private float _startPositionY;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _camera = Camera.main.transform;
        _cameraStartPosition = _camera.position;
        textureOffsetY = _meshRenderer.material.mainTextureOffset.y;
        _startPositionY = transform.position.y;
    }

    private void LateUpdate()
    {
        float distanceFromStartX = _camera.position.x - _cameraStartPosition.x;
        float distanceFromStartY = _camera.position.y - _cameraStartPosition.y;

        Vector3 newPosition = new Vector3(_camera.position.x, transform.position.y, transform.position.z);

        if (isFollowCameraYOn)
            newPosition.y = _startPositionY + (distanceFromStartY * followSpeedY);

        transform.position = newPosition;

        _meshRenderer.material.mainTextureOffset = new Vector2(distanceFromStartX * scrollSpeedX /10, textureOffsetY);
    }
}
