﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void EscenaDemo()
    {
        SceneManager.LoadScene("NivelDemo");
    }
    
    public void EscenaJuego()
    {
        SceneManager.LoadScene("Nivel1");
    }

    public void Salir()
    {
        Application.Quit();
    }
}
