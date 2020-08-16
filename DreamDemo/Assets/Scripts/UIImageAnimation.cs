using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIImageAnimation : MonoBehaviour
{
    public int TotalFrames = 0;
    public bool Loop = true;
    public Image Image;
    public List<Sprite> Sprites = new List<Sprite>();
    public List<int> KeyFrames = new List<int>();

    private int curIndex;
    private int curFrame;
    private int nextKeyFrame;
    private bool finish;
    // Start is called before the first frame update
    void Start()
    {
        curIndex = 0;
        nextKeyFrame = KeyFrames[curIndex];
        curFrame = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (finish)
        {
            return;
        }

        if (curFrame == nextKeyFrame)
        {
            Image.sprite = Sprites[curIndex];

            curIndex = (curIndex + 1) % KeyFrames.Count;
            nextKeyFrame = KeyFrames[curIndex];
        }

        curFrame += 1;
        if (curFrame > TotalFrames)
        {
            if (Loop)
            {
                curFrame = 0;
            }
            else
            {
                finish = true;
            }
        }
    }
}
