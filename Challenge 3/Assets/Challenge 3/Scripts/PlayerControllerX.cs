/*
David Huerta
Challenge 3
Controls balloon movement and collisions.
*/
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    [Header("State")]
    public bool gameOver = false;

    [Header("Float Settings")]
    public float floatForce = 8f;
    public float groundBounceImpulse = 6f;
    public float topLimit = 16f;
    private float gravityModifier = 1.5f;

    [Header("FX")]
    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    [Header("Audio")]
    private AudioSource playerAudio;
    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip groundBounceClip;

    private Rigidbody playerRb;

    void Start()
    {
        if (Physics.gravity.y > -10)
        {
            Physics.gravity *= gravityModifier;
        }

        playerRb = GetComponent<Rigidbody>();
        if (!playerRb) Debug.LogError("PlayerControllerX: Missing Rigidbody!");

        playerAudio = GetComponent<AudioSource>();

        // small initial lift
        if (playerRb) playerRb.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    void Update()
    {
        if (gameOver || playerRb == null) return;

        bool isLowEnough = transform.position.y < topLimit;

        if (Input.GetKey(KeyCode.Space) && isLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (gameOver) return;

        // Bomb → explode + end game + UI
        if (other.gameObject.CompareTag("Bomb"))
        {
            if (explosionParticle) explosionParticle.Play();
            if (playerAudio && explodeSound) playerAudio.PlayOneShot(explodeSound, 1f);

            gameOver = true;
            if (GameManagerX.Instance) GameManagerX.Instance.GameOver();

            Destroy(other.gameObject);
        }
        // Money → fireworks + sfx + score++
        else if (other.gameObject.CompareTag("Money"))
        {
            if (fireworksParticle)
            {
                fireworksParticle.transform.position = transform.position;
                fireworksParticle.Play();
            }
            if (playerAudio && moneySound) playerAudio.PlayOneShot(moneySound, 1f);

            if (GameManagerX.Instance) GameManagerX.Instance.AddScore(1);

            Destroy(other.gameObject);
        }
        // Ground → bounce + thud
        else if (other.gameObject.CompareTag("Ground"))
        {
            if (playerRb) playerRb.AddForce(Vector3.up * groundBounceImpulse, ForceMode.Impulse);
            if (playerAudio && groundBounceClip) playerAudio.PlayOneShot(groundBounceClip, 1f);
        }
    }
}
