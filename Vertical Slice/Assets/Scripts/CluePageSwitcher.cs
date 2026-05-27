using UnityEngine;

public class CluePageSwitcher : MonoBehaviour
{
    [SerializeField] private GameObject shortPage;
    [SerializeField] private GameObject detailPage;

    private void OnEnable()
    {
        ShowShortPage();
    }

    public void ShowShortPage()
    {
        shortPage.SetActive(true);
        detailPage.SetActive(false);
    }

    public void ShowDetailPage()
    {
        shortPage.SetActive(false);
        detailPage.SetActive(true);
    }
}