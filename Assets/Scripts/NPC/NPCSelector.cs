using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCSelector : MonoBehaviour
{
    public List<UnityEngine.Sprite> spritelist;
    public int frames = 12;
    public int selectedSprite;
    private int varNum = 0;
    private int currentFrame;
    void Start()
    {
        selectedSprite = Random.Range(0, (spritelist.Count/4)-1);
        gameObject.GetComponent<SpriteRenderer>().sprite = spritelist[selectedSprite];
        currentFrame = frames;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentFrame > 1)
        {
            currentFrame -= 1;
        }
        else
        {
            currentFrame = frames;
            ChangeSprite();
        }
    }

    void ChangeSprite()
    {
        if (varNum == 0||varNum ==2)
        {
            varNum += 1;
        }
        else
        {
            if (Random.Range(0, 5) > 3)
            {
                varNum = 2;
            }
            else
            {
                varNum = 0;
            }
        }

        gameObject.GetComponent<SpriteRenderer>().sprite = spritelist[selectedSprite + (16 * varNum)];
    }
}
