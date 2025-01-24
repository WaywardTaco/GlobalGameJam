using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopIn : MonoBehaviour
{
    void OnEnable() {
        GetComponent<Animator>().Play("Press");
    }
}
