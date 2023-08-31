using System.Collections;
using UnityEngine;

public class CoroutineProcessorService : MonoBehaviour
{
    public void StartCoroutineProcess(IEnumerator coroutine) => StartCoroutine(coroutine);
}