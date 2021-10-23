using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Text;

public class GameControl : MonoBehaviour {


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
    public RectTransform[] buttonsRect;
    private Image[] cards = new Image[4]; //selected card
    public Player player;

    // ordinary
    public int[] selectNums = new int[4];

    void Start() {
        playingCardSpriteList.Add(palyingCardClubSprite);
        playingCardSpriteList.Add(palyingCardDiamondSprite);
        playingCardSpriteList.Add(palyingCardHeartSprite);
        playingCardSpriteList.Add(palyingCardSpardeSprite);
        generateQuestion();
    }

    public void generateQuestion() {
        // 如果界面上已存在扑克牌，删除
        for (int i = 0; i < cards.Length; i++) {
            if (cards[i] != null && cards[i].gameObject != null) {
                Destroy(cards[i].gameObject);
            }
        }
        QuestionGenerator generator = new QuestionGenerator();
        List<int> list = generator.build();
        for (int i = 0; i < 4; i++) {
            selectNums[i] = list[i] + 1;
            cards[i] = Instantiate(CARD, cardContainer.transform).GetComponent<Image>();
            cards[i].sprite = playingCardSpriteList[i][list[i]];
        }
    }



    public bool checkAnswer(string expression) {
        ExpressionCalculator helper = new ExpressionCalculator();
        int result = helper.calc(expression, selectNums);
        if (result == 24)
            return true;
        else return false;
    }

    bool muteFlag = false;
    public void soundSwitch() {
        if (muteFlag) {
            soundImage.sprite = soundSprite[0];
            GetComponent<AudioSource>().volume = 0.5f;
        } else {
            soundImage.sprite = soundSprite[1];
            GetComponent<AudioSource>().volume = 0f;
        }
        muteFlag = !muteFlag;
    }

    public void buttonMouseIn(int sign) {
        buttonsRect[sign].DOScale(new Vector3(1.1f, 1.1f, 1f), 0.25f);
    }

    public void buttonMouseOut(int sign) {
        buttonsRect[sign].DOScale(new Vector3(1f, 1f, 1f), 0.25f);

    }
}
