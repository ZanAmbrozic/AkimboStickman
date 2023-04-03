using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnTypeManager : MonoBehaviour
{
    public void AssignConnType(string connType)
    {
        DataManager.instance.connType = connType;
    }
}
