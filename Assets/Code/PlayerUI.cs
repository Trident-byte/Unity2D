using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private GameObject DashBarPrefab;
    [SerializeField] private RectTransform dashBarTransform;
    public void ActivateBar()
    {
        GameObject dashBar = Instantiate(DashBarPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        dashBar.transform.SetParent(transform);
        dashBarTransform = dashBar.GetComponent<RectTransform>();
        dashBarTransform.anchoredPosition = new Vector2(110, -60);
        dashBarTransform.localScale = new Vector3(2, 0.243f, 0);
    }
}
