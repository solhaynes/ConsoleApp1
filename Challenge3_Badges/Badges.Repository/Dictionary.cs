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

    // Update
    public bool UpdateExistingBadge(int originalID, List<string> newList)
    {
     // Find the badge
      bool foundID = ValidateID(originalID);
      List<string> oldBadgeList = _badgeDictionary[originalID];

      // Update the badge list
      if (foundID == true)
     {
        oldBadgeList = newList;

       return true;
      }
      else 
      {
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

