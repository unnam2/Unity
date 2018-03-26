using UnityEngine.UI;

public class UIloading : UIHud
{
    public Slider Slider { get; private set; }

    protected override void OnAwake()
    {
        base.OnAwake();

        Slider = GetComponentInChildren<Slider>();
    }

    protected override void OnStart()
    {
        base.OnStart();
        Slider.minValue = 0f;
        Slider.maxValue = 1f;
        Slider.value = 0f;
    }
}
