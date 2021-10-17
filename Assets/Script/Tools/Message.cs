using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

using UnityEngine.UI;

public class Message : MonoBehaviour
{
    public Text messageText;
    public RectTransform messageRectTrans;

    private float anminationDuration = 1.5f;
    private float waitDuration = 2f;


    public void showWarning(string text)
    {
        StartCoroutine(messageCoroutine(text, new Color(1, 0, 0, 1)));
    }

    public void showTips(string text)
    {
        StartCoroutine(messageCoroutine(text, new Color(0, 1, 0, 1)));
    }

    private IEnumerator messageCoroutine(string text, Color textColor)
    {
        messageText.gameObject.SetActive(true);
        messageText.text = text;
        messageText.DOColor(textColor, anminationDuration);
        float originPosY = messageRectTrans.anchoredPosition.y;
        messageRectTrans.DOAnchorPosY(originPosY + 30, anminationDuration);
        yield return new WaitForSeconds(waitDuration);
        messageText.DOFade(0, anminationDuration);
        messageRectTrans.DOAnchorPosY(originPosY, anminationDuration);
        yield return new WaitForSeconds(waitDuration);
        messageText.gameObject.SetActive(false);
    }
}
