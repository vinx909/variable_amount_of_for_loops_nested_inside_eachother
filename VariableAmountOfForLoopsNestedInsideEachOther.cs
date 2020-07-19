using System;
using System.Collections.Generic;

public class VariableAmountOfForLoopsNestedInsideEachOther
{
    private int test;

    public static void Main()
    {
        int amountOfLoops = 3;
        string descriptionTest = "the indexes are: ";

        //the variable that tells how many loops are inside loops.

        //variableAmountOfLoops(amountOfLoops);
        variableAmountOfLoopsComplicated(amountOfLoops, descriptionTest);
    }

    static void variableAmountOfLoops(int amountOfLoops)
    {
        int totalNumber = 0;
        //this is just to show it works

        int loopNumber = 0;
        //there must be a way to keep track of which loop you are in and to call back to it. this variable itself should not change and only be given as a variable.

        List<Action<int>> nestedLoopContainer = new List<Action<int>>();
        //this will contain all the loops AND the final actual functionality

        Action<int> loop = (int loopNumberInternalVariable) =>
        {
            for (int i = 0; i < 10; i++)
            {
                nestedLoopContainer[loopNumberInternalVariable + 1].Invoke(loopNumberInternalVariable + 1);
            }
        };
        //this contains the actual loop. by calling to loopNumberInternalVariable + 1 it effectively calls for the next loop. MAKE SURE that it doesn't call to it's own index as that'll lead to an infinite loop.
        Action<int> function = (int doesNothing) =>
        {
            Console.Write(totalNumber + " ");
            totalNumber++;
        };
        //this contains what must be nested inside the final for loop. the "loop" and the "function" must be of the exact same type, which is also the same type the "nestedLoopContainer" is a list of. even if "loop" or "function" makes no use of it. in this case the int does nothing in function, but is required to be there for it to be stored in "nestedLoopContainer".

        for (int i = 0; i < amountOfLoops; i++)
        {
            nestedLoopContainer.Add(loop);
        }
        //here we add the variable amount of loops to the container, effectively nesting them inside each other because of the call to the container the loops make.
        nestedLoopContainer.Add(function);
        //here the actual function is added. if this one is forgotten you will get a nullPointerException.

        nestedLoopContainer[loopNumber].Invoke(loopNumber);
        //here the first loop is called, which will start the nested loop.
    }
    static void variableAmountOfLoopsComplicated(int amountOfLoops, string descriptionTest)
    {
        int[] indexes = new int[amountOfLoops];

        List<Action<int, string>> nestedLoopContainer = new List<Action<int, string>>();
        Action<int, string> loop = (int loopnumber, string giveAlongText) =>
            {
                for (int i = 0; i <= 2; i++)
                {
                    indexes[loopnumber] = i;
                    nestedLoopContainer[loopnumber + 1].Invoke(loopnumber + 1, giveAlongText);
                };
            };
        Action<int, string> function = (int nowIrrelavent, string text) =>
            {
                for (int i = 0; i < indexes.Length; i++)
                {
                    text += indexes[i] + ", ";
                }
                Console.WriteLine(text);
            };
        for(int i = 0; i < amountOfLoops; i++)
        {
            nestedLoopContainer.Add(loop);
        }
        nestedLoopContainer.Add(function);

        int loopnumber = 0;
        nestedLoopContainer[loopnumber].Invoke(loopnumber, descriptionTest);
    }
}