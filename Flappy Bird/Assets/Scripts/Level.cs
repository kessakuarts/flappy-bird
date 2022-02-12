using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    private const float CAMERA_ORTHO_SIZE = 50f;

    private const float PIPE_WIDTH = 8f;
    private const float PIPE_HEAD_HEIGHT = 4f;
    private const float PIPE_MOVE_SPEED = 30f;

    private const float PIPE_DESTROY_X_POSITION = -100f;
    private const float PIPE_SPAWN_X_POSITION = 100f;

    private List<Pipe> pipeList;

    private float pipeSpawnTimer;
    private float pipeSpawnTimerMax;

    private float gapSize;

    private int pipeSpawned;

    public enum Difficulty
    {
        Easy,
        Medium,
        Hard,
        Impossible,
    }

    private void Awake()
    {
        pipeList = new List<Pipe>();

        pipeSpawnTimerMax = 1f;
        SetDifficulty(Difficulty.Easy);
    }

    private void Start()
    {
        //CreatePipe(20f, 50f, true);
        //CreatePipe(20f, 50f, false);

        //CreateGapPipes(50f, 20f, 20f);
    }

    private void Update()
    {
        HandlePipeMovement();
        HandlePipeSpawning();
    }

    private void HandlePipeSpawning()
    {
        pipeSpawnTimer -= Time.deltaTime;
        
        if (pipeSpawnTimer < 0)
        {
            pipeSpawnTimer += pipeSpawnTimerMax;

            float heightEdgeLimit = 10f;
            float totalHeight = CAMERA_ORTHO_SIZE * 2f;

            float minHeight = gapSize * .5f + heightEdgeLimit;
            float maxHeight = totalHeight - gapSize * .5f - heightEdgeLimit;

            float height = Random.Range(minHeight, maxHeight);

            CreateGapPipes(height, gapSize, PIPE_SPAWN_X_POSITION);
        }
    }

    private void HandlePipeMovement()
    {
        for (int i = 0; i < pipeList.Count; i++)
        {
            Pipe pipe = pipeList[i];
            pipe.Move();

            if (pipe.GetXPosition() < PIPE_DESTROY_X_POSITION)
            {
                // Destroy pipe
                pipe.DestroySelf();
                pipeList.Remove(pipe);

                i--;
            }
        }
    }

    private void SetDifficulty(Difficulty difficulty)
    {
        switch (difficulty)
        {
            case Difficulty.Easy:
                gapSize = 60f;
                pipeSpawnTimerMax = 1.6f;
                break;
            case Difficulty.Medium:
                pipeSpawnTimerMax = 1.25f;
                gapSize = 55f;
                break;
            case Difficulty.Hard:
                gapSize = 43f;
                pipeSpawnTimerMax = 1.1f;
                break;
            case Difficulty.Impossible:
                gapSize = 35f;
                pipeSpawnTimerMax = 1.001f;
                break;
        }
    }

    private Difficulty GetDifficulty()
    {
        if (pipeSpawned >= 30) return Difficulty.Impossible;
        if (pipeSpawned >= 20) return Difficulty.Hard;
        if (pipeSpawned >= 10) return Difficulty.Medium;

        return Difficulty.Easy;
    }

    private void CreateGapPipes(float gapY, float gapSize, float xPosition)
    {
        CreatePipe(xPosition, gapY - gapSize * .5f, true);
        CreatePipe(xPosition, CAMERA_ORTHO_SIZE * 2f - gapY - gapSize * .5f, false);

        pipeSpawned++;
        SetDifficulty(GetDifficulty());

        Debug.Log("SPAWNED: " + pipeSpawned + "; Current difficulty: " + GetDifficulty());
    }

    private void CreatePipe(float xPosition, float height, bool createBottom)
    {
        // Set up Pipe head
        Transform pipeHead = Instantiate(GameAssets.GetInstance().pfPipeHead);
        float pipeHeadYPosition;
        if (createBottom)
        {
            pipeHeadYPosition = -CAMERA_ORTHO_SIZE + height - PIPE_HEAD_HEIGHT * .5f;
        } else
        {
            pipeHeadYPosition = +CAMERA_ORTHO_SIZE - height + PIPE_HEAD_HEIGHT * .5f;
        }
        pipeHead.position = new Vector3(xPosition, pipeHeadYPosition);

        // Set up Pipe body
        Transform pipeBody = Instantiate(GameAssets.GetInstance().pfPipeBody);
        float pipeBodyYPosition;
        if (createBottom)
        {
            pipeBodyYPosition = -CAMERA_ORTHO_SIZE;
        } else
        {
            pipeBodyYPosition = +CAMERA_ORTHO_SIZE;
            pipeBody.localScale = new Vector3(1, -1, 1);
        }
        pipeBody.position = new Vector3(xPosition, pipeBodyYPosition);

        SpriteRenderer pipeBodySpriteRenderer = pipeBody.GetComponent<SpriteRenderer>();
        pipeBodySpriteRenderer.size = new Vector2(PIPE_WIDTH, height);

        BoxCollider2D pipeBodyBoxCollider = pipeBody.GetComponent<BoxCollider2D>();
        pipeBodyBoxCollider.size = new Vector2(PIPE_WIDTH, height);
        pipeBodyBoxCollider.offset = new Vector2(0f, height * .5f);

        Pipe pipe = new Pipe(pipeHead, pipeBody);
        pipeList.Add(pipe);
    }

    private class Pipe
    {
        private Transform pipeHeadTransform;
        private Transform pipeBodyTransform;

        public Pipe(Transform pipeHeadTransform, Transform pipeBodyTransform)
        {
            this.pipeHeadTransform = pipeHeadTransform;
            this.pipeBodyTransform = pipeBodyTransform;
        }

        public void Move()
        {
            pipeHeadTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
            pipeBodyTransform.position += new Vector3(-1, 0, 0) * PIPE_MOVE_SPEED * Time.deltaTime;
        }

        public float GetXPosition()
        {
            return pipeHeadTransform.position.x;
        }

        public void DestroySelf()
        {
            Destroy(pipeHeadTransform.gameObject);
            Destroy(pipeBodyTransform.gameObject);
        }
    }
}
