namespace Part2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var lines = File.ReadAllLines("input.txt");
            char[][] map = CreateMap(lines);

            var hashToIndices = new Dictionary<int, List<long>>();
            var hashToWeight = new Dictionary<int, int>();
            long goal = 1000000000;
            for (long i = 1; i < 1000; i++)
            {
                TiltNorth(map);
                TiltWest(map);
                TiltSouth(map);
                TiltEast(map);

                if (i > 500)
                {
                    int hash = GetMapHash(map);

                    if (!hashToIndices.ContainsKey(hash))
                    {
                        int weight = GetWeight(map);
                        hashToIndices[hash] = new List<long>();
                        hashToWeight[hash] = weight;
                    }
                    hashToIndices[hash].Add(i);
                }
            }

            foreach (var p in hashToIndices)
            {
                var diff = GetDiff(p.Value);
                if(diff == 0)
                    continue;
                if ((goal - p.Value[0]) % diff == 0)
                {
                    Console.WriteLine($"Found answer: {hashToWeight[p.Key]}");
                    break;
                }
            }
        }

        private static long GetDiff(List<long> l)
        {
            long diff = l[1] - l[0];
            for (int i = 2; i < l.Count(); i++)
            {
                long newDiff = l[i] - l[i - 1];
                if (diff != newDiff)
                {
                    return 0;
                }
            }
            return diff;
        }

        private static int GetMapHash(char[][] map)
        {
            string[] lines = new string[map.Length];
            for (int i = 0;i < map.Length;i++)
            {
                lines[i] = string.Join("", map[i]);
            }
            return string.Join("", lines).GetHashCode();
        }

        private static char[][] CreateMap(string[] lines)
        {
            var map = new char[lines.Length][];
            for (int i = 0; i < lines.Length; i++)
            {
                map[i] = lines[i].ToCharArray();
            }

            return map;
        }

        private static int GetWeight(char[][] map)
        {
            int sum = 0;
            for (int y = 0; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'O')
                    {
                        sum += map.Length - y;
                    }
                }
            }

            return sum;
        }

        private static void TiltNorth(char[][] map)
        {
            for (int y = 1; y < map.Length; y++)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'O')
                    {
                        for (int newY = y - 1; newY >= 0; newY--)
                        {
                            if (map[newY][x] != '.')
                                break;

                            map[newY][x] = 'O';
                            map[newY + 1][x] = '.';
                        }
                    }
                }
            }
        }

        private static void TiltSouth(char[][] map)
        {
            for (int y = map.Length - 2; y >= 0; y--)
            {
                for (int x = 0; x < map[y].Length; x++)
                {
                    if (map[y][x] == 'O')
                    {
                        for (int newY = y + 1; newY <= map.Length - 1; newY++)
                        {
                            if (map[newY][x] != '.')
                                break;

                            map[newY][x] = 'O';
                            map[newY - 1][x] = '.';
                        }
                    }
                }
            }
        }

        private static void TiltEast(char[][] map)
        {
            for (int x = map[0].Length - 2; x >= 0; x--)
            {
                for (int y = 0; y < map.Length; y++)
                {
                    if (map[y][x] == 'O')
                    {
                        for (int newX = x + 1; newX <= map[0].Length - 1; newX++)
                        {
                            if (map[y][newX] != '.')
                                break;

                            map[y][newX] = 'O';
                            map[y][newX - 1] = '.';
                        }
                    }
                }
            }
        }

        private static void TiltWest(char[][] map)
        {
            for (int x = 1; x < map[0].Length; x++)
            {
                for (int y = 0; y < map.Length; y++)
                {
                    if (map[y][x] == 'O')
                    {
                        for (int newX = x - 1; newX >= 0; newX--)
                        {
                            if (map[y][newX] != '.')
                                break;

                            map[y][newX] = 'O';
                            map[y][newX + 1] = '.';
                        }
                    }
                }
            }
        }
    }
}
