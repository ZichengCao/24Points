using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAdministrator : MonoBehaviour
{

    private ExpressionCalculator helper = new ExpressionCalculator();

    // static resource
    public Sprite[] soundSprite;
    public Sprite[] palyingCardClubSprite;
    public Sprite[] palyingCardDiamondSprite;
    public Sprite[] palyingCardHeartSprite;
    public Sprite[] palyingCardSpardeSprite;
    public GameObject CARD;
    public List<Sprite[]> playingCardSpriteList = new List<Sprite[]>();

    // dynamic gameobject
    public Image soundImage;
    public GameObject cardContainer;
    private Image[] cards = new Image[4]; //selected card


    // ordinary
    private int[] selectNums = new int[4];

    void Start()
    {
        playingCardSpriteList.Add(palyingCardClubSprite);
        playingCardSpriteList.Add(palyingCardDiamondSprite);
        playingCardSpriteList.Add(palyingCardHeartSprite);
        playingCardSpriteList.Add(palyingCardSpardeSprite);
        generateQuestion();
    }

    public void generateQuestion()
    {
        // 如果界面上已存在扑克牌，删除
        for (int i = 0; i < cards.Length; i++)
        {
            if (cards[i] != null && cards[i].gameObject != null)
            {
                Destroy(cards[i].gameObject);
            }
        }

        for (int i = 0; i < 4; i++)
        {
            int temp = Random.Range(0, 13);
            selectNums[i] = temp + 1;
            cards[i] = Instantiate(CARD, cardContainer.transform).GetComponent<Image>();
            cards[i].sprite = playingCardSpriteList[i][temp];
        }
    }


    public List<string> generateCorrectAnswer()
    {
        return null;
    }

    public bool checkAnswer(string expression)
    {
        int result = helper.calc(expression, selectNums);
        if (result == 24)
            return true;
        else return false;
    }

    bool muteFlag = false;
    public void soundSwitch()
    {
        if (muteFlag)
        {
            soundImage.sprite = soundSprite[0];
            GetComponent<AudioSource>().volume = 0.5f;
        }
        else
        {
            soundImage.sprite = soundSprite[1];
            GetComponent<AudioSource>().volume = 0f;
        }
        muteFlag = !muteFlag;
    }
}
