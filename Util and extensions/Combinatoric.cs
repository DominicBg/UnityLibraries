using System;
using System.Collections.Generic;


class Combinatorics<T>
{
    public static List<List<T>> GetCombination(List<T> set, int numberToPick)
    {
        List<List<T>> output = new List<List<T>>();
        GetCombinationRecursif(set, numberToPick, 0, new List<T>(), output);
        return output;
    }

    public static List<List<T>> GetAllCombinations(List<T> set, int numberToPick)
    {
        List<List<T>> output = new List<List<T>>();

        for (int i = 1; i <= numberToPick; i++)
        {
            List<List<T>> tempOutput = new List<List<T>>();
            GetCombinationRecursif(set, i, 0, new List<T>(), tempOutput);
            output.AddRange(tempOutput);
        }
          
        return output;
    }


    public static List<List<T>> GetAllCombinations(List<T> set)
    {
        List<List<T>> output = new List<List<T>>();

        for (int i = 1; i <= set.Count; i++)
        {
            List<List<T>> tempOutput = new List<List<T>>();
            GetCombinationRecursif(set, i, 0, new List<T>(), tempOutput);
            output.AddRange(tempOutput);
        }

        return output;
    }

    static void GetCombinationRecursif(List<T> set, int numberToPick, int startIndex, List<T> currentList, List<List<T>> output)
    {
        if(currentList.Count == numberToPick)
        {
            output.Add(currentList);
            return;
        }

        for (int i = startIndex; i < set.Count; i++)
        {                
            List<T> newList = new List<T>(currentList);
            newList.Add(set[i]);

            GetCombinationRecursif(set, numberToPick, i+1, newList, output);
        }
    }

    static string GetStringData(List<List<T>> lists)
    {
        string fullOutput = "";
        foreach(List list in lists)
        {
            string output = "(";
            for (int i = 0; i < list.Count; i++)
            {
                output += list[i].ToString();
                if(i < list.Count-1)
                {
                    output += ", ";
                }
            }
            output += ")";

            fullOutput += output;
        }
        return fullOutput;
    }
}

