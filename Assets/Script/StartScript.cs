using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScript : MonoBehaviour
{
    private GameManager _GameManager;

    private void Awake()
    {
        _GameManager = GameManager.Instance;

        Destroy(this.gameObject);
    }
}
