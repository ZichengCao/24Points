using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;


public class Player : MonoBehaviour {
    public Message message;
    public InputFieldScaler anwserInput;
    public GameControl administrator;

    public static bool isRun = false;
    System.Random rd = new System.Random();

    public Button submitBtn, generateBtn, btn_getAnAnswer, btn_getAllAnswer;
    public Text tipsTextArea;
    public GameObject tipsContainer;
    private void Update() {
        // 右回车，左回车
        if (!isRun && (Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))) {
            isRun = true;
            submit();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
        }
    }

    string[] replaceInvalid = { "（", "）", "A", "a", "J", "j", "Q", "q", "K", "k", " " };
    string[] replaceVaild = { "(", ")", "1", "1", "11", "11", "12", "12", "13", "13", "" };

    public void submit() {
        string expression = anwserInput.inputField.text;
        for (int i = 0; i < replaceInvalid.Length; i++) {
            expression = expression.Replace(replaceInvalid[i], replaceVaild[i]);
        }
        try {
            bool result = administrator.checkAnswer(expression);
            if (result) {
                message.showTips("恭喜你，答对了！");
            } else {
                message.showWarning("很遗憾，答错了！");
            }
        } catch (Exception e) {
            //Debug.Log(e.StackTrace);
            if (e.Message.StartsWith("#"))
                message.showWarning(e.Message.Substring(1));
            else
                message.showWarning("请输入合法的表达式！");
        }
    }

    bool tipFlag = false;
    public void showAnswerArea() {
        if (!tipFlag) {
            QuestionAnswerer answerer = new QuestionAnswerer();
            submitBtn.interactable = false;
            generateBtn.interactable = false;
            btn_getAnAnswer.interactable = false;
            btn_getAllAnswer.GetComponentInChildren<Text>().text = "close tips";
            List<string> ansList = answerer.run(administrator.selectNums);
            tipsContainer.SetActive(true);
            StringBuilder sb = new StringBuilder();
            foreach (string ss in ansList) {
                sb.Append(ss + "\n");
            }
            tipsTextArea.text = sb.ToString();
        } else {
            tipsTextArea.text = "";
            submitBtn.interactable = true;
            generateBtn.interactable = true;
            btn_getAnAnswer.interactable = true;
            btn_getAllAnswer.GetComponentInChildren<Text>().text = "show all answers";
            tipsContainer.SetActive(false);
        }
        tipFlag = !tipFlag;
    }

    public void getAnAnswer() {
        QuestionAnswerer answerer = new QuestionAnswerer();
        List<string> ansList = answerer.run(administrator.selectNums);
        anwserInput.inputField.text = ansList[rd.Next(0, ansList.Count)];
    }
}