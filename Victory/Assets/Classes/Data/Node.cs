using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    private Vector3 _nodePosition;

    private bool _nodeClosed;

    public Vector3 NodePosition
    {
        get { return _nodePosition; }
        set { _nodePosition = value; }
    }

    public bool NodeClosed
    {
        get { return _nodeClosed; }
        set { _nodeClosed = value; }
    }
}
