using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private int _pointsPerCollected;
    [SerializeField] private Text _scoreText;
    private int _totalPoints;

    [SerializeField] private Text _timerTxt;
    private float _timer;

    private void Start()
    {
        _totalPoints = 0;
        StartCoroutine(TimerRefresher());
    }

    private void Update()
    {
        _timer = _timer + Time.deltaTime;
    }
    public void UpdatePointsCounter()
    {
        _totalPoints = _totalPoints + _pointsPerCollected;
        _scoreText.text = _totalPoints.ToString();
    }

    private IEnumerator TimerRefresher()
    {
        while (true)
        {
            int minutes = Mathf.FloorToInt(_timer / 60);
            int seconds = Mathf.FloorToInt(_timer % 60);

            _timerTxt.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1f);
        }
    }
}
