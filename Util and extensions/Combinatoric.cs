using System;
using System.Collections;
using System.Collections.Generic;


class Combinatorics<T>
{
    public static CombinatoiricSet<T> GetCombination(List<T> set, int numberToPick)
    {
        List<List<T>> output = new List<List<T>>();
        GetCombinationRecursif(set, numberToPick, 0, new List<T>(), output);
        return new CombinatoiricSet<T>(output);
    }

    public static CombinatoiricSet<T> GetAllCombinations(List<T> set, int numberToPick)
    {
        List<List<T>> output = new List<List<T>>();

        for (int i = 1; i <= numberToPick; i++)
        {
            List<List<T>> tempOutput = new List<List<T>>();
            GetCombinationRecursif(set, i, 0, new List<T>(), tempOutput);
            output.AddRange(tempOutput);
        }

        return new CombinatoiricSet<T>(output);
    }

    public static CombinatoiricSet<T> GetAllCombinations(List<T> set)
    {
        List<List<T>> output = new List<List<T>>();

        for (int i = 1; i <= set.Count; i++)
        {
            List<List<T>> tempOutput = new List<List<T>>();
            GetCombinationRecursif(set, i, 0, new List<T>(), tempOutput);
            output.AddRange(tempOutput);
        }

        return new CombinatoiricSet<T>(output);
    }

    public static CombinatoiricSet<T> GetPermutation(List<T> set)
    {
        List<List<T>> output = new List<List<T>>();
        bool[] currentlPicked = new bool[set.Count];
        GetPermutationRecursif(set, currentlPicked, set.Count, new List<T>(), output);
        return new CombinatoiricSet<T>(output);
    }

    public static CombinatoiricSet<T> GetArrangement(List<T> set, int numberToPick)
    {
        List<List<T>> output = new List<List<T>>();
        bool[] currentlPicked = new bool[set.Count];
        GetPermutationRecursif(set, currentlPicked, numberToPick, new List<T>(), output);
        return new CombinatoiricSet<T>(output);
    }

    public static CombinatoiricSet<T> GetAllArrangements(List<T> set)
    {
        List<List<T>> output = new List<List<T>>();

        for (int i = 1; i <= set.Count; i++)
        {
            List<List<T>> tempOutput = new List<List<T>>();
            bool[] currentlPicked = new bool[set.Count];
            GetPermutationRecursif(set, currentlPicked, i, new List<T>(), tempOutput);
            output.AddRange(tempOutput);
        }
        return new CombinatoiricSet<T>(output);
    }


    static void GetCombinationRecursif(List<T> set, int numberToPick, int startIndex, List<T> currentList, List<List<T>> output)
    {
        if (currentList.Count == numberToPick)
        {
            output.Add(currentList);
            return;
        }

        for (int i = startIndex; i < set.Count; i++)
        {
            List<T> newList = new List<T>(currentList);
            newList.Add(set[i]);

            GetCombinationRecursif(set, numberToPick, i + 1, newList, output);
        }
    }
    static void GetPermutationRecursif(List<T> set, bool[] currentlPicked, int depth, List<T> currentList, List<List<T>> output)
    {
        if(depth == 0)
        {
            output.Add(currentList);
            return;
        }

        for (int i = 0; i < set.Count; i++)
        {
            if (currentlPicked[i])
                continue;

            bool[] newCurrentlPicked = new bool[currentlPicked.Length];
            currentlPicked.CopyTo(newCurrentlPicked, 0);
            newCurrentlPicked[i] = true;

            List<T> newList = new List<T>(currentList);
            newList.Add(set[i]);

            GetPermutationRecursif(set, newCurrentlPicked, depth - 1, newList, output);
        }
    }
}

public class CombinatoiricSet<T> : IEnumerable
{
    List<List<T>> set;

    public CombinatoiricSet(List<List<T>> set)
    {
        this.set = set;
    }

    public List<T> this[int index]
    {
        get
        {
            return set[index];
        }
    }

    public IEnumerator GetEnumerator()
    {
        for (int index = 0; index < set.Count; index++)
        {
            yield return set[index];
        }
    }

    public override string ToString()
    {
        string fullOutput = "";
        foreach (List<T> list in set)
        {
            string output = "(";
            for (int i = 0; i < list.Count; i++)
            {
                output += list[i].ToString();
                if (i < list.Count - 1)
                {
                    output += ", ";
                }
            }
            output += ") \n";

            fullOutput += output;
        }
        return fullOutput;
    }
}

