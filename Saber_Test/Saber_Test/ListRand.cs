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

        public void Print()
        {
            var node = Head;

            while (node != null)
            {
                Console.WriteLine($"Node data = [{node.Data}] ; Node rand data = [{node.Rand.Data}]");
                node = node.Next;
            }
        }

        public void Serialize(FileStream fileStream)
        {
            var streamWriter = new StreamWriter(fileStream); // default .NET stream;

            var randomIndexesByCurrentNode = ConvertToDictionaryWithIndexes(); // default .NET dictionary

            var node = Head;

            while (node != null)
            {
                var str = $"Data = {node.Data} ; Rand = {randomIndexesByCurrentNode[node.Rand]}";
                streamWriter.WriteLine(str);
                node = node.Next;
            }

            randomIndexesByCurrentNode.Clear();
            streamWriter.Close();
        }

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

        public void Deserialize(FileStream fileStream)
        {
            StreamReader reader = new StreamReader(fileStream);
            reader.Close();
        }
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
