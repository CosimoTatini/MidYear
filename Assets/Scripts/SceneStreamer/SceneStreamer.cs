using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Questa classe è responsabile di gestire il caricamento asincrono delle scene.
/// Data una lista di SceneData e parametri di distanza, 
/// carica e scarica le scene in base alla posizione del giocatore.
/// </summary>
public class SceneStreamer : MonoBehaviour
{
    [Header("References")]
    [SerializeField] SceneConfig sceneConfig;
    [SerializeField] Transform playerTransform;

    [Header("Load Settings")]
    [SerializeField] float loadDistance = 100f;
    [SerializeField] float unloadDistance = 120f;
    [SerializeField] float checkInterval = 1f;

    private async void Start()
    {
        // De-carica tutte le scene all'avvio
        foreach (SceneData sceneData in sceneConfig.Scenes)
        {
            if (SceneManager.GetSceneByName(sceneData.SceneName).isLoaded)
            {
                await SceneManager.UnloadSceneAsync(sceneData.SceneName);
            }
        }

        StartCoroutine(CheckAndHandleScenes());
    }

    private IEnumerator CheckAndHandleScenes()
    {
        while (true)
        {
            foreach (SceneData sceneData in sceneConfig.Scenes)
            {
                // controlliamo se il player è all'interno della bounding box della scena
                if (sceneData.SceneBounds.Contains(playerTransform.position))
                {
                    // assicuriamoci che la scena in cui sta il player sia caricata
                    if (!SceneManager.GetSceneByName(sceneData.SceneName).isLoaded)
                    {
                        // carica la scena
                        yield return SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
                    }
                }
                else
                {
                    // calcoliamo il punto più vicino al bound della scena
                    Vector3 closestPoint = sceneData.SceneBounds.ClosestPoint(playerTransform.position);
                    
                    // calcoliamo la distanza tra il punto più vicino e il player
                    float distance = Vector3.Distance(closestPoint, playerTransform.position);

                    // se la distanza è minore della distanza di caricamento
                    // e la scena non è ancora caricata,
                    // allora, Carica la scena
                    if (distance <= loadDistance 
                        && !SceneManager.GetSceneByName(sceneData.SceneName).isLoaded)
                    {
                        // carica la scena
                        yield return SceneManager.LoadSceneAsync(sceneData.SceneName, LoadSceneMode.Additive);
                    }

                    // se la distanza è maggiore della distanza di caricamento
                    // e la scena è caricata,
                    // allora, De-carica la scena
                    if (distance >= unloadDistance 
                        && SceneManager.GetSceneByName(sceneData.SceneName).isLoaded)
                    {
                        yield return SceneManager.UnloadSceneAsync(sceneData.SceneName);
                    }
                }
            }

            yield return new WaitForSeconds(checkInterval);
        }
    }
}