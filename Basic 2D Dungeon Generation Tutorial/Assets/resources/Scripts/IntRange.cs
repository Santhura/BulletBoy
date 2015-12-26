using System;

//Serializable so it will show up in the inspector
[Serializable]
public class IntRange {

    public int m_Min;           // the minimum value in this range
    public int m_Max;           // the maximum value in this range

    // Contrustor to set the values
    public IntRange(int min, int max)
    {
        m_Max = max;
        m_Min = min;
    }

    //Get a random value from the range
    public int Random
    {
        get { return UnityEngine.Random.Range(m_Min, m_Max); }
    }
}
