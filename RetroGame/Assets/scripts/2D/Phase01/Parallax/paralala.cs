using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralala : MonoBehaviour{
    private Transform cameraPlayer;
    private float tamanho, posicaoInicial;
    public float velocidadeParalax;
    
    void Start()
    {
        
        posicaoInicial = transform.position.x;
        tamanho = GetComponent<SpriteRenderer>().bounds.size.x;
        cameraPlayer = Camera.main.transform;
    }

    
    void Update()
    {
        float temp = (cameraPlayer.transform.position.x * (1 - velocidadeParalax));
        float distancia = (cameraPlayer.transform.position.x * velocidadeParalax);

        Vector3 PositionLerped = Vector3.Lerp(
            transform.position,
            new Vector3(cameraPlayer.transform.position.x, transform.position.y, transform.position.z),
            0.005f
        );

        transform.position = PositionLerped;


    }
}