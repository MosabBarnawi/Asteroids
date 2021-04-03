using UnityEngine;
using UnityEditor;
using System.IO;

public static class Helpers
{
    //TOOD EXTRACT 
    /// <summary>
    /// Get Screen Corners By Calculating Bottom Left of Screen and Extracting Other Corners Values from it
    /// </summary>
    /// <returns>Vector3 Absolute Value of Bottom Left which is the Top Right Position of Screen In world Space <br></br> Vector y = 0f </returns>
    public static Vector3 GetScreenCorners(Camera camera)
    {
        float cameraHeight = camera.transform.position.y;

        Vector3[] frustumCorners = new Vector3[4];
        camera.CalculateFrustumCorners(new Rect(0, 0, 1, 1), cameraHeight, Camera.MonoOrStereoscopicEye.Mono, frustumCorners);

        Vector3 bottomLeftOfScreenWorldPosition = camera.transform.TransformVector(frustumCorners[0]); // GET BOTTOM LEFT


        float xPosition = Mathf.Abs(bottomLeftOfScreenWorldPosition.x);
        float yPosition = 0f;
        float zPosition = Mathf.Abs(bottomLeftOfScreenWorldPosition.z);

        return new Vector3(xPosition, yPosition, zPosition);
    }

    public static void GenerateEnum(string enumName, string[] items)
    {
        string filePath = "Assets/Scripts/Enums/";
        string fileName = enumName + ".cs";

        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
            Debug.Log("Enum Folder Generated");
        }

        using (StreamWriter streamWriter = new StreamWriter(filePath + fileName))
        {
            streamWriter.WriteLine("public enum " + enumName);
            streamWriter.WriteLine("{");

            for (int i = 0; i < items.Length; i++)
            {
                items[i].Replace("(", "X");
                items[i].Replace(")", "X");
                items[i].Replace("[0-9]{2,}", "A");
                streamWriter.WriteLine("\t" + items[i] + ",");
            }

            streamWriter.WriteLine("}");
        }
        AssetDatabase.Refresh();
    }


    /// <summary>
    /// If Random is Above 0.5f it returns true
    /// </summary>
    /// <returns></returns>
    public static bool GetRandomChance()
    {
        float random = Random.value;

        if (random > 0.5f)
        {
            return true;
        }

        return false;
    }


    // TODO:: AVOID NESTING
    public static Direction GetRandomDirection()
    {
        Direction positionOfSpawn = Direction.Right;

        // MIX
        positionOfSpawn = Direction.Left;
        if (GetRandomChance())
        {
            positionOfSpawn = Direction.Bottom;
            if (GetRandomChance())
            {
                positionOfSpawn = Direction.Top;
            }
        }
        return positionOfSpawn;
    }

    public static float AngleBetweenTwoPoints(Vector3 a, Vector3 b)
    {
        return Mathf.Atan2(a.y - b.y, a.x - b.x) * Mathf.Rad2Deg;
    }
}