using TMPro;
using UnityEngine;

public class PointCalculation : MonoBehaviour
{
    [SerializeField] AudioSource PointGain;

    int pointammount;

    [SerializeField] TextMeshProUGUI pointText;
    public void GainPoints(int point)
    {
        Debug.Log("Gained " + point + " points");
        PointGain.Play();
        pointammount += point;
        pointText.text = pointammount.ToString();
    }
}
