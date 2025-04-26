using System;

public class Challenge
{
    public int Difficulty { get; set; }
    public string Type { get; set; }

    public Challenge(int difficulty, string type)
    {
        Difficulty = difficulty;
        Type = type;
    }

    public override string ToString()
    {
        return $"{Type} (Difficulty: {Difficulty})";
    }
}

public class ChallengeNode
{
    public Challenge Data { get; set; }
    public ChallengeNode Left { get; set; }
    public ChallengeNode Right { get; set; }

    public ChallengeNode(Challenge data)
    {
        Data = data;
    }
}

public class ChallengeBST
{
    public ChallengeNode Root { get; private set; }

    public void Insert(Challenge challenge)
    {
        Root = Insert(Root, challenge);
    }

    private ChallengeNode Insert(ChallengeNode node, Challenge challenge)
    {
        if (node == null) return new ChallengeNode(challenge);

        if (challenge.Difficulty < node.Data.Difficulty)
            node.Left = Insert(node.Left, challenge);
        else
            node.Right = Insert(node.Right, challenge);

        return node;
    }

    public Challenge FindClosest(int target)
    {
        return FindClosest(Root, target, Root.Data);
    }

    private Challenge FindClosest(ChallengeNode node, int target, Challenge closest)
    {
        if (node == null) return closest;

        if (Math.Abs(node.Data.Difficulty - target) < Math.Abs(closest.Difficulty - target))
            closest = node.Data;

        if (target < node.Data.Difficulty)
            return FindClosest(node.Left, target, closest);
        else
            return FindClosest(node.Right, target, closest);
    }

    public Challenge Remove(int difficulty)
    {
        Challenge removed = null;
        Root = Remove(Root, difficulty, ref removed);
        return removed;
    }

    private ChallengeNode Remove(ChallengeNode node, int difficulty, ref Challenge removed)
    {
        if (node == null) return null;

        if (difficulty < node.Data.Difficulty)
            node.Left = Remove(node.Left, difficulty, ref removed);
        else if (difficulty > node.Data.Difficulty)
            node.Right = Remove(node.Right, difficulty, ref removed);
        else
        {
            removed = node.Data;

            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            ChallengeNode minNode = FindMin(node.Right);
            node.Data = minNode.Data;
            node.Right = Remove(node.Right, minNode.Data.Difficulty, ref removed);
        }

        return node;
    }

    private ChallengeNode FindMin(ChallengeNode node)
    {
        while (node.Left != null)
            node = node.Left;
        return node;
    }

    public void InOrderTraversal()
    {
        InOrderTraversal(Root);
    }

    private void InOrderTraversal(ChallengeNode node)
    {
        if (node == null) return;
        InOrderTraversal(node.Left);
        Console.WriteLine(node.Data);
        InOrderTraversal(node.Right);
    }
}
