using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SceneConfig", menuName ="Scene Streamer/Scene Config")]
public class SceneConfig : ScriptableObject
{
    public List<SceneData> Scenes;
}
