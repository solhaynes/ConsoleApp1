namespace Cars.Repository;

public class CarRepository
{
  private List<Car> _listOfCars = new List<Car>();

  // Create
  public void AddCarToList(Car car)
  {
    _listOfCars.Add(car);
  }

  // Read
  public List<Car> GetCarList()
  {
    return new List<Car>(_listOfCars);
  }

  // Update
  public bool UpdateExistingCar(string originalModel, Car newCar)
  {
    Car oldCar = GetCarByModel(originalModel);

    if(oldCar != null)
    {
      oldCar.Make = newCar.Make;
      oldCar.Model = newCar.Model;
      oldCar.TypeOfEngine = newCar.TypeOfEngine;
      oldCar.TypeOfVehicle = newCar.TypeOfVehicle;
      oldCar.MilesPerGallon = newCar.MilesPerGallon;
      oldCar.TotalPassengers = newCar.TotalPassengers;

      return true;
    }
    else
    {
      return false;
    }
  }

  // Helper method to find car by the model
  public Car GetCarByModel(string model)
  {
    foreach(Car car in _listOfCars)
    {
      if(car.Model.ToLower() == model.ToLower())
      {
        return car;
      }
    }
    return null;
  }
  // Delete
  public bool RemoveCarFromList(string model)
  {
    Car car = GetCarByModel(model);

    if (car == null)
    {
      return false;
    }

    int initialCount = _listOfCars.Count;
    _listOfCars.Remove(car);

    if(initialCount > _listOfCars.Count)
    {
      return true;
    }
    else{
      return false;
    }
  }
}