using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MariusBudrauskasPyramid
{
    class Solver
    {
        private class Node
        {
            public int Value { get; set; }
            public Node PreviousValidNode { get; set; }
        }

        public Result GetPyramidOddEvenLongestPath(string stringArray)
        {
            var pyramid = StringToNodeArray(stringArray);
            if (pyramid != null)
            {
                for (int i = pyramid.Length - 2; i >= 0; i--)
                {
                    for (int j = 0; j < pyramid[i].Length; j++)
                    {

                        var res = GetMostValid(pyramid[i + 1][j], pyramid[i + 1][j + 1], pyramid[i][j]);
                        if (res != null)
                            pyramid[i][j].PreviousValidNode = res;
                    }
                }

                var validPath = GetValidPath(pyramid[0][0]);

                if (validPath.Count != pyramid.Length) return null;

                return new Result() { MaxSum = validPath.Sum(), Path = string.Join("->", validPath) };
            }
            return null;
        }

        private List<int> GetValidPath(Node firstNode)
        {
            List<int> path = new List<int>();
            Node currentNode = firstNode;

            while (currentNode != null)
            {
                path.Add(currentNode.Value);
                currentNode = currentNode.PreviousValidNode;
            }

            return path;
        }

        private Node[][] StringToNodeArray(string input)
        {
            if (string.IsNullOrEmpty(input)) return null;

            string[] AllInputs = input.Split(new string[] { "\n" }, StringSplitOptions.None);
            var result = new Node[AllInputs.Length][];

            for (int i = 0; i < AllInputs.Length; i++)
            {
                var stringArray = AllInputs[i].Trim().Split(' ');
                Node[] myNodes = new Node[stringArray.Length];
                for (int t = 0; t < stringArray.Length; t++)
                {
                    int myInt;
                    if (int.TryParse(stringArray[t], out myInt))
                    {
                        myNodes[t] = new Node() { Value = myInt };
                    }
                    else
                    {
                        return null;
                    }

                }

                result[i] = myNodes;
            }

            return result;
        }

        private bool IsOddEven(int input)
        {
            return input % 2 == 0;
        }

        private Node GetMostValid(Node leftNode, Node rightNode, Node originalNode)
        {
            if (leftNode == null || rightNode == null) return null;

            var originalIsOddEven = IsOddEven(originalNode.Value);
            var firstIsOddEven = IsOddEven(leftNode.Value);
            var secondIsOddEven = IsOddEven(rightNode.Value);

            if (firstIsOddEven ^ secondIsOddEven)
            {
                return firstIsOddEven ^ originalIsOddEven ? leftNode : rightNode;
            }
            else
            {
                if (firstIsOddEven == originalIsOddEven && secondIsOddEven == originalIsOddEven)
                {
                    return null;
                }

                return leftNode.Value > rightNode.Value ? leftNode : rightNode;
            }

        }
    }
}
