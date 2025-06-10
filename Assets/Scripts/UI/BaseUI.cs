using UnityEngine;

public class BaseUI : MonoBehaviour
{

    public virtual void Initialize()
    {
    }

    public virtual void LanguageSetting()
    {
    }

    public virtual void Show() => gameObject.SetActive(true);
    public virtual void Close() => gameObject.SetActive(false);
}