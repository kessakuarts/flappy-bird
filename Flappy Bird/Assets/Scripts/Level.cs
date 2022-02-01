using UnityEngine;

public class Level : MonoBehaviour
{
    private void Start()
    {
        CreatePipe(0f, 0f);
        CreatePipe(0f, 20f);
    }

    private void CreatePipe(float height, float xPosition)
    {
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        pipeHead.position = new Vector3(xPosition, 0f);

        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        pipeBody.position = new Vector3(xPosition, 0f);
    }
}
