using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamthrowerAim : MonoBehaviour
{
    void Update()
    {
        if (Time.timeScale == 0)
        {
            return;
        }
        Vector3 mousePosition = Input.mousePosition;
        mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);

        Vector2 direction = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);

        transform.up = direction;
    }
}
