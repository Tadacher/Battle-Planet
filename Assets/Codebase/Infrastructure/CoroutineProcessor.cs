using System.Collections;
using UnityEngine;

public class CoroutineProcessor : MonoBehaviour
{
    public void StartCoroutineProcess(IEnumerator coroutine) => StartCoroutine(coroutine);
}