using System;
using System.Collections.Generic;
using System.Text;

namespace Saber_Test
{
    internal class ListRand
    {
        public ListNode Head { get; private set; }
        public ListNode Tail { get; private set; }
        public int Count { get; private set; }

        public ListRand()
        {
            Head = null;
            Tail = null;
            Count = 0;
        }

        #region List_Filling

        public void AddAfterTail(ListNode node)
        {
            if (Head == null)
            {
                SetHead(node);
                SetTail(node);
                Count = 1;
                return;
            }

            var oldTail = Tail;

            node.Prev = oldTail;
            oldTail.Next = node;
            Tail = node;
            Count += 1;
        }

        private void SetHead(ListNode node)
        {
            Head = node;
            Head.Prev = null;
        }

        private void SetTail(ListNode node)
        {
            Tail = node;
            Tail.Next = null;
        }

        public void SetRands()
        {
            var node = Head;

            while (node != null)
            {
                var rand = GetRand();
                node.Rand = rand;
                node = node.Next;
            }
        }

        private ListNode GetRand()
        {
            var nodeIndex = Random.Shared.Next(Count);
            var result = Head;

            for (int i = 0; i < nodeIndex; i++)
                result = result.Next;

            return result;
        }

        #endregion List_Filling



        #region Serialize

        // serializing, O(n)
        public void Serialize(FileStream fileStream)
        {
            var randIndexesByCurrentNode = ConvertToDictionaryWithIndexes(); // default .NET dictionary

            var node = Head;

            while (node != null)
            {
                var randIndex = randIndexesByCurrentNode[node.Rand];
                var str = $"Data = {node.Data} ; Rand = {randIndex} ; {Environment.NewLine}";
                var bytes = Encoding.Default.GetBytes(str);
                fileStream.Write(bytes);
                node = node.Next;
            }

            randIndexesByCurrentNode.Clear();
        }

        // making dictionary<node, nodeIndex>, O(n)
        private Dictionary<ListNode, int> ConvertToDictionaryWithIndexes()
        {
            var node = Head;
            var index = 0;
            var result = new Dictionary<ListNode, int>();

            while (node != null)
            {
                result[node] = index;
                index++;
                node = node.Next;
            }

            return result;
        }

        #endregion Serialize



        #region Deserialize

        // deserializing, O(n)
        public void Deserialize(FileStream fileStream)
        {
            StreamReader reader = new StreamReader(fileStream); // default .NET stream

            List<ListNode> nodes = new List<ListNode>(); // default .NET lists
            List<int> nodeRands = new List<int>();

            // filling list with nodes from stream, O(n)
            while (!reader.EndOfStream)
            {
                var nodeData = reader.ReadLine(); // reading line from file stream
                var deserializeLineResult = DeserializeLine(nodeData, out var node, out var randIndex); // deserializing node and rand index from line

                if (!deserializeLineResult)
                {
                    Console.WriteLine($"ERROR!");
                    continue;
                }

                nodes.Add(node);
                nodeRands.Add(randIndex);

                AddAfterTail(node);
            }

            reader.Close(); // closing stream

            // set rands, O(n)
            for (int i = 0; i < nodes.Count; i++)
            {
                var currentNode = nodes[i];
                var randIndex = nodeRands[i];
                var randNode = nodes[randIndex];
                currentNode.Rand = randNode;
            }

            nodes.Clear();
            nodeRands.Clear();
        }

        private bool DeserializeLine(string line, out ListNode node, out int randIndex)
        {
            node = null;
            randIndex = int.MinValue;

            try
            {
                var lineSplit = line.Split(" ; ");

                var nodeDataString = lineSplit[0];
                var nodeDataSplit = nodeDataString.Split(" = ");
                var nodeData = nodeDataSplit[1];

                var randIndexString = lineSplit[1];
                var randIndexSplit = randIndexString.Split(" = ");
                randIndex = int.Parse(randIndexSplit[1]);

                node = new ListNode();
                node.Data = nodeData;
            }
            catch (Exception e)
            {
                Console.WriteLine("ERROR!");
                Console.WriteLine(e.Message);
                Console.WriteLine(e.StackTrace);
                return false;
            }

            return true;
        }

        #endregion Deserialize



        #region Debug

        public void Print()
        {
            var node = Head;

            while (node != null)
            {
                Console.WriteLine($"Node data = [{node.Data}] ; Node rand data = [{node.Rand.Data}]");
                node = node.Next;
            }
        }

        #endregion Debug
    }

    internal class ListNode
    {
        public ListNode Prev;
        public ListNode Next;
        public ListNode Rand;
        public string Data;

        public ListNode()
        {
            Prev = null;
            Next = null;
            Rand = null;
            Data = string.Empty;
        }
    }
}
