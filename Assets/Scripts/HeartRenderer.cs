using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class HeartRenderer : MonoBehaviour
{
    [SerializeField] private Sprite _heartSprite;
    [SerializeField] private Vector2 _heartSize;
    private List<Image> _hearts = new List<Image>();


    public void UpdateHearts(int health)
    {
        InstantiateHearts(health);
        for (int i = 0; i < _hearts.Count; i++)
        {
            _hearts[i].color = i < health ? Color.white : Color.clear;
        }
    }
    
    private void InstantiateHearts(int number)
    {
        for (int i = _hearts.Count; i < number; i++)
        {
            _hearts.Add(InstantiateHeart());
        }
    }

    private Image InstantiateHeart()
    {
        GameObject newHeart = new GameObject("Heart" + (_hearts.Count+1));
        newHeart.transform.SetParent(transform);
        Image imageHeart = newHeart.AddComponent<Image>();
        newHeart.transform.localScale = new Vector3(1, 1, 1);
        newHeart.GetComponent<RectTransform>().sizeDelta = _heartSize;
        imageHeart.sprite = _heartSprite;
        return imageHeart; 
    }
}
