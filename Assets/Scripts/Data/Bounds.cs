using UnityEngine;
/// <summary>
/// Stores Screen Bounds
/// </summary>
public struct Bounds
{
    /// <summary>
    /// Constructor for Generating Min-Max of X and Z
    /// </summary>
    /// <param name="bottomLeftCornerOfScreen">Preferable to be Absolute Value For Correct Location On Screen</param>
    public Bounds(Vector3 bottomLeftCornerOfScreen)
    {
        xMax = Mathf.CeilToInt(bottomLeftCornerOfScreen.x);    // RIGHT
        xMin = -Mathf.FloorToInt(bottomLeftCornerOfScreen.x);  // LEFT
        zMax = Mathf.CeilToInt(bottomLeftCornerOfScreen.z);    // TOP
        zMin = -Mathf.FloorToInt(bottomLeftCornerOfScreen.z);  // BOTTOM
    }

    public int xMax;
    public int xMin;
    public int zMax;
    public int zMin;
}
