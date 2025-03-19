using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatWalls : MonoBehaviour
{
    public Vector3 startPos; // Sijainti, johon "käytävä" palautetaan
    private float repeat;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position; //aloitus ja nykyinen sijainti.
        repeat = GetComponent<BoxCollider>().size.x / 2; // lasketaan boxcolliderin puolikas
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < startPos.x - repeat) // katsotaan onko käytävä liian kaukana aloituspaikasta.
        {
            transform.position = startPos; // palautetaan se aloitusijaintin.
        }
    }
}
