using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
    private bool isLock = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt))
        {
            if (isLock)
            {
                isLock = false;
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                isLock = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}
