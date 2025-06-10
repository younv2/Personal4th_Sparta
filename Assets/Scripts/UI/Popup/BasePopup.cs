
using UnityEngine.UI;

public class BasePopup : BaseUI
{
    Button closeBtn;
    
    public override void Initialize()
    {
        base.Initialize();

        closeBtn = transform.Find("CloseBtn")?.GetComponent<Button>();
        closeBtn?.onClick.AddListener(() => Close());

        Close();
    }

    protected virtual void OnClickEventSetting() { }
}