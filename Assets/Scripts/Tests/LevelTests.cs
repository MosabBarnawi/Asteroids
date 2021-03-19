using NUnit.Framework;
using UnityEngine;

public class Bounds_Min_Max_Tests
{
    [Test]
    public void Level_Bounds_X_Max_TEST()
    {
        float xValue = 150f;
        Vector3 boundsSize = new Vector3(xValue, 0, 50);
        Bounds bounds = new Bounds(boundsSize);

        // X Max Test
        float expected_xMax = Mathf.CeilToInt(xValue);
        float actual_xMax = bounds.xMax;
        Assert.AreEqual(expected_xMax, actual_xMax);
    }

    [Test]
    public void Level_Bounds_Z_Max_TEST()
    {
        float zValue = 90f;
        Vector3 boundsSize = new Vector3(18, 0, zValue);
        Bounds bounds = new Bounds(boundsSize);

        // Z Max Test
        float expected_zMax = Mathf.CeilToInt(zValue);
        float actual_zMax = bounds.zMax;
        Assert.AreEqual(expected_zMax, actual_zMax);
    }

    [Test]
    public void Level_Bounds_X_Min_TEST()
    {
        float xValue = 15f;
        Vector3 boundsSize = new Vector3(xValue, 0, 10);
        Bounds bounds = new Bounds(boundsSize);

        // X Min Test
        float expected_xMin = -Mathf.FloorToInt(xValue);
        float actual_xMin = bounds.xMin;
        Assert.AreEqual(expected_xMin, actual_xMin);
    }

    [Test]
    public void Level_Bounds_Z_Min_TEST()
    {
        float zValue = 500f;
        Vector3 boundsSize = new Vector3(88, 0, zValue);
        Bounds bounds = new Bounds(boundsSize);

        // Z Min Test
        float expected_zMin = -Mathf.FloorToInt(zValue);
        float actual_zMin = bounds.zMin;
        Assert.AreEqual(expected_zMin, actual_zMin);
    }

    [Test]
    public void Level_Zero_Division_TEST()
    {
        float zValue = 0f;
        Vector3 boundsSize = new Vector3(88, 0, zValue);
        Bounds bounds = new Bounds(boundsSize);

        // Z Min Test
        float expected_zMin = -Mathf.FloorToInt(zValue);
        float actual_zMin = bounds.zMin;
        Assert.AreEqual(expected_zMin, actual_zMin);
    }
}

public class Bounds_Values_Rounding_Tests
{
    [Test]
    public void Level_Bounds_X_Min_Int_RoundDown_TEST()
    {
        float xValue = 16.1f;
        Vector3 boundsSize = new Vector3(xValue, 0, 15.1f);
        Bounds bounds = new Bounds(boundsSize);

        // X Min Test
        float expected_xMin = -Mathf.FloorToInt(xValue);
        float actual_xMin = bounds.xMin;
        Assert.AreEqual(expected_xMin, actual_xMin);
    }

    [Test]
    public void Level_Bounds_X_Max_Int_RoundUp_TEST()
    {
        float xValue = 11.1f;
        Vector3 boundsSize = new Vector3(xValue, 0, 15.1f);
        Bounds bounds = new Bounds(boundsSize);

        // X Max Test
        float expected_xMax = Mathf.CeilToInt(xValue);
        float actual_xMax = bounds.xMax;
        Assert.AreEqual(expected_xMax, actual_xMax);
    }

    [Test]
    public void Level_Bounds_Z_Min_Int_RoundDown_TEST()
    {
        float zValue = 15.1f;
        Vector3 boundsSize = new Vector3(1.0f, 0, zValue);
        Bounds bounds = new Bounds(boundsSize);

        // Z Min Test
        float expected_zMin = -Mathf.FloorToInt(zValue);
        float actual_zMin = bounds.zMin;
        Assert.AreEqual(expected_zMin, actual_zMin);
    }

    [Test]
    public void Level_Bounds_Z_Max_Int_RoundUp_TEST()
    {
        float zValue = 10.1f;
        Vector3 boundsSize = new Vector3(50f, 0, zValue);
        Bounds bounds = new Bounds(boundsSize);

        // Z Max Test
        float expected_zMax = Mathf.CeilToInt(zValue);
        float actual_zMax = bounds.zMax;
        Assert.AreEqual(expected_zMax, actual_zMax);
    }
}

public class Bounds_Expected_Location_Tests
{
    [Test]
    public void Bottom_Left_Position_TEST()
    {
        float Value = 15;
        Vector3 inputVector = new Vector3(Value, 0, Value);
        Bounds bounds = new Bounds(inputVector);

        // Bottom Left Test

        Vector3 expected_bottom_left = new Vector3(-Value, 0, -Value);
        Vector3 actual_bottom_left = new Vector3(bounds.xMin, 0, bounds.zMin);
        Assert.AreEqual(expected_bottom_left, actual_bottom_left);
    }

    [Test]
    public void Bottom_Right_Position_TEST()
    {
        float Value = 15;
        Vector3 inputVector = new Vector3(Value, 0, Value);
        Bounds bounds = new Bounds(inputVector);

        // Bottom Right Test

        Vector3 expected_bottom_right = new Vector3(Value, 0, -Value);
        Vector3 actual_bottom_right = new Vector3(bounds.xMax, 0, bounds.zMin);
        Assert.AreEqual(expected_bottom_right, actual_bottom_right);
    }

    [Test]
    public void Top_Left_Position_TEST()
    {
        float Value = 15;
        Vector3 inputVector = new Vector3(Value, 0, Value);
        Bounds bounds = new Bounds(inputVector);

        // Top Left Test

        Vector3 expected_top_left = new Vector3(-Value, 0, Value);
        Vector3 actual_top_left = new Vector3(bounds.xMin, 0, bounds.zMax);
        Assert.AreEqual(expected_top_left, actual_top_left);
    }

    [Test]
    public void Top_Right_Position_TEST()
    {
        float Value = 15;
        Vector3 inputVector = new Vector3(Value, 0, Value);
        Bounds bounds = new Bounds(inputVector);

        // Top Right Test

        Vector3 expected_top_left = new Vector3(Value, 0, Value);
        Vector3 actual_top_left = new Vector3(bounds.xMax, 0, bounds.zMax);
        Assert.AreEqual(expected_top_left, actual_top_left);
    }
}
