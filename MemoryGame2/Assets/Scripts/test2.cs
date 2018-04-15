using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class test2 : MonoBehaviour {
    public const int gridRows = 4;//kolumny i wiersze
    public const int gridCols = 4;
    public const float offsetX = 2f;//odstępy pomiędzy kartammi
    public const float offsetY = 1.9f;

    [SerializeField]
    public MainCard originalCard;//karta do kopiowanie
    [SerializeField]
    public Sprite[] images;//obrazki do gry
    // Use this for initialization
    void Start () {
      
        Vector3 startPos = originalCard.transform.position;//pozycja orginalnej kart, na jej podstawie generowana pozycja reszy kart
        int[] numbers = { 0, 0, 1, 1, 2, 2, 3, 3, 4, 4,5,5,6,6,7,7 };//id kart
        numbers = ShuffleArray(numbers);//ustawia w losowej kolejności id

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }
                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, images[id]);//ustawienie id i obrazka

                float posX = (offsetX * i) + startPos.x;//ustawinie pozycji kart
                float posY = (offsetY * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);

            }
        }
    }
    private int[] ShuffleArray(int[] numbers)
    {
        int[] newArray = numbers.Clone() as int[];
        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = UnityEngine.Random.Range(i, newArray.Length);
            newArray[i] = newArray[r];
            newArray[r] = tmp;
        }
        return newArray;
    }

    private MainCard _firstRevealed;
    private MainCard _secondRevealed;

    private int _score = 0;
    [SerializeField]
    private TextMesh scoreLabel;

    public bool canReveal//czy można odwrócić 2 karte
    {
        get { return _secondRevealed == null; }
    }


    public void CardRevealed(MainCard card)
    {
        if(_firstRevealed==null)
        {
            _firstRevealed = card;
        }
        else
        {
            _secondRevealed = card;
            StartCoroutine(CheckMatch());
        }
    }
    public int count = 0;

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id==_secondRevealed.id)
        {
            _score += 10;
            scoreLabel.text = "Score: " + _score;//ustawienie wyniku
            count++;
            if (count == 8)
            {
                scoreLabel.text = "Score: " + _score+"END GAME!";
            }
            yield return null;
        }
        else
        {
            _score -= 2;
            scoreLabel.text = "Score: " + _score;//ew min 0 brak ujemnych pkt?
            yield return new WaitForSeconds(1.0f);//jęli wybrane 2 karty sa różne zaczekaj 1s
            _firstRevealed.Unreveal();//przysłąniecie kart z powrotem
            _secondRevealed.Unreveal();
        }
        _firstRevealed = null;
        _secondRevealed = null;
    }
    //załadowanie ponownie strony
    public void Restart()
    {
        //count = 0;
        SceneManager.LoadScene("Scene_001");
        
    }
}
