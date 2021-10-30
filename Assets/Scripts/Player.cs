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

    public Button submitBtn, generateBtn;
    public Text tipsTextArea;
    public GameObject tipsContainer;
    private void Update() {
        // 右回车，左回车
        if (!isRun && (Input.GetKeyDown(KeyCode.KeypadEnter)|| Input.GetKeyDown(KeyCode.Return))) {
            isRun = true;
            submit();
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            UnityEngine.SceneManagement.SceneManager.LoadScene("StartScene");
        }
    }

    public void submit() {
        string expression = anwserInput.inputField.text;
        expression = expression.Replace('（', '(');
        expression = expression.Replace('）', ')');
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
            tipsContainer.SetActive(false);
        }
        tipFlag = !tipFlag;
    }
}