using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomController : MonoBehaviour
{
    [SerializeField]
    private GameObject[] walls; //0 == UP, 1 == DOWN, 2 == RIGHT, 3 == LEFT

    [SerializeField]
    private GameObject[] doorways;

    public void UpdateRoom(bool[] status)
    {
        for (int i = 0; i < status.Length; i++)
        {
            doorways[i].SetActive(status[i]);
            walls[i].SetActive(!status[i]);
        }
    }
}
