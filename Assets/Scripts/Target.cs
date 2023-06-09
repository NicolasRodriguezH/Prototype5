using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;

    private float minSpeed = 12;
    private float maxSpeed = 16;
    private float maxtorque = 10;
    private float xRange = 4;
    private float ySpawnPos = -2;

    public AudioSource targetAudio;
    public AudioClip soundEffect;

    public int pointValue;
    public ParticleSystem explotionParticle;

    // Start is called before the first frame update
    void Start()
    {
        targetAudio = GameObject.Find("Main Camera").GetComponent<AudioSource>();
        targetRb = GetComponent<Rigidbody>();
        //targetAudio = GetComponent<AudioSource>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomSpawnPos();

        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if(gameManager.isGameActive)
        {
            // Se agrega el sonido antes de destruir el objeto, si no, no funcionara
            targetAudio.PlayOneShot(soundEffect, 1.0f);
            Destroy(gameObject);
            Instantiate(explotionParticle, transform.position, explotionParticle.transform.rotation);
    
            // Parte necesaria para actualizar score desde game manager, Player Controller es el que recibe eta linea.
            // Dentro del OnCollitionEnter
            gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if(!gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            //gameManager.GameOver();
            // Con el llamado al metodo UpdateLives de la clase Game Manager le pasamos -1 para restarle una vida, cada que haga click en una caja "Bad"
            gameManager.UpdateLives(-1);
        }
    }

    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            // Se agrega el sonido antes de destruir el objeto, si no, no funcionara
            targetAudio.PlayOneShot(soundEffect, 1.0f);
            Destroy(gameObject);
            Instantiate(explotionParticle, transform.position, explotionParticle.transform.rotation);

            // Parte necesaria para actualizar score desde game manager, Player Controller es el que recibe eta linea.
            // Dentro del OnCollitionEnter
            gameManager.UpdateScore(pointValue);
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-maxtorque, maxtorque);
    }

    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos, 0);
    }
}
