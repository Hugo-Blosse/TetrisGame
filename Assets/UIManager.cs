using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Text scoreText;
    private Image[] images;
    private List<RectTransform> rectTransforms = new List<RectTransform>();
    private int score;
    private List<int[]> colors = new List<int[]>();
    private List<float[]> positions = new List<float[]>();

    public void StartUIManager()
    {
        score = 0;
        scoreText = GameObject.FindGameObjectWithTag("Score" + tag[^1]).GetComponent<Text>();
        images = GameObject.FindGameObjectWithTag("Images" + tag[^1]).GetComponentsInChildren<Image>() ;
        //foreach (Image image in images)
        //{
        //    rectTransforms.Append(image.gameObject.GetComponent)
        //}

        RectTransform[] rectTransformsList = GameObject.FindGameObjectWithTag("Images" + tag[^1]).GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < rectTransformsList.Length; i++)
        {
            rectTransforms.Add(rectTransformsList[i]);
        }
        colors.Add(new int[] { 255, 20, 255 });
        colors.Add(new int[] { 20, 255, 255 });
        colors.Add(new int[] { 255, 250, 55 });
        colors.Add(new int[] { 51, 220, 60 });
        positions.Add(new float[] { -16f, 16f, 0f, 0f, 0f, 16f, 16f, 0f });
        positions.Add(new float[] { -16f, 0f, 0f, 0f, 0f, 16f, 16f, 16f });
        positions.Add(new float[] { -16f, 0f, 0f, 0f, 0f, 16f, 16f, 0f });
        positions.Add(new float[] { 16f, 16f, 0f, 0f, 0f, 16f, 16f, 0f });
    }

    // Update is called once per frame
    void Update()
    {
        scoreText.text = "Score: " + score;
    }
    public void AddScore(int points)
    {
        score += points;
    }
    public int GetScore()
    {
        return score;
    }

    public void SetImages(int TetrominoIndex)
    {
        foreach (Image image in images)
        {
            image.color = new Color((float)colors[TetrominoIndex][0]/255, (float)colors[TetrominoIndex][1]/255, (float)colors[TetrominoIndex][2]/255, 100);
        }
        for (int i = 0; i < 4; i++)
        {
            Vector2 vect = new Vector2(positions[TetrominoIndex][2 * i], positions[TetrominoIndex][2 * i + 1]);
            rectTransforms[i].anchoredPosition = vect;
        }
    }
}
