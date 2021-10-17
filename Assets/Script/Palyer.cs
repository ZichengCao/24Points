using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Palyer : MonoBehaviour
{
    public Message message;
    public InputFieldScaler anwserInput;
    public GameAdministrator administrator;
    public void answer()
    {
        string expression = anwserInput.inputField.text;
        try
        {
            bool result = administrator.checkAnswer(expression);
            if (result)
            {
                message.showTips("恭喜你，答对了");
            }
            else
            {
                message.showWarning("恭喜你，答错了");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.StackTrace);
            message.showWarning(e.Message);
        }
    }
    public void getCorrectAnswer()
    {

    }
}