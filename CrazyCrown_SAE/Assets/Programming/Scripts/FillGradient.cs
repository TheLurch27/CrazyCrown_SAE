using UnityEngine;
using UnityEngine.UI;

public class FillGradient : MonoBehaviour
{
    public Slider slider;

    public Gradient gradient;
    public Image fillImage;

    public Image happyImage;
    public Image normalImage;
    public Image stressedImage;
    public Image angryImage;

    private void Update()
    {
        float sliderNormalizedValue = slider.normalizedValue;

        fillImage.color = gradient.Evaluate(sliderNormalizedValue);

        float sliderValue = slider.value;

        if (sliderValue >= 0.76f)
        {
            ShowImage(happyImage);
            HideImage(normalImage);
            HideImage(stressedImage);
            HideImage(angryImage);
        }
        else if (sliderValue >= 0.51f)
        {
            HideImage(happyImage);
            ShowImage(normalImage);
            HideImage(stressedImage);
            HideImage(angryImage);
        }
        else if (sliderValue >= 0.26f)
        {
            HideImage(happyImage);
            HideImage(normalImage);
            ShowImage(stressedImage);
            HideImage(angryImage);
        }
        else
        {
            HideImage(happyImage);
            HideImage(normalImage);
            HideImage(stressedImage);
            ShowImage(angryImage);
        }
    }

    private void ShowImage(Image image)
    {
        image.gameObject.SetActive(true);
    }

    private void HideImage(Image image)
    {
        image.gameObject.SetActive(false);
    }
}
