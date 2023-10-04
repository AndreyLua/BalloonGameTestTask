using UnityEngine;

[CreateAssetMenu(fileName = "New BackgroundConfig", menuName = "BackgroundConfig")]
public class BackgroundConfig : ScriptableObject
{
    public Background BackgroundPrefab;
    public Color StartBackgroundColor;
    public Color FinalBackgroundColor;
    public Vector2 StartBackgroundPosition;
}