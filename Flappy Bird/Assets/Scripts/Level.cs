using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_ORTHO_SIZE = 50f;

    private const float PIPE_WIDTH = 8f;
    private const float PIPE_HEAD_HEIGHT = 3.75f;

    private void Start()
    {
        CreatePipe(50f, 0f);
        CreatePipe(40f, 20f);
        CreatePipe(30f, 40f);
        CreatePipe(20f, 60f);
    }

    private void CreatePipe(float height, float xPosition)
    {
        // Set up Pipe head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        pipeHead.position = new Vector3(xPosition, -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * .5f);

        // Set up Pipe body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        pipeBody.position = new Vector3(xPosition, -CAMERA_ORTHO_SIZE);

        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_WIDTH, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * .5f);

    }
}
