using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCharacter_ChangeColor : MonoBehaviour
{

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GetComponent<SpriteRenderer>().color = new Color(0.223f, 0.752f, 0.458f, 0.62f);
            Invoke("ReturnColor", 0.3f);
        }

    }

    void ReturnColor()
    {
        GetComponent<SpriteRenderer>().color = new Color(1.0f, 1.0f, 1.0f);
    }
}
