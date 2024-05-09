using Badges.Repository;

namespace Badges.Console;

public class BadgesUI 
{
  private BadgeDictionary _badgeRepo = new BadgeDictionary();

  public void Run()
  {
    SeedContentList();
    Menu();
  }
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
          EditBadge();
          break;
        case "3": 
        // List all badges
          DisplayAllBadges();
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
          done = AddAnotherDoor(secondInput);
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
  private bool AddAnotherDoor(string input)
  {
    if (input == "y")
    {
      return false;
    }
    else
    {
      return true;
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
      DisplayAllBadges();
      System.Console.Write("What is the badge number to update? ");
      userInput =  int.Parse(System.Console.ReadLine());

      validID = _badgeRepo.ValidateID(userInput);

      if (validID)
      {
        System.Console.WriteLine("How would you like to update the badge?\n" + 
        "   1. Remove a door \n" +
        "   2. Remove all doors \n" +
        "   3. Add a door");

        response = System.Console.ReadLine();

        switch (response)
        {
          case "1": 
          // remove a single door
            RemoveDoorFromBadge(userInput);
            break;
          case "2": 
          // remove all doors from badge
            RemoveAllDoorsFromBadge(userInput);
            break;
          case "3":
          // Add a door
            AddDoorToBadge(userInput);
            break;
          default:
            System.Console.WriteLine("Please make a valid selection");
            break;
        }
      }
    } 
  }

  // Helper methods for EditBadge()
  private void RemoveDoorFromBadge(int badgeIDNumber)
  {
    Dictionary<int, List<string>> badgeDictionary = _badgeRepo.GetDictionary();
    List<string> doorList = badgeDictionary[badgeIDNumber];

    bool doorRemoved = false;

    ShowDoors(badgeIDNumber);

    System.Console.WriteLine("Which door would you like to remove access to?");
    string input = System.Console.ReadLine();

      foreach (string door in doorList)
      {
        if (door == input)
        {
          doorList.Remove(door);
          doorRemoved = true;
        }
      }

      // bool updated = _badgeRepo.UpdateExistingBadge(badgeIDNumber, doorList);

      if(doorRemoved)
      {
        System.Console.WriteLine("Door successfully removed.");
        ShowDoors(badgeIDNumber);
      }
      else 
      {
        System.Console.WriteLine("Door could not be removed. Try again later.");
      }
  }

  private void RemoveAllDoorsFromBadge(int badgeIDNumber)
  {
    Dictionary<int, List<string>> badgeDictionary = _badgeRepo.GetDictionary();
    var doorList = badgeDictionary[badgeIDNumber].ToList();

    bool doorsRemoved = false;

    foreach (string door in doorList)
      {
        doorList.Remove(door);
      }

      bool updated = _badgeRepo.UpdateExistingBadge(badgeIDNumber, doorList);

      if(doorList.Count == 0)
      {
        doorsRemoved = true;
      }

      if(doorsRemoved)
      {
        System.Console.WriteLine("All doors successfully removed.");
      }
      else 
      {
        System.Console.WriteLine("Door could not be removed. Try again later.");
      }
  }

  private void AddDoorToBadge(int badgeIDNumber)
  {
    Dictionary<int, List<string>> badgeDictionary = _badgeRepo.GetDictionary();
    List<string> doorList = badgeDictionary[badgeIDNumber];
    
    // display doors the badge currently has access to
    ShowDoors(badgeIDNumber);
    System.Console.WriteLine("What door would you like to add to badge" + badgeIDNumber + "?");
    string userInput = System.Console.ReadLine();

    doorList.Add(userInput);

    bool updated = _badgeRepo.UpdateExistingBadge(badgeIDNumber, doorList);

    System.Console.WriteLine("Door " + userInput + " was added to the list.\n");
    ShowDoors(badgeIDNumber);
  }
  private void ShowDoors(int badgeIDNumber)
  {
    Dictionary<int, List<string>> badgeDictionary = _badgeRepo.GetDictionary();
    List<string> doorList = badgeDictionary[badgeIDNumber];
    
    System.Console.Write("Badge " + badgeIDNumber + " has access to: ");
    
    foreach (string door in doorList)
    {
      System.Console.Write(door + "  ");
    }

    System.Console.WriteLine();
  }

  // Display all badges
  private void DisplayAllBadges()
  {
    Dictionary<int, List<string>> dictionary = _badgeRepo.GetDictionary();

    System.Console.WriteLine("\nAll badges in dictionary: ");
    System.Console.WriteLine("Badge ID     Doors it can access");

    foreach (KeyValuePair<int, List<string>> badge in dictionary)
    {
      System.Console.Write(badge.Key + "         ");
      badge.Value.ForEach(door => System.Console.Write(door + " "));
      System.Console.WriteLine();
    }

  }

  private void SeedContentList()
  {
    int id1 = 1234;
    int id2 = 4321;
    int id3 = 6969;
    int id4 = 2319;

    List<string> list1 = new List<string> {"A2", "B1", "C3"};
    List<string> list2 = new List<string> {"A2", "A3", "C1", "D1"};
    List<string> list3 = new List<string> {"A2", "B1"};
    List<string> list4 = new List<string> {"A1", "B1", "B1"};

    string name1 = "badge1";
    string name2 = "badge2";
    string name3 = "badge3";
    string name4 = "badge4";

    Badge badge1 = new Badge(id1, list1, name1);
    Badge badge2 = new Badge(id2, list2, name2);
    Badge badge3 = new Badge(id3, list3, name3);
    Badge badge4 = new Badge(id4, list4, name4);

    _badgeRepo.CreateBadge(badge1);
    _badgeRepo.CreateBadge(badge2);
    _badgeRepo.CreateBadge(badge3);
    _badgeRepo.CreateBadge(badge4);
  }
}