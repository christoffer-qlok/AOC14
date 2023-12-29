namespace AOC14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            var map = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            // Move
            for (int y = 1;y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'O')
                    {
                        for (int newY = y-1; newY >= 0; newY--)
                        {
                            if (map[newY][x] != '.')
                                break;

                            map[newY][x] = 'O';
                            map[newY + 1][x] = '.';
                        }
                    }
                }
            }


            // Count
            int sum = 0;
            for (int y = 0; y < map.Length; y++)
            {
                for(int x = 0;x < map[y].Length; x++)
                {
                    if (map[y][x] == 'O')
                    {
                        sum += map.Length - y;
                    }
                }
            }
            Console.WriteLine(sum);
        }
    }
}
