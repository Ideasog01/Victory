using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static Vector2 movementInput;
    public static Vector2 mousePosition;

    [SerializeField]
    private float movementSpeed = 2f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private Transform farmPrefab;

    private Transform _playerTarget;
    private Camera _gameCamera;
    private GridManager _gridManager;

    private void Awake()
    {
        _playerTarget = GameObject.Find("PlayerTarget").transform;
        _gameCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        _gridManager = this.GetComponent<GridManager>();
    }

    private void Update()
    {
        ApplyMovement();
    }

    public void PlaceObject()
    {
        //If object to place is selected

        RaycastHit hit;

        if(Physics.Raycast(_gameCamera.ScreenPointToRay(mousePosition), out hit))
        {
            Node node = _gridManager.FindNode(hit.point);

            if(node != null)
            {
                if(!node.NodeClosed)
                {
                    Instantiate(farmPrefab, node.NodePosition, Quaternion.identity);
                    Debug.Log("PlaceObject() Called");
                    node.NodeClosed = true;
                }
            }        
        }
    }

    private void ApplyMovement()
    {
        _playerTarget.Translate(new Vector3(movementInput.y, 0, -movementInput.x) * Time.deltaTime * movementSpeed);
    }
}
