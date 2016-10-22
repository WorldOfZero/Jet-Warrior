using UnityEngine;
using System.Collections;

public class ObstacleModel : MonoBehaviour {

    public float baseHealth = 100;
    public float health = 100;
    public float speed = 5;
    public float damage = 10;

    public void Start()
    {
        health = baseHealth;
    }
}
