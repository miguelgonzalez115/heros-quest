using System;
using System.Collections.Generic;

public class Hero
{
    public int Strength { get; set; }
    public int Agility { get; set; }
    public int Intelligence { get; set; }
    public int Health { get; set; } = 20;

    private Queue<string> inventory = new Queue<string>();

    public Hero(int strength, int agility, int intelligence)
    {
        Strength = strength;
        Agility = agility;
        Intelligence = intelligence;

        inventory.Enqueue("Sword");
        inventory.Enqueue("Health Potion");
    }

    public void AddItem(string item)
    {
        if (inventory.Count >= 5)
        {
            Console.WriteLine($"Inventory full. Discarding: {inventory.Dequeue()}");
        }

        inventory.Enqueue(item);
        Console.WriteLine($"{item} added to inventory.");
    }

    public bool HasItem(string item)
    {
        return inventory.Contains(item);
    }

    public void ShowStats()
    {
        Console.WriteLine($"Stats - STR: {Strength}, AGI: {Agility}, INT: {Intelligence}, HP: {Health}");
    }

    public void ShowInventory()
    {
        Console.WriteLine("Inventory:");
        foreach (var item in inventory)
        {
            Console.WriteLine($"- {item}");
        }
    }
}
