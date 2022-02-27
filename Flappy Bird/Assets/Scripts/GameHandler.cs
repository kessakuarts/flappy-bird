using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;
    }

    private void Start()
    {
        Debug.Log("GameHandler.Start");

        //GameObject gameObject = new GameObject("Pipe", typeof(SpriteRenderer));
        //gameObject.GetComponent<SpriteRenderer>().sprite = GameAssets.GetInstance().pipeHeadSprite;
    }

    void Update()
    {
        
    }
}
