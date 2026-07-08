using UnityEngine;

public class InformationHolder : MonoBehaviour
{
    public static int NumberOfPlayers = -1;

    
    public static void SetNumberOfPlayers(int number)
    {
        NumberOfPlayers = number;
    }
}
