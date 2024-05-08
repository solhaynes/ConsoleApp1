namespace Cars.Repository;

public class Car
{
  public string Make{ get; set; }
  public string Model { get; set; }
  public string TypeOfEngine { get; set; }
  public string TypeOfVehicle { get; set; }
  public double MilesPerGallon { get; set; }
  public int TotalPassengers { get; set; }

// Constructors
public Car(){ }

public Car(string make, string model, string engine, string vehicleType, double mpg, int numberOfPassengers)
{
  Make = make;
  Model = model;
  TypeOfEngine = engine;
  TypeOfVehicle = vehicleType;
  MilesPerGallon = mpg;
  TotalPassengers = numberOfPassengers;
}
}
