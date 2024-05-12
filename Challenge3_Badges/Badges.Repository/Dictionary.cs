namespace Badges.Repository;

  public class BadgeDictionary
  {
    private Dictionary<int, List<string>> _badgeDictionary = new Dictionary<int, List<string>>();

    // Create
    public void CreateBadge(Badge badge)
    {
      _badgeDictionary.Add(badge.BadgeID, badge.DoorNameList);
    }

   // Read
    public Dictionary<int, List<string>> GetDictionary()
    {
      return new Dictionary<int, List<string>>(_badgeDictionary);
    }

    public List<string> GetListByID(int id)
    {
      return new List<string>(_badgeDictionary[id]);
    }

    public void DisplayAllBadges()
  {
    System.Console.WriteLine("\nAll badges in dictionary: ");
    System.Console.WriteLine("Badge ID     Doors it can access");

    foreach (KeyValuePair<int, List<string>> badge in _badgeDictionary)
    {
      System.Console.Write(badge.Key + "         ");
      badge.Value.ForEach(door => System.Console.Write(door + " "));
      System.Console.WriteLine();
    }
  }

    // Update
    public bool RemoveDoorFromList(int badgeID, string door)
    {
      int intitialCount = _badgeDictionary[badgeID].Count;
      _badgeDictionary[badgeID].Remove(door);

      if (intitialCount > _badgeDictionary[badgeID].Count)
      {
        return true;
      }
      else {
        return false;
      }
    }

    public bool RemoveAllDoorsFromBadge(int badgeID)
    {
      _badgeDictionary[badgeID] = new List<string>();

      if (_badgeDictionary[badgeID].Count == 0)
      {
        return true;
      }
      else {
        return false;
      }
    }

    public bool AddDoorToList(int badgeID, string door)
    {
      int intitialCount = _badgeDictionary[badgeID].Count;
      _badgeDictionary[badgeID].Add(door);

      if (intitialCount < _badgeDictionary[badgeID].Count)
      {
        return true;
      }
      else {
        return false;
      }
    }

    // Delete
    public bool RemoveBadgeFromDictionary(int id)
    {
      bool valid = ValidateID(id);

      int intitialCount = _badgeDictionary.Count;
      _badgeDictionary.Remove(id);

      if (intitialCount > _badgeDictionary.Count)
      {
       return true;
      }
      else {
        return false;
      }
    }

    // Find a badge by the id
   public bool ValidateID(int id)
    {
      if (_badgeDictionary.ContainsKey(id))
      {
        return true;
      }
      return false;
    }
  }
