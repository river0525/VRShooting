using UnityEngine;

public static class FlagManager
{
    private static int flagNum = 10;
    public static bool[] flags = new bool[flagNum];
    public enum FlagName
    {
        getBucket,
        getKey,
        digested
    }

    public static void EnableFlag(FlagName flagname)
    {
        flags[(int)flagname] = true;
    }

    public static bool CheckFlag(FlagName flagname)
    {
        return flags[(int)flagname];
    }

    public static void ResetFlag()
    {
        for (int i = 0; i < flagNum; ++i) flags[i] = false;
    }
}
