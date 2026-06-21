using UnityEngine;

public static class ChipironUtility 
{
    public static float EvaluateVector2(Vector2 vector, float time)
    {
        return vector.x + (vector.y - vector.x) * time;
    }

    public static int RandomRound(float num)
    {
        int num2 = Mathf.FloorToInt(num);
        return num2 + (Random.Range(1,100) <= (num - num2) * 100 ?  1 : 0);
    }
}
