using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class FlamethrowerController : MonoBehaviour
{
    [SerializeField]
    GameObject flame;

    [SerializeField]
    float heat = 10f;
    float cooldown = 3f;

    bool isThrowingFlame, isOverheated;

    void Start()
    {
        isThrowingFlame = false;
        isOverheated = false;
        flame.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)&& !isOverheated)
        {
            isThrowingFlame = true;
            flame.SetActive(true);
        }

        if (Input.GetMouseButtonUp(0) || heat<=0)
        {
            isThrowingFlame = false;
            flame.SetActive(false);
        }

        if (heat <= 0)
        {
            StartCoroutine(StartCooling());
        }

        HandleHeat();
       
    }

    void HandleHeat()
    {
        if (isThrowingFlame && heat>0)
        {
            heat -= 2*Time.deltaTime;
        }
        else if (!isThrowingFlame && heat < 10)
        {
            heat += 2*Time.deltaTime;
        }
    }

    IEnumerator StartCooling()
    {
        Debug.Log("Cooling started");
        isOverheated = true;
        yield return new WaitForSeconds(cooldown);
        isOverheated = false;
        Debug.Log("Cooling finished");
    }
}
