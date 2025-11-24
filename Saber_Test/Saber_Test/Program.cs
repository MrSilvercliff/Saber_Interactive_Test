using System.Text;

namespace Saber_Test
{
    internal class Program
    {
        private static ListRand _listRand;

        private static void Main(string[] args)
        {
            _listRand = new ListRand();
            FillList();

            _listRand.Print();

            TestSerialize();
            TestDeserialize();
        }

        private static void FillList()
        {
            var count = Random.Shared.Next(10, 16);
            var builder = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                var data = GenerateListNodeData(builder);

                var node = new ListNode();
                node.SetData(data);
                _listRand.AddAfterTail(node);
            }
        }

        private static string GenerateListNodeData(StringBuilder stringBuilder)
        {
            stringBuilder.Clear();

            var now = DateTime.Now;
            stringBuilder.Append($"{now}.{now.Millisecond} ");

            var count = Random.Shared.Next(10, 21);

            for (int i = 0; i < count; i++)
            {
                var randInt = Random.Shared.Next(1, 10);
                stringBuilder.Append(randInt);
            }

            var result = stringBuilder.ToString();
            return result;
        }

        private static void TestSerialize()
        {
        }

        private static void TestDeserialize()
        {
        }
    }
}