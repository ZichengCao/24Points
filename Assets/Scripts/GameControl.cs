using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;


public class GameControl : MonoBehaviour {


    // static resource
    public Sprite[] palyingCardClubSprite;
    public Sprite[] palyingCardDiamondSprite;
    public Sprite[] palyingCardHeartSprite;
    public Sprite[] palyingCardSpardeSprite;
    public GameObject CARD;
    public List<Sprite[]> playingCardSpriteList = new List<Sprite[]>();

    // dynamic gameobject
    public InputFieldScaler anwserInput;
    public GameObject cardContainer;
    public RectTransform[] buttonsRect;
    private Image[] cards = new Image[4]; //selected card
    public Player player;
    public Image imgClearAnwser;
    // ordinary
    public int[] selectNums = new int[4];

    void Start() {
        playingCardSpriteList.Add(palyingCardClubSprite);
        playingCardSpriteList.Add(palyingCardDiamondSprite);
        playingCardSpriteList.Add(palyingCardHeartSprite);
        playingCardSpriteList.Add(palyingCardSpardeSprite);
        generateQuestion();
    }

    private void Update() {
        if (anwserInput.inputField.text.Length == 0) {
            imgClearAnwser.enabled = false;
        } else {
            imgClearAnwser.enabled = true;
        }
    }

    public void generateQuestion() {
        clearAnswer();
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

    public void clearAnswer() {
        anwserInput.inputField.text = "";
    }

    public void buttonMouseIn(int sign) {
        buttonsRect[sign].DOScale(new Vector3(1.1f, 1.1f, 1f), 0.25f);
    }

    public void buttonMouseOut(int sign) {
        buttonsRect[sign].DOScale(new Vector3(1f, 1f, 1f), 0.25f);

    }

}
