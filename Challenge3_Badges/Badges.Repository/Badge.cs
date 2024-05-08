namespace Badges.Repository;

public class Badge
{
  public int BadgeID { get; set; }
  public List<string> DoorNameList { get; set; }
  public string BadgeName { get; set; }

// Constructors
public Badge(){ }

public Badge(int ID, List<string> doorList, string name)
{
  BadgeID = ID;
  DoorNameList = doorList;
  BadgeName = name;
}
}