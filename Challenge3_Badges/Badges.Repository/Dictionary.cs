using System.Collections.Generic;

namespace Badges.Repository

public class Dictionary
{
  Dictionary<int, List<string>> _badgeDictionary = new Dictionary<int, List<string>>();

  // Create
  public void CreateBadge(Badge badge)
  {
    _badgeDictionary.Add(badge.BadgeID, badge.DoorNameList);
  }

  // Read
  public Dictionary<int, List<string>> GetDictionary ()
  {
    return new Dictionary<int, List<string>>(_badgeDictionary);
  }

  // Update
  public bool UpdateExistingBadge(int originalID, Badges newBadge)
  {
    // Find the badge
    Badge oldBadge = GetBadgeByID(originalID);

    // Update the badge
    if (oldBadge != null)
    {
      oldBadge.BadgeID = newBadge.BadgeID;
      oldBadge.DoorNameList = newBadge.DoorNameList;
      oldBadge.BadgeName = newBadge.BadgeName;
    }
  }

  // Delete
  public bool RemoveBadgeFromDictionary(int id)
  {
    Badge badge = GetBadgeByID(id);

    if (badge == null)
    {
      return false;
    }

    int intitialCount = _badgeDictionary.Count;
    _badgeDictionary.Remove(badge);

    if (intitialCount > _badgeDictionary.Count)
    {
      return true;
    }
    else {
      return false;
    }
  }

  // Find a badge by the id
  public Badge GetBadgeByID(string id)
  {
    foreach(Badge badge in _badgeDictionary)
    {
      if(badge.BadgeID == id)
      {
        return badge;
      }
    }
    return null;
  }
}