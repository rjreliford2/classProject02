using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstatiateSpaceShip : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpaceShipPrefab;
    public GameObject target;
    private GameObject ship;
    public float rotationalspeed;
    void Start()
    {
        ship = Instantiate(SpaceShipPrefab, new Vector3(Random.Range(-10.0f, 10.0f), Random.Range(1.0f, 6.0f), Random.Range(-10.0f, 10.0f)), SpaceShipPrefab.transform.rotation);
        rotationalspeed = Random.Range(10.0f, 40.0f);
    }

    void Update()
    {
        ship.transform.RotateAround(target.transform.position, Vector3.up, rotationalspeed * Time.deltaTime);
    }
}
