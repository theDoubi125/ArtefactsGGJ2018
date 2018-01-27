using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMenuDisplay : MonoBehaviour
{
    public List<Transform> ImageList;

    public void AddPlayer(int controllerId)
    {
        Transform instance = Instantiate(ImageList[controllerId]);
        instance.SetParent(transform);
    }
}
