Project Overview

Hero's Quest is a text-based adventure game created as part of a college project. The game utilizes various data structures and algorithms such as graphs, binary search trees (BST), stacks, and queues to create an engaging and challenging experience for the player. The player takes on the role of a hero navigating a perilous world full of challenges, with the goal of reaching the exit room while maintaining health and completing various challenges.

Features

Map Generation: A graph representing the world where rooms are connected by paths. The player must explore these rooms, solve challenges, and ultimately find the exit.

Challenge System: A BST is used to store challenges (Combat, Traps, and Puzzles) in rooms. Each challenge has a difficulty, and players must meet certain stat requirements to succeed.

Exploration: Players use a stack to track visited rooms and ensure they don't fall into dead-ends.

Treasure: Players can find treasure in rooms, which may boost stats or provide bonus points.

Winning Condition: The player wins by reaching the exit room with health greater than 0.

Big O Analysis
1. Binary Search Tree (BST) Search
Operation: Searching for a challenge in the BST.

Description: Each room is mapped to a challenge with a specific difficulty. The difficulty is stored in a binary search tree (BST), and when a player enters a room, the game must find the closest challenge in the tree.

Time Complexity: O(log n)

In a balanced binary search tree, searching for a node takes logarithmic time relative to the number of nodes in the tree. In the worst case, the BST is balanced, so the search operation will have a time complexity of O(log n).


2. Queue Operations (Inventory Management)
Operation: Managing the hero's inventory using a queue.

Description: The hero’s inventory is managed using a queue (FIFO). When the player picks up an item, it is added to the queue. If the inventory exceeds its maximum size, the oldest item is discarded. This ensures that the inventory behaves like a queue.

Time Complexity: O(1) for both enqueue and dequeue operations.

Enqueueing and dequeueing from a queue are both constant-time operations. No matter how large the queue is, these operations take O(1) time since the queue maintains the order of items by simply updating the pointers to the first and last elements.