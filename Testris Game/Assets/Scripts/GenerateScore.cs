using UnityEngine;
using UnityEngine.UI;

public class GenerateScore : MonoBehaviour
{

    #region Variables
    public Text txtScore;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        //txtScore = gameObject.GetComponent<Text>();
        txtScore.text = "0";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showScore(int score)
    {
        txtScore.text = score.ToString();
    }
}
