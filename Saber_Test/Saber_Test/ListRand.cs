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
            //node.Rand = null;
        }

        private void SetTail(ListNode node)
        {
            Tail = node;
            Tail.Next = null;
            //Tail.Rand = null;
        }

        private void SetRand(ListNode node)
        {
        }

        private ListNode GetRand()
        {
            return null;
        }

        public void Print()
        {
            var node = Head;

            while (node != null)
            {
                Console.WriteLine(node.Data);
                node = node.Next;
            }
        }

        public void Serialize(FileStream fileStream)
        {
        }

        public void Deserialize(FileStream fileStream)
        {
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

        public void SetData(string data)
        {
            Data = data;
        }
    }
}
