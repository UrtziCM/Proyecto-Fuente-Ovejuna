using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrearBotonesScript : MonoBehaviour
{

    [SerializeField] private GameObject botonPrefab;

    // Start is called before the first frame update
    void Start()
    {
        Vector3 posicion = botonPrefab.transform.position;
        float posicionY = posicion.y;
        for(int i =0;i<7;i++){
            posicionY -= (i+60);
            var botonCreado = Instantiate(botonPrefab,new Vector2(posicion.x,posicionY),Quaternion.identity); 
            botonCreado.transform.parent = this.transform.GetChild(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
