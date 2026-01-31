using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

#if UNITY_EDITOR
public class DEBUG_ObjectsOpener : MonoBehaviour
{
    [SerializeField] GameObject testerCoroutine;
    [SerializeField] GameObject testerAsync;

    private async void OnEnable()
    {
        StartCoroutine(ActiveObjectCoroutine());
        await ActiveObjectAsync();
    }

    private async Task ActiveObjectAsync()
    {
        await Task.Run(() => testerAsync.SetActive(true));
    }

    private IEnumerator ActiveObjectCoroutine()
    {
        yield return null;
        testerCoroutine.SetActive(true);
    }
}
#endif