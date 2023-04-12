using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpRotation : MonoBehaviour
{
    private void Start()
    {
        transform.rotation = Camera.main.transform.rotation;
    }
}
