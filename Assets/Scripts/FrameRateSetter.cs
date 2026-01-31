using UnityEngine;

public class FrameRateSetter : MonoBehaviour
{
    [SerializeField] int targetFrameRate = 90;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.targetFrameRate = targetFrameRate;
    }
}
