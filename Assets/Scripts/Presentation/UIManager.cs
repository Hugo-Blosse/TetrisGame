using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    private int score;
    private Text scoreText;
    private Image[] images;
    private List<RectTransform> rectTransforms = new List<RectTransform>();
    private List<int[]> colors = new List<int[]>();
    private List<float[]> positions = new List<float[]>();

    void Start()
    {
        StartUIManager();
    }
    public void StartUIManager()
    {
        score = 0;
        scoreText = GameObject.FindGameObjectWithTag("Score" + tag[^1]).GetComponent<Text>();
        images = GameObject.FindGameObjectWithTag("Images" + tag[^1]).GetComponentsInChildren<Image>() ;
        
        RectTransform[] rectTransformsList = GameObject.FindGameObjectWithTag("Images" + tag[^1]).GetComponentsInChildren<RectTransform>();
        for (int i = 1; i < rectTransformsList.Length; i++)
        {
            rectTransforms.Add(rectTransformsList[i]);
        }
        // Set parameters for each tetromino block color
        colors.Add(new int[] { 255, 20, 255 });
        colors.Add(new int[] { 20, 255, 255 });
        colors.Add(new int[] { 255, 250, 55 });
        colors.Add(new int[] { 51, 220, 60 });
        // Set position for each tetromino block piece
        positions.Add(new float[] { -16f, 16f, 0f, 0f, 0f, 16f, 16f, 0f });
        positions.Add(new float[] { -16f, 0f, 0f, 0f, 0f, 16f, 16f, 16f });
        positions.Add(new float[] { -16f, 0f, 0f, 0f, 0f, 16f, 16f, 0f });
        positions.Add(new float[] { 16f, 16f, 0f, 0f, 0f, 16f, 16f, 0f });
    }
    void Update()
    {
        // Update score text
        scoreText.text = "Player" + scoreText.tag[^1] + ": " + score;
    }
    // Add score to total
    public void OnAddScore(int points, char tag)
    {
        if (this.tag[^1] == tag)
        {
            score += points;
        }
    }
    public int GetScore()
    {
        return score;
    }
    // Set tetromino block preview
    public void OnSetPreviewImages(int TetrominoIndex, char tag)
    {
        if (this.tag[^1] == tag)
        {
            // Set color for next tetromino block preview
            foreach (Image image in images)
            {
                image.color = new Color((float)colors[TetrominoIndex][0] / 255, (float)colors[TetrominoIndex][1] / 255, (float)colors[TetrominoIndex][2] / 255, 100);
            }
            // Set position for tetromino block pieces
            for (int i = 0; i < 4; i++)
            {
                Vector2 vect = new Vector2(positions[TetrominoIndex][2 * i], positions[TetrominoIndex][2 * i + 1]);
                rectTransforms[i].anchoredPosition = vect;
            }
        }
    }
    public void OnEnable()
    {
        TetrisManager.AddScore += OnAddScore;
        TetrisManager.SetPreviewImages += OnSetPreviewImages;
        GameLogicManager.GameStart += StartUIManager;
    }
}
