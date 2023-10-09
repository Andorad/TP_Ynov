using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AsteroidController : MonoBehaviour
{
    [SerializeField]
    private float scale;
    [SerializeField]
    private float minSpeed;
    [SerializeField]
    private float maxSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private Transform visual;

    [SerializeField]
    private GameObject asteroidPiece;
    [SerializeField]
    private Vector3 offset;


    void Start()
    {
        scale = Random.Range(0.3f, 2f);
        transform.localScale += new Vector3(scale, scale, scale);
        Destroy(gameObject, 20f);
    }


    void Update()
    {
        visual.transform.Rotate(Vector3.back * rotationSpeed * Time.deltaTime);
        transform.Translate(Vector2.down * Random.Range(minSpeed, maxSpeed) * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            SceneManager.LoadScene("SampleScene");
        }
        else if (col.CompareTag("Asteroid"))
        {
            Instantiate(asteroidPiece, transform.position + offset, transform.rotation);
            Instantiate(asteroidPiece, transform.position + offset, transform.rotation);
        }

        if (col.CompareTag("Finish"))
        {
            UIManager.instance.nbOfAsteroids--;
            Destroy(gameObject);
        }
    }
}
