using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LogicTetramino : MonoBehaviour
{

    #region variables
    private float previousTime;
    public float timeDown = 0.8f;
    public static int high = 20;
    public static int width = 10;
    public Vector3 rotationPoint;
    private static Transform[,] grid = new Transform[width, high];
    public static int score;
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
                checkLines();
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
     * Metodo que nos ayuda con el transform para agregar que en la posición "tal" ya hay un objecto.
     * Nos ayuda a estar atentos que ya hay un objecto en una posición.
     * */
    void addGrid()
    {
        foreach (Transform transformChild in transform)
        {
            int wholeX = Mathf.RoundToInt(transformChild.transform.position.x);
            int wholeY = Mathf.RoundToInt(transformChild.transform.position.y);

            grid[wholeX, wholeY] = transformChild;

            if (wholeY >= 19)
            {
                score = 0;
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    /**
     * Verificar si hay lineas completas, de ser así, se eliminan.
     * */
    void checkLines()
    {
        for (int idx = high - 1; idx >= 0; idx--)
        {
            if (hasLine(idx))
            {
                deleteLine(idx);
                goDownLine(idx);
            }
        }
    }

    /**
     * Verificar si ya hay una linea completa en el juego.
     * */
    bool hasLine(int param)
    {
        for (int idx = 0; idx < width; idx++)
        {
            if (grid[idx,param] == null)
            {
                return false;
            }
        }
        score += 100;
        FindObjectOfType<GenerateScore>().showScore(score);
        return true;
    }

    /**
     * Destruye los objectos
     * */
    void deleteLine(int param)
    {
        for (int idx = 0; idx < width; idx++)
        {
            Destroy(grid[idx, param].gameObject);
            grid[idx, param] = null;
        }
    }

    /**
     * Si se destruye algo, baja lo que hay y lo que bajo lo deja nulo.
     * */
    void goDownLine(int param)
    {
        for (int idx = param; idx < high; idx++)
        {
            for (int idxj = 0; idxj < width; idxj++)
            {
                if (grid[idxj, idx] != null)
                {
                    grid[idxj, idx - 1] = grid[idxj, idx];
                    grid[idxj, idx] = null;
                    grid[idxj, idx - 1].transform.position -= new Vector3(0, 1, 0);
                }
            }
        }
    }
}
