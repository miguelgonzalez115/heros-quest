using System;

class Program
{
    static void Main(string[] args)
    {
        // Create hero  
        Hero hero = new Hero(strength: 7, agility: 6, intelligence: 5);
        Console.WriteLine("Welcome, Hero Miguel!");
        hero.ShowStats();
        hero.ShowInventory();

        // Create map
        Graph map = new Graph(15);
        map.ShowMap();
        Console.WriteLine($"Start Room: {map.StartRoom}, Exit Room: {map.ExitRoom}");

        // Create and show challenges
        ChallengeBST bst = new ChallengeBST();
        string[] types = { "Combat", "Puzzle", "Trap" };
        Random rand = new Random();

        for (int i = 0; i < 10; i++)
        {
            int diff = rand.Next(1, 21);
            string type = types[rand.Next(types.Length)];
            bst.Insert(new Challenge(diff, type));
        }

        Console.WriteLine("\nAll Challenges:");
        bst.InOrderTraversal();

        // Pick a room
        Console.WriteLine("\nEnter a room number (1-15):");
        int room = int.Parse(Console.ReadLine());

        // Find and handle closest challenge
        var challenge = bst.FindClosest(room);
        Console.WriteLine($"Closest challenge to Room {room}: {challenge}");

        Console.WriteLine("\nRemoving challenge...");
        bst.Remove(challenge.Difficulty);

        Console.WriteLine("Challenges after removal:");
        bst.InOrderTraversal();

        Exploration explorer = new Exploration(map, hero, bst);
        explorer.Explore();
    }
}
