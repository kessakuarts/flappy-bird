using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private void Awake()
    {
        //QualitySettings.vSyncCount = 0;
        //Application.targetFrameRate = Screen.currentResolution.refreshRate;
        //Application.targetFrameRate = -1;

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
