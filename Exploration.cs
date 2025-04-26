using System;
using System.Collections.Generic;

public class Exploration
{
    private Graph map;
    private Hero hero;
    private ChallengeBST challengeTree;
    private Stack<int> visitedRooms = new Stack<int>();
    private Stack<string> treasureStack = new Stack<string>();
    private Dictionary<int, string> roomData = new Dictionary<int, string>();
    private Random rand = new Random();

    public Exploration(Graph map, Hero hero, ChallengeBST challengeTree)
    {
        this.map = map;
        this.hero = hero;
        this.challengeTree = challengeTree;

        GenerateRoomData();
    }

    private void GenerateRoomData()
    {
        foreach (var room in map.AdjacencyList.Keys)
        {
            string[] types = { "Combat", "Puzzle", "Trap", "Safe" };
            roomData[room] = types[rand.Next(types.Length)];
        }
    }

    public void Explore()
{
    int currentRoom = map.StartRoom;
    visitedRooms.Push(currentRoom);

    Console.WriteLine("\n--- Begin Exploration ---");

    while (hero.Health > 0 && currentRoom != map.ExitRoom)
    {
        Console.WriteLine($"\nEntered Room {currentRoom}");
        Console.WriteLine($"Room Type: {roomData[currentRoom]}");

        TryTreasure();

        var challenge = challengeTree.FindClosest(currentRoom);
        Console.WriteLine($"Nearest Challenge: {challenge}");

        bool success = ResolveChallenge(challenge);

        if (!success)
        {
            Console.WriteLine("You failed the challenge!");
            hero.Health -= Math.Abs(hero.Strength - challenge.Difficulty); // Simple rule
        }
        else
        {
            Console.WriteLine("Challenge completed!");
            challengeTree.Remove(challenge.Difficulty);
        }

        Console.WriteLine($"HP: {hero.Health}");
        hero.ShowInventory();

        // Pick a connected room
        var options = map.AdjacencyList[currentRoom];
        Console.Write("Connected Rooms: ");
        foreach (var room in options) Console.Write(room + " ");
        Console.WriteLine();

        // Ask the player to choose a room
        Console.Write("Choose a room to enter: ");
        string userInput = Console.ReadLine();
        int nextRoom;
        
        // Validate the user input
        if (int.TryParse(userInput, out nextRoom) && options.Contains(nextRoom))
        {
            if (visitedRooms.Contains(nextRoom))
            {
                Console.WriteLine("Dead end detected, backtracking...");
                visitedRooms.Pop(); // backtrack
                currentRoom = visitedRooms.Peek();
            }
            else
            {
                visitedRooms.Push(nextRoom);
                currentRoom = nextRoom;
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please choose a valid connected room.");
        }
    }

    if (hero.Health <= 0)
    {
        Console.WriteLine("\n💀 You died.");
        ShowPathToExit(map.StartRoom, map.ExitRoom); // Show the path when player dies
    }
    else if (challengeTree.Root == null)
    {
        Console.WriteLine("\n📜 All challenges completed, but you failed to reach the exit in time!");
        ShowPathToExit(map.StartRoom, map.ExitRoom); // Show path if challenges are completed but exit not reached
    }
    else
    {
        Console.WriteLine("\n🎉 You reached the exit and survived!");
        Console.WriteLine($"Final Stats:");
        hero.ShowStats();
        hero.ShowInventory();
        Console.WriteLine($"Path Taken: {string.Join(" -> ", visitedRooms)}");
    }
}

    private bool ResolveChallenge(Challenge challenge)
    {
        switch (challenge.Type)
        {
            case "Combat":
                return hero.Strength >= challenge.Difficulty || hero.HasItem("Sword");
            case "Puzzle":
                return hero.Intelligence >= challenge.Difficulty || hero.HasItem("Hint Scroll");
            case "Trap":
                return hero.Agility >= challenge.Difficulty || hero.HasItem("Smoke Bomb");
            default:
                return true;
        }
    }

    private void TryTreasure()
    {
        int chance = rand.Next(1, 11); // 10% chance
        if (chance == 1)
        {
            string[] treasures = { "Gold", "Gem", "Magic Feather", "Elixir" };
            string treasure = treasures[rand.Next(treasures.Length)];
            treasureStack.Push(treasure);
            Console.WriteLine($"🎁 Found Treasure: {treasure}");

            switch (treasure)
            {
                case "Gold":
                    hero.Strength += 1;
                    Console.WriteLine("Strength boosted by +1!");
                    break;
                case "Gem":
                    hero.Intelligence += 1;
                    Console.WriteLine("Intelligence boosted by +1!");
                    break;
                case "Magic Feather":
                    hero.Agility += 1;
                    Console.WriteLine("Agility boosted by +1!");
                    break;
                case "Elixir":
                    hero.Health += 5;
                    Console.WriteLine("Health +5!");
                    break;
            }
        }
    }

    // New method to show the path to the exit if health reaches 0 or challenges are finished
    private void ShowPathToExit(int start, int exit)
    {
        Console.WriteLine($"\n📍 Path from Room {start} to Exit {exit}:");
        var path = FindPathDFS(start, exit, new HashSet<int>());
        if (path.Count == 0)
        {
            Console.WriteLine("No path found!");
        }
        else
        {
            Console.WriteLine(string.Join(" -> ", path));
        }
    }

    // Depth-First Search (DFS) to find the path from start to exit
    private List<int> FindPathDFS(int current, int target, HashSet<int> visited)
    {
        if (current == target)
            return new List<int> { current };

        visited.Add(current);

        foreach (var neighbor in map.AdjacencyList[current])
        {
            if (!visited.Contains(neighbor))
            {
                var path = FindPathDFS(neighbor, target, visited);
                if (path.Count > 0)
                {
                    path.Insert(0, current);
                    return path;
                }
            }
        }

        return new List<int>();
    }
}
