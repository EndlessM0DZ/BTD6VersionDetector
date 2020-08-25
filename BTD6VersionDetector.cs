using System;
using System.IO;
using System.Windows.Forms;
//made by Ty Morrison aka Endless#1418
//special thanks to BowDown097
public class BTD6VersionDetector
{
    byte[] zeroToNine = { 0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 };
    byte decimalByte = 0x2E;
    byte[] globalgamemanagers;
    public string VersionDetector(string dir)
    {
        try
        {
            int gameOffsetVersionDec = FindGameVersionOffsetDecimal(dir);
            int NumbersBeforeDec = FromDecimal(gameOffsetVersionDec, -1);//the 1 is negative to go backwards
            int NumbersAfterDec = FromDecimal(gameOffsetVersionDec, 1);//the 1 is positive to go forwards
            int totalBytes = NumbersBeforeDec + 1 + NumbersAfterDec;
            return BuildVersionString(gameOffsetVersionDec, NumbersBeforeDec, totalBytes);
        }
        catch (Exception ex)
        {
            MessageBox.Show("Could not find the game version in globalgamemanagers!\nError:\n" + ex.Message);
            return "Could not find the game version in globalgamemanagers!";
        }
    }
    private int FindGameVersionOffsetDecimal(string dir)
    {
        if (File.Exists(dir))
        {
            globalgamemanagers = File.ReadAllBytes(dir);
            for (int i = 478; i < globalgamemanagers.Length; i++)
            {
                if (globalgamemanagers[i] == decimalByte)
                {
                    for (int j = 0; j < zeroToNine.Length; j++)
                    {
                        if (globalgamemanagers[i + 1] == zeroToNine[j])
                        {
                            return i;
                        }
                    }
                }
            }
        }
        return -1;
    }
    private int FromDecimal(int gameOffsetVersionDec, int i)
    {
        bool continueSearchingBefore = true;
        while (continueSearchingBefore)
        {
            int j = 0;
            bool searchingCurrentNumber = true;
            bool tempForEachNum = false;
            while(searchingCurrentNumber)
            {
                if (globalgamemanagers[gameOffsetVersionDec + i] == zeroToNine[j])
                {
                    tempForEachNum = true;
                    searchingCurrentNumber = false;
                }
                else if (j == 9 && tempForEachNum == false)
                {
                    continueSearchingBefore = false;
                }
                if(j == 9)
                {
                    searchingCurrentNumber = false;
                }
                j++;
            }
            i--;
        }
        return Math.Abs(i);
    }
    private string BuildVersionString(int gameOffsetVersionDec, int NumbersBeforeDec, int totalBytes)
    {
        string buildVersionNumber = "";
        for (int j = 0; j < totalBytes; j++)
        {
            for (int n = 0; n < zeroToNine.Length; n++)
            {
                if (globalgamemanagers[gameOffsetVersionDec - NumbersBeforeDec + j] == zeroToNine[n])
                {
                    buildVersionNumber += n;
                }
            }
            if (globalgamemanagers[gameOffsetVersionDec - NumbersBeforeDec + j] == decimalByte)
            {
                buildVersionNumber += ".";
            }
        }
        return buildVersionNumber;
    }
}
