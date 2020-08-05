using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tragetoria : MonoBehaviour
{
    public int dotsNumber;
    public GameObject dotsParent;
    public GameObject dotPrefab;
    public float dotSpacing;
    [Range(0.1f, 0.6f)]
    public float dotMinScale;
    [Range(0.6f, 0.9f)]
    public float dotMaxScale;

    Transform[] dotsList;

    Vector2 pos;
    float timeStamp;

    private void Start()
    {
        Hide();

        PrepareDots();
    }
    void PrepareDots()
    {
        dotsList = new Transform[dotsNumber];
        dotPrefab.transform.localScale = Vector3.one * dotMaxScale;
        float scale = dotMaxScale;
        float scaleFactor = scale / dotsNumber;
        for (int i = 0; i < dotsNumber; i++)
        {
            dotsList[i] = Instantiate(dotPrefab, null).transform;
            dotsList[i].parent = dotsParent.transform;
            dotsList[i].localScale = Vector3.one * scale;
            if (scale > dotMinScale)
            {
                scale -= scaleFactor;
            }
        }
    }

    public void UpdateDots(Vector3 ballPos,Vector2 forceApplied)
    {
        timeStamp = dotSpacing;
        for(int i  = 0;i < dotsNumber ; i++)
        {
            pos.x = (ballPos.x + forceApplied.x * timeStamp);
            pos.y = (ballPos.y + forceApplied.y * timeStamp) - (Physics.gravity.magnitude*timeStamp*timeStamp)/2;

            dotsList[i].position = pos;
            timeStamp += dotSpacing;
        }
    }

    public void Show()
    {
        dotsParent.SetActive(true);
    }
    public void Hide()
    {
        dotsParent.SetActive(false);
    }
}
