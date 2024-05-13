using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSystem : MonoBehaviour
{
    void Start()
    {

    }

    void Update()
    {

    }
    public void SetState(OtheloSystem.SpriteState _spriteState)
    {
        if (_spriteState == OtheloSystem.SpriteState.White)
        {
            var rotation = Quaternion.Euler(270, 0, 0);
            gameObject.transform.rotation = rotation;
        }
        else if (_spriteState == OtheloSystem.SpriteState.Black)
        {
            var rotation = Quaternion.Euler(90, 0, 0);
            gameObject.transform.rotation = rotation;
        }
        else gameObject.SetActive(false);
    }
}
