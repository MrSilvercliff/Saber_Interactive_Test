using System.Text;

namespace Saber_Test
{
    internal class Program
    {
        private static ListRand _listRandSerialize;
        private static ListRand _listRandDeserialize;

        private static void Main(string[] args)
        {
            _listRandSerialize = new ListRand();
            _listRandDeserialize = new ListRand();

            FillList(_listRandSerialize);
            _listRandSerialize.SetRands();

            Console.WriteLine($"List to serialize:");
            _listRandSerialize.Print();
            Console.WriteLine();

            TestSerialize();
            TestDeserialize();

            Console.WriteLine("Deserialized list:");
            _listRandDeserialize.Print();
        }

        private static void FillList(ListRand listRand)
        {
            var count = Random.Shared.Next(4, 8);
            var builder = new StringBuilder();

            for (int i = 0; i < count; i++)
            {
                var data = GenerateListNodeData(builder);

                var node = new ListNode();
                node.Data = data;
                listRand.AddAfterTail(node);
            }
        }

        private static string GenerateListNodeData(StringBuilder stringBuilder)
        {
            stringBuilder.Clear();

            for (int i = 0; i < 4; i++)
            {
                var randInt = Random.Shared.Next(1, 10);
                stringBuilder.Append(randInt);
            }

            var result = stringBuilder.ToString();
            return result;
        }

        private static void TestSerialize()
        {
            FileStream? fileStream = null;

            try
            {
                if (File.Exists("./ListRandData.dat"))
                    fileStream = new FileStream("ListRandData.dat", FileMode.Truncate, FileAccess.Write); // rewrite file if exist
                else
                    fileStream = new FileStream("ListRandData.dat", FileMode.Create, FileAccess.Write); // create new file if doesn't exist

                _listRandSerialize.Serialize(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                fileStream?.Close();
            }
            finally
            {
                fileStream?.Close();
            }
        }

        private static void TestDeserialize()
        {
            FileStream? fileStream = null;

            try
            {
                fileStream = new FileStream("ListRandData.dat", FileMode.Open, FileAccess.Read);
                _listRandDeserialize.Deserialize(fileStream);
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR!");
                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.StackTrace);
                fileStream?.Close();
            }
            finally
            {
                fileStream?.Close();
            }
        }
    }
}