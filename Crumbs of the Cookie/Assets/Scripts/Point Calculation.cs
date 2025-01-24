using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PointCalculation : MonoBehaviour
{
    [SerializeField] AudioSource PointGain;

    int pointammount;

    void Start()
    {
        GainPoints(0);
    }

    [SerializeField] TextMeshProUGUI pointText;
    public void GainPoints(int point)
    {
        Debug.Log("Gained " + point + " points");
        PointGain.Play();
        pointammount += point;
        pointText.text = pointammount.ToString();
    }
}
