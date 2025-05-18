using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]         Transform       levelTransform;
    [SerializeField] private float           rotationTime = 1f;
    [SerializeField] private GameObject      playerBall;
    [SerializeField] private bool            allowVerticalRotation = true;
    [SerializeField] private bool            allowResetRotation = true;
    [SerializeField] private ParticleSpawner particleSpawner;
    Collider                                 playerBallCollider;
    Rigidbody                                playerBallRigidBody;
    Vector3                                  playerBallVelocity;


    Vector3              playerStartPosition;
    Quaternion           targetRotation;
    Quaternion           startRotation;
    private float        rotationNormalizedTimer = 1f;
    private event Action FinishedRotation;
    private event Action StartRotation;
    bool                 isRotating => rotationNormalizedTimer < 1;
    bool                 _levelCompleted = false;
    float                fallDelay       = 0.3f;
    float                fallDelayTimer  = 0f;

    private void Start()
    {
        playerStartPosition = playerBall.transform.position;
        targetRotation = levelTransform.rotation;
        playerBallCollider = playerBall.GetComponent<Collider>();
        playerBallRigidBody = playerBall.GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        StartRotation += HandleRotationStarted;
        FinishedRotation += HandleRotationFinished;
    }

    private void OnDisable()
    {
        StartRotation -= HandleRotationStarted;
        FinishedRotation -= HandleRotationFinished;
    }

    private void HandleRotationFinished()
    {
        playerBallCollider.enabled = true;
        playerBallRigidBody.useGravity = true;
    }

    private void HandleRotationStarted()
    {
        playerBallCollider.enabled = false;
        playerBallRigidBody.useGravity = false;
        // playerBallVelocity = playerBallRigidBody.linearVelocity;
        playerBallRigidBody.linearVelocity = Vector3.zero;
        rotationNormalizedTimer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_levelCompleted)
        {
            HandleInput();
        }
        

        if (rotationNormalizedTimer < 1)
        {
            rotationNormalizedTimer += Time.deltaTime / rotationTime;
            if (rotationNormalizedTimer >= 1)
            {
                FinishedRotation?.Invoke();
            }

            levelTransform.rotation = Quaternion.Slerp(startRotation, targetRotation, rotationNormalizedTimer);
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.W) && allowVerticalRotation && CanRotate())
        {
            startRotation = levelTransform.rotation;
            targetRotation = Quaternion.Euler(90, 0, 0) * targetRotation;
            rotationNormalizedTimer = 0;
            StartRotation?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.S) && allowVerticalRotation && CanRotate())
        {
            startRotation = levelTransform.rotation;
            targetRotation = Quaternion.Euler(-90, 0, 0) * targetRotation;

            StartRotation?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.A) && CanRotate())
        {
            startRotation = levelTransform.rotation;
            targetRotation = Quaternion.Euler(0, 0, 90) * targetRotation;
            rotationNormalizedTimer = 0;
            StartRotation?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.D) && CanRotate())
        {
            startRotation = levelTransform.rotation;
            targetRotation = Quaternion.Euler(0, 0, -90) * targetRotation;
            rotationNormalizedTimer = 0;
            StartRotation?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.Space) && CanRotate())
        {
            playerBall.transform.position = playerStartPosition;
        }
    }

    private void FixedUpdate()
    {
        if (isRotating)
        {
            playerBallRigidBody.linearVelocity = Vector3.zero;
            fallDelayTimer = 0;
        }
        else if (!isRotating && fallDelayTimer < fallDelay)
        {
            fallDelayTimer += Time.fixedDeltaTime;
            playerBallRigidBody.constraints = RigidbodyConstraints.FreezePosition;
        }
        else
        {
            if (playerBall.transform.position.z < -0.5f)
            {
                playerBallRigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
            }
            else
            {
                playerBallRigidBody.constraints = RigidbodyConstraints.FreezePosition;
            }
        }
    }

    public void LevelCompleted()
    {
        _levelCompleted = true;
        StartCoroutine(EndLevel());
    }

    private IEnumerator EndLevel()
    {
        yield return particleSpawner.Spawn();

        yield return new WaitForSeconds(1f);

        int currentIndex = SceneManager.GetActiveScene().buildIndex;
        if (currentIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentIndex + 1);
        }
        else
        {
            SceneManager.LoadScene(0);
        }
    }

    private bool CanRotate()
    {
        return rotationNormalizedTimer > 0.2f;
    }
}