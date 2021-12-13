using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using System.IO;

public class SaveSystemTestScript
{
    private static string filePath = Application.persistentDataPath;

    // test the creation of a PlayerData object from a GameObject
    // expected outcome: position value should be saved to the first 3 indices of the PlayerData's position array
    [Test]
    public void TestPDataInit()
    {
        GameObject go = new GameObject("test object");
        go.transform.position = new Vector3(1, 4, 2);

        PlayerData pData = new PlayerData(go);

        Assert.IsTrue(go.transform.position.x == pData.position[0]
            && go.transform.position.y == pData.position[1]
            && go.transform.position.z == pData.position[2]);
    }

    // test conversion from PlayerData position array back to Vector3 transform position
    [Test]
    public void TestPDataGetPosition()
    {
        GameObject go = new GameObject("test object");
        go.transform.position = new Vector3(5, 2, 6);

        PlayerData pData = new PlayerData(go);

        Assert.IsTrue(go.transform.position.Equals(pData.GetPosition()));
    }

    // test conversion from PlayerData rotation array back to Quaternion transform rotation
    [Test]
    public void TestPDataGetRotation()
    {
        GameObject go = new GameObject("test object");
        go.transform.rotation = new Quaternion(0, 14f, 0, 1f);

        PlayerData pData = new PlayerData(go);

        Assert.IsTrue(go.transform.rotation.Equals(pData.GetRotation()));
    }

    // test the creation of a save file object using the JSONSaveSytem method
    [Test]
    public void TestJSONSave()
    {
        GameObject go = new GameObject("test object");
        go.transform.position = new Vector3(1, 2, 3);

        JSONSaveSystem.SavePlayer(go, "UnitTestSave");

        Assert.IsTrue(File.Exists(filePath + "UnitTestSave.json"));

        // delete file after creation
        File.Delete(filePath + "UnitTestSave.json");
    }

    // test loading data from a save file object using the JSONSaveSytem method
    [Test]
    public void TestJSONLoad()
    {
        GameObject go = new GameObject("test object");
        go.transform.position = new Vector3(1, 2, 3);

        JSONSaveSystem.SavePlayer(go, "UnitTestLoad");

        PlayerData pData = JSONSaveSystem.LoadPlayer("UnitTestLoad");

        Assert.IsTrue(go.transform.position.Equals(pData.GetPosition())
            && go.transform.rotation.Equals(pData.GetRotation()));

        // delete file after creation
        File.Delete(filePath + "UnitTestLoad.json");
    }
}
