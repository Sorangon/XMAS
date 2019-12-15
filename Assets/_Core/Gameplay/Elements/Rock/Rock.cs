using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Rock : MonoBehaviour
{
    public UnityEvent OnRockSlashed;

    private void OnCollisionEnter2D(Collision2D other)
    {
        OnRockSlashed.Invoke();
    }
}
