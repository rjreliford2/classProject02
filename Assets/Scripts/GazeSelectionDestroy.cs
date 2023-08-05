using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GazeSelectionDestroy : MonoBehaviour
{
    public float gazeTime;
    public GameObject explosionPrefab;
    public float explosionLife;
    private GameObject explosion;
    public GameObject particlePrefab;
    private GameObject particle;
    public int Score;
    public GameObject[] asteroids;
    public GameObject SpaceShipPrefab;
    public GameObject target;
    private GameObject ship;
    public float rotationalspeed;
    // Start is called before the first frame update
    //start sets up the first ship instance as well as the first values for gazetime and score
    void Start()
    {
        ship = Instantiate(SpaceShipPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(1.0f, 6.0f), Random.Range(-10.0f, 10.0f)), SpaceShipPrefab.transform.rotation);
        rotationalspeed = Random.Range(10.0f, 40.0f);
        gazeTime = 0;
        Score = 0;
    }

    // Update is called once per frame
    //rotates the ship around on each frame
    //explosion life keeps track of the previous most explosion and deletes it once it's
    //lifespan hits 2 sec
    void Update()
    {
        if (explosion != null)
        {
            explosionLife += Time.deltaTime;
            if(explosionLife >= 2)
            {
                Destroy(explosion);
            }
        }
        ship.transform.RotateAround(target.transform.position, Vector3.up, rotationalspeed * Time.deltaTime);
    }
    //sets the line and ship color once they come into conntact
    void OnCollisionEnter(Collision collision)
    {
        Renderer ship = collision.gameObject.GetComponent<Renderer>();
        Renderer rend = GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.yellow);
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.yellow);
        ship.material.shader = Shader.Find("_Color");
        ship.material.SetColor("_Color", Color.yellow);
        ship.material.shader = Shader.Find("Specular");
        ship.material.SetColor("_SpecColor", Color.yellow);
    }
    //begins to increase the continous gaze counter
    //deleting the object at 3 seconds
    //spawns the particle effect when colliding and the explosion once deleted
    void OnCollisionStay(Collision collision)
    {
        particle = Instantiate(particlePrefab, collision.gameObject.transform.position, Quaternion.identity);
        gazeTime += Time.deltaTime;
        if(gazeTime >= 3)
        {
            explosion = Instantiate(explosionPrefab, collision.gameObject.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            ScoreIncrease(Score);
            Score += 1;
            gazeTime = 0;
        }
        Destroy(particle);
    }
    //resets the line color and ship color
    void OnCollisionExit(Collision collisionInfo)
    {
        gazeTime = 0;
        Renderer rend = GetComponent<Renderer>();
        Renderer ship = collisionInfo.gameObject.GetComponent<Renderer>();
        rend.material.shader = Shader.Find("_Color");
        rend.material.SetColor("_Color", Color.blue);
        rend.material.shader = Shader.Find("Specular");
        rend.material.SetColor("_SpecColor", Color.blue);
        ship.material.shader = Shader.Find("_Color");
        ship.material.SetColor("_Color", Color.gray);
        ship.material.shader = Shader.Find("Specular");
        ship.material.SetColor("_SpecColor", Color.gray);
    }
    //spawns the asteroids that keep track of score
    //spawns the next ship
    //and if score is or above 10 disables the collider on the line making interactions imposiible
    void ScoreIncrease(int Score)
    {
        SpaceShipSpawn(SpaceShipPrefab);
        int randomPick = Random.Range(0, 5);
        if (Score < 10)
        {
            Instantiate(asteroids[randomPick], new Vector3(-5 + Score, 0, 2), asteroids[randomPick].transform.rotation);
        }
        else if(Score >= 10)
        {
            Collider lineCollider = GetComponent<Collider>();
            lineCollider.enabled = !lineCollider.enabled;
        }
        
    }
    //spawns the next instance of the spaceship
    void SpaceShipSpawn(GameObject SpaceShipPrefab)
    {
        rotationalspeed = Random.Range(10.0f, 40.0f);
        ship = Instantiate(SpaceShipPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(1.0f, 6.0f), Random.Range(-10.0f, 10.0f)), SpaceShipPrefab.transform.rotation);
    }
}
