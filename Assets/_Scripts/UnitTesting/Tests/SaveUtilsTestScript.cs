using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NUnit.Framework;

public class SaveUtilsTestScript
{
    // test conversion of a an array containing more than 1 float
    // expected outcome: result is a string with each value in the array separated by single commas
    [Test]
    public void TestManyFloatsToCSV()
    {
        float[] input = new float[3]{ 1f, 0.5f, 0.3f };

        string output = SaveSystemUtils.ArrayToCSV(input);

        Assert.AreEqual("1,0.5,0.3", output);
    }

    // test conversion of a an array containing only 1 float
    // expected outcome: result is a string with a single floating point number (without the 'f')
    [Test]
    public void TestSingleFloatToCSV()
    {
        float[] input = new float[1] { 0.5f };

        string output = SaveSystemUtils.ArrayToCSV(input);

        Assert.AreEqual("0.5", output);
    }

    // test conversion of a an empty array
    // expected outcome: result is a an empty string
    [Test]
    public void TestEmptyArrayToCSV()
    {
        float[] input = new float[0] { };

        string output = SaveSystemUtils.ArrayToCSV(input);

        Assert.AreEqual("", output);
    }

    // test conversion from an empty string to a float array
    // expected outcome: result is an empty float array
    [Test]
    public void TestCSVToEmptyArray()
    {
        string input = "";

        float[] output = SaveSystemUtils.CSVToArray(input);

        Assert.AreEqual(0, output.Length);
    }

    // test conversion from a string with a single number to a float array
    // expected outcome: result is an array with a single floating point number
    [Test]
    public void TestCSVToSingleFloat()
    {
        string input = "0.75";

        float[] output = SaveSystemUtils.CSVToArray(input);

        Assert.AreEqual(new float[1] { 0.75f }, output);
    }

    // test conversion from a string of comma-separated-values to a float array
    // expected outcome: result is an array containing each value
    [Test]
    public void TestCSVToManyFloat()
    {
        string input = "0.5,0.8,1,2.1";

        float[] output = SaveSystemUtils.CSVToArray(input);

        Assert.AreEqual(new float[4] { 0.5f, 0.8f, 1f, 2.1f }, output);
    }

    // test conversion of a an array containing more than 1 boolean
    // expected outcome: result is a string with each value in the array separated by single commas
    [Test]
    public void TestManyBoolsToCSV()
    {
        bool[] input = new bool[3] { true, false, true };

        string output = SaveSystemUtils.ArrayToCSV(input);

        Assert.AreEqual("1,0,1", output);
    }

    // test conversion of a an array containing only 1 boolean
    // expected outcome: result is a string with a single number 1 or 0 representing true or false respectively
    [Test]
    public void TestSingleBoolToCSV()
    {
        bool[] input = new bool[1] { true };

        string output = SaveSystemUtils.ArrayToCSV(input);

        Assert.AreEqual("1", output);
    }

    // test conversion of a an empty array
    // expected outcome: result is an empty string
    [Test]
    public void TestEmptyBoolArrayToCSV()
    {
        bool[] input = new bool[0] { };

        string output = SaveSystemUtils.ArrayToCSV(input);

        Assert.AreEqual("", output);
    }

    // test conversion from an empty string to a boolean array
    // expected outcome: result is an empty bool array
    [Test]
    public void TestCSVToEmptyBoolArray()
    {
        string input = "";

        bool[] output = SaveSystemUtils.CSVToBoolArray(input);

        Assert.AreEqual(0, output.Length);
    }

    // test conversion from a string with a single integer to a bool array
    // expected outcome: result is an array with a single boolean value
    [Test]
    public void TestCSVToSingleBool()
    {
        string input = "1";

        bool[] output = SaveSystemUtils.CSVToBoolArray(input);

        Assert.AreEqual(new bool[1] { true }, output);
    }

    // test conversion from a string of comma-separated-values to a bool array
    // expected outcome: result is an array containing true or false values for each 1 or 0 respectively
    [Test]
    public void TestCSVToManyBool()
    {
        string input = "0,1,1,0";

        bool[] output = SaveSystemUtils.CSVToBoolArray(input);

        Assert.AreEqual(new bool[4] { false, true, true, false }, output);
    }
}
