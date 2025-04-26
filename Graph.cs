using System;
using System.Collections.Generic;

public class Graph
{
    public Dictionary<int, List<int>> AdjacencyList { get; private set; } = new Dictionary<int, List<int>>();
    public int StartRoom { get; private set; } = 1;
    public int ExitRoom { get; private set; }

    public Graph(int numRooms)
    {
        GenerateMap(numRooms);
    }

    private void GenerateMap(int numRooms)
    {
        Random rand = new Random();

        for (int i = 1; i <= numRooms; i++)
        {
            AdjacencyList[i] = new List<int>();
        }

        // Ensure connected graph (path from 1 to numRooms)
        for (int i = 1; i < numRooms; i++)
        {
            AdjacencyList[i].Add(i + 1);
            AdjacencyList[i + 1].Add(i);
        }

        // Add extra random edges
        for (int i = 0; i < numRooms; i++)
        {
            int roomA = rand.Next(1, numRooms + 1);
            int roomB = rand.Next(1, numRooms + 1);

            if (roomA != roomB && !AdjacencyList[roomA].Contains(roomB))
            {
                AdjacencyList[roomA].Add(roomB);
                AdjacencyList[roomB].Add(roomA);
            }
        }

        ExitRoom = numRooms;
    }

    public void ShowMap()
    {
        Console.WriteLine("--- Map (Adjacency List) ---");
        foreach (var room in AdjacencyList)
        {
            Console.Write($"Room {room.Key}: ");
            foreach (var neighbor in room.Value)
            {
                Console.Write(neighbor + " ");
            }
            Console.WriteLine();
        }
    }
}
