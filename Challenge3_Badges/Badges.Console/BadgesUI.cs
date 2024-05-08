using Badges.Repository;

namespace Badges.Console;

public class BadgesUI 
{
  private BadgeDictionary _badgeRepo = new BadgeDictionary();
  private void Menu()
  {
    bool keepRunning = true;
    
    while (keepRunning)
    {
      // Display options to the user
    System.Console.WriteLine("Hello, Security Admin. What would you like to do? \n" +
      "1. Add a badge\n" +
      "2. Edit a badge\n" +
      "3. List all badges\n" +
      "4. Exit");

      // Get user's input
      string input = System.Console.ReadLine();

      // Evaluate the user's input and act accordingly
      switch (input)
      {
        case "1": 
        // Create a new badge
          AddBadge();
          break;
        case "2": 
        // Edit a badge
          
          break;
        case "3": 
        // List all badges
          
          break;
        case "4": 
        //Exit
          System.Console.WriteLine("Goodbye!");
          keepRunning = false;
          break;
        default: 
          System.Console.WriteLine("Please enter a valid number.");
          break;
        }
      System.Console.WriteLine("Please Press any key to continue...");
      System.Console.ReadKey();
      System.Console.Clear();
    }
  }

  // Add a badge
  private void AddBadge()
  {
    Badge newBadge = new Badge();
    bool done = false;
    List<string> doorList = new List<string>();

    System.Console.Write("Enter the number on the badge: ");
    newBadge.BadgeID = int.Parse(System.Console.ReadLine());

    System.Console.Write("Give the badge a name: ");
    newBadge.BadgeName = System.Console.ReadLine();

    while (!done)
    {
      System.Console.Write("List a door that it needs access to: ");
      string input = System.Console.ReadLine();

      System.Console.Write("Any other doors? (y/n) ");
      string secondInput = System.Console.ReadLine();
      if (secondInput== "y" || secondInput == "n")
        {
          done = AddAnotherBadge(secondInput);
        }
        else{
          System.Console.WriteLine("Invalid response.  Please try again.");
        }

      doorList.Add(input);
    }

    newBadge.DoorNameList = doorList;

    _badgeRepo.CreateBadge(newBadge);
  }

// Helper method for AddBadge
  private bool AddAnotherBadge(string input)
  {
    if (input == "y")
    {
      return true;
    }
    else
    {
      return false;
    }
  }

  // Edit a badge
  private void EditBadge()
  {
    bool validID = false; 
    int userInput;
    string response;

    while (!validID)
    {
      System.Console.Write("What is the badge number to update? ");
      userInput =  int.Parse(System.Console.ReadLine());

      validID = _badgeRepo.ValidateID(userInput);

      if (validID)
      {
        System.Console.WriteLine("How would you like to update the badge?\n " + 
        "   1. Remove a door \n" +
        "   2. Add a door");

        response = System.Console.ReadLine();

        switch (response)
        {
          case "1": 
            RemoveDoorFromBadge(userInput);
            break;
          case "2": 
            break;
          default:
            System.Console.WriteLine("Please make a valid selection");
            break;
        }
      }
    } 
  }

  // Helper methods for EditBadge()
  private bool RemoveDoorFromBadge(int badgeIDNumber)
  {
    string input;
    bool doorExists = false;
    List<string> doorList = _badgeRepo[badgeIDNumber];
    bool doorRemoved = false;

    showDoors(badgeIDNumber);

    System.Console.WriteLine("Which door would you like to remove access to?");
    input = System.Console.ReadLine();

      foreach (string door in doorList)
      {
        if (door == input)
        {
          doorList.Remove(door);
          doorRemoved = true;
        }
      }

      if(doorRemoved)
      {
        System.Console.WriteLine("Door successfully removed.");
        _badgeRepo.showDoors(badgeIDNumber);
      }
      else 
      {
        System.Console.WriteLine("Door could not be removed. Try again later.");
      }
  }

  private void showDoors(int badgeIDNumber)
  {
    List<string> doorList = _badgeRepo[badgeIDNumber];
    
    System.Console.Write("Badge " + badgeIDNumber + " has access to: ");
    
    foreach (string door in doorList)
    {
      System.Console.Write(door + "  ");
    }

    System.Console.WriteLine();
  }
}