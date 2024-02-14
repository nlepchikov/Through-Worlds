using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    [SerializeField] private Text strawberriesText;
    [SerializeField] private AudioSource itemSound;

    private int collectibles;
    private int strawberries = 0;

    private void Start()
    {
        collectibles = GameObject.FindGameObjectsWithTag("Strawberry").Length;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Strawberry"))
        {
            itemSound.Play();
            Destroy(collision.gameObject);
            strawberries++;
            strawberriesText.text = ":" + strawberries + " / " + collectibles;
        }
    }
}
    

