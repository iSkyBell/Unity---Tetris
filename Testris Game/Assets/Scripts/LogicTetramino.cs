using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LogicTetramino : MonoBehaviour
{

    #region variables
    private float previousTime;
    public float timeDown = 0.8f;
    public static int high = 20;
    public static int width = 10;
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[width, high];
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Move Left object
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.position += new Vector3(-1, 0, 0);
            if (!limits())
            {
                transform.position -= new Vector3(-1, 0, 0);
            }
        }

        // Move Rigth object
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.position += new Vector3(1, 0, 0);
            if (!limits())
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }

        // Fall object automatic, if press the button down, the object fall quickly.
        if (Time.time - previousTime > (Input.GetKey(KeyCode.DownArrow) ? timeDown / 20 : timeDown))
        {
            transform.position += new Vector3(0, -1, 0);
            if (!limits())
            {
                transform.position += new Vector3(0, 1, 0);
                addGrid();
                this.enabled = false;
                FindObjectOfType<GenerateTetramino>().newTetramino();
            }

            previousTime = Time.time;
        }

        // When press key up, the object rotate -90 gradles. 
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), -90);
            if (!limits())
            {
                transform.RotateAround(transform.TransformPoint(rotationPoint), new Vector3(0, 0, 1), 90);
            }
        }

    }

    /**
     * Metodo que sirve para que el objecto no pase del limite del fondo.
     * Si el objecto llega al limite del fondo, o sea, por abajo no lo deja avanzar mas y detiene el objecto.
     * @return Retorna un booleano en caso de que supere el limite es false y no lo deja.
     * */
    bool limits()
    {
        foreach (Transform transformChild in transform)
        {
            int wholeX = Mathf.RoundToInt(transformChild.transform.position.x);
            int wholeY = Mathf.RoundToInt(transformChild.transform.position.y);
            if(wholeX < 0 || wholeX >= width || wholeY < 0 || wholeY >= high)
            {
                return false;
            }

            if(grid[wholeX,wholeY] != null)
            {
                return false;
            }

        }
        return true;
    }

    /**
     * Metodo que nos ayuda con el transform para agregar que en la posición tal ya hay un objecto.
     * */
    void addGrid()
    {
        foreach (Transform transformChild in transform)
        {
            int wholeX = Mathf.RoundToInt(transformChild.transform.position.x);
            int wholeY = Mathf.RoundToInt(transformChild.transform.position.y);

            grid[wholeX, wholeY] = transformChild;

        }
    }
}
