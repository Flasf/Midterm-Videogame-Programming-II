using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogicaCalidad : MonoBehaviour
{
    public Dropdown dropdown;
    public int calidad;
    // Start is called before the first frame update
    void Start()
    {

        calidad = PlayerPrefs.GetInt("numerodeCalidad", 3);
        dropdown.value = calidad;
        AjustarCalidad();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AjustarCalidad()
    {
        QualitySettings.SetQualityLevel(dropdown.value);
        PlayerPrefs.SetInt("numeroDeCalidad", dropdown.value);
        calidad = dropdown.value;
    }
}
