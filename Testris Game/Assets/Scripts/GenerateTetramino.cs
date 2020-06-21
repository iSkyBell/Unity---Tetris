using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTetramino : MonoBehaviour
{

    #region variables
    public GameObject[] tetramino;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        newTetramino();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /**
     * Metodo que generara nuevas piezas del tetris.
     * */
    public void newTetramino()
    {
        Instantiate(tetramino[Random.Range(0,tetramino.Length)], transform.position, Quaternion.identity);
    }
}
