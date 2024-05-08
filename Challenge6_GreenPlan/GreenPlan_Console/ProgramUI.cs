using Cars.Repository;

namespace GreenPlan_Console;

public class ProgramUI
{
  private CarRepository _carRepo = new CarRepository();

  // Method that runs the application
  public void Run()
  {
    SeedContentList();
    Menu();
  }

  // Menu
  private void Menu()
  {
    bool keepRunning = true;

    while(keepRunning)
    {
       // Display options to the user
      System.Console.WriteLine("Select a menu option: \n" +
      "1. Create Vehicle\n" +
      "2. Update a Vehicle\n" +
      "3. See Vehicle Details\n" +
      "4. Delete a Vehicle from List\n" +
      "5. See a List of Vehicles\n" +
      "6. Exit");

      string input = System.Console.ReadLine();

      switch (input)
      {
        case "1":
        // Create New Vehicle
          CreateNewVehicle();
          break;
        case "2":
        // Update a vehicle
          UpdateVehicle();
          break;
        case "3":
        // See vehicle details
          DisplayDetailsByMake();
          break;
        case "4":
        // Delete vehicle from the list
          DeleteVehicleFromRepository();
          break;
        case "5":
        // See All Vehicles
          DisplayVehicleList();
          break;
        case "6":
        // exit
          System.Console.WriteLine("Goodbye!");
          keepRunning = false;
          break;
        default: 
          System.Console.WriteLine("Please enter a valid number.");
          break;
      }
      System.Console.WriteLine("Press any key to continue...");
      System.Console.ReadKey();
      System.Console.Clear();
    }
  }

  private void CreateNewVehicle()
  {
    System.Console.Clear();
    Car newCar = new Car();

    // Make
    System.Console.WriteLine("Enter the make of the vehicle:");
    newCar.Make = System.Console.ReadLine();

    // Model
    System.Console.WriteLine("Enter the model of the vehicle:");
    newCar.Model = System.Console.ReadLine();

    // Engine Type
    System.Console.WriteLine("Enter the engine type (gas, hybrid, electric):");
    newCar.TypeOfEngine = System.Console.ReadLine();

    // Vehicle Type
    System.Console.WriteLine("Enter the vehicle type (ex. coup, sedan, SUV, truck, etc...)");
    newCar.TypeOfVehicle = System.Console.ReadLine();
    
    // Miles Per Gallon
    System.Console.WriteLine("Fuel Economy (in mpg for gas or miles/charge for electric):");
    newCar.MilesPerGallon = double.Parse(System.Console.ReadLine());

    // Total Passengers
    System.Console.WriteLine("How many passengers can the vehicle fit? (Please enter a number)");
    newCar.TotalPassengers = int.Parse(System.Console.ReadLine());

    _carRepo.AddCarToList(newCar);
    System.Console.WriteLine("Vehicle added to the repository.");
  }

  private void UpdateVehicle()
  {
    bool successfullyUpdatedVehicle = false;

    System.Console.WriteLine();
    DisplayAllVehicles();
    System.Console.WriteLine("\nWhich vehicle would you like to update? Enter the make of the vehicle (Case sensitive)");
    string userInput = System.Console.ReadLine();

    // save the details from the original vehicle
    Car oldCar = _carRepo.GetCarByModel(userInput);

    // Basically create a new car object to replace the old details
    Car newCar = new Car();

    // Make
    System.Console.WriteLine("Enter the make of the vehicle:");
    newCar.Make = System.Console.ReadLine();

    // Model
    System.Console.WriteLine("Enter the model of the vehicle:");
    newCar.Model = System.Console.ReadLine();

    // Engine Type
    System.Console.WriteLine("Enter the engine type (gas, hybrid, electric):");
    newCar.TypeOfEngine = System.Console.ReadLine();

    // Vehicle Type
    System.Console.WriteLine("Enter the vehicle type (ex. coup, sedan, SUV, truck, etc...)");
    newCar.TypeOfVehicle = System.Console.ReadLine();
    
    // Miles Per Gallon
    System.Console.WriteLine("Fuel Economy (in mpg for gas or miles/charge for electric):");
    newCar.MilesPerGallon = double.Parse(System.Console.ReadLine());

    // Total Passengers
    System.Console.WriteLine("How many passengers can the vehicle fit? (Please enter a number)");
    newCar.TotalPassengers = int.Parse(System.Console.ReadLine());

    successfullyUpdatedVehicle = _carRepo.UpdateExistingCar(oldCar.Model, newCar);

    if(successfullyUpdatedVehicle)
    {
      System.Console.WriteLine("Successfully updated the vehicle.");
    }
    else
    {
      System.Console.WriteLine("Something went wrong. Try again.");
    }

  }
  private void DisplayDetailsByMake()
  {
    System.Console.Clear();

    // Ask user to give a make to search
    System.Console.WriteLine("Enter the make of the vehicle you'd like to see:");
    string model = System.Console.ReadLine();

    var car = _carRepo.GetCarByModel(model);

    if (car != null)
    {
      System.Console.WriteLine("Make: " + car.Make +
        "\nModel: " + car.Model + 
        "\nEngine Type: " + car.TypeOfEngine +
        "\nVehicle Type: " + car.TypeOfVehicle + 
        "\nFuel Economy (in MPG or Miles Per Charge): " + car.MilesPerGallon + 
        "\nTotal Passengers: \n" + car.TotalPassengers);
    }
    else{
      System.Console.WriteLine("No cars by that make exist in the repository.");
    }
  }

  private void DeleteVehicleFromRepository()
{

  bool deletedCar = false;
  System.Console.WriteLine("\nWhich type of vehicle would you like to delete? \n" +
      "1. Gas\n" +
      "2. Hybrid\n" +
      "3. Electric ");
  
  string userInput = System.Console.ReadLine();

  switch (userInput)
  {
    case "1":
      DisplaySpecificVehicleTypeList("gas");
      break;
    case "2":
      DisplaySpecificVehicleTypeList("hybrid");
      break;
    case "3":
      DisplaySpecificVehicleTypeList("electric");
      break;
    default: 
      System.Console.WriteLine("Invalid Input. Try again.");
      break;
  }

  System.Console.WriteLine("What is the model of the vehicle you'd like to delete? (case sensitive)");
  userInput = System.Console.ReadLine();
  
  deletedCar = _carRepo.RemoveCarFromList(userInput);

  if(deletedCar)
  {
    System.Console.WriteLine("Vehicle successfully deleted from repository.");
  }
  else
  {
    System.Console.WriteLine("Something went wrong. Try again.");
  }
}
  private void DisplayVehicleList()
  {
    System.Console.WriteLine("\nWhich type of vehicle list would you like to see? \n" +
      "1. Gas Vehicles\n" +
      "2. Hybrid Vehicles\n" +
      "3. Electric Vehicles\n" + 
      "4. All vehicles in repository\n");

    string userInput = System.Console.ReadLine();

    switch (userInput)
    {
      case "1":
        // Gas vehicle list
        DisplaySpecificVehicleTypeList("gas");
        break;
      case "2":
        // Hybrid vehicle list
        DisplaySpecificVehicleTypeList("hybrid");
        break;
      case "3":
        // Electric vehicle list
        DisplaySpecificVehicleTypeList("electric");
        break;
      case "4":
        // Print all vehicles
        DisplayAllVehicles();
        break;
      default: 
        System.Console.WriteLine("Invalid input. Try again.");
        break;
    }
  }

  private void DisplaySpecificVehicleTypeList(string input)
  {
    List<Car> carList = _carRepo.GetCarList();

    if (input == "1")
    {
      System.Console.WriteLine("All gas-Powered vehicles in repository:");
    }
    else if (input == "2")
    {
      System.Console.WriteLine("All hybrid-Powered vehicles in repository:");
    }
    else if (input == "3")
    {
      System.Console.WriteLine("All electric-Powered vehicles in repository:");
    }

    foreach (Car vehicle in carList )
    {
      if (vehicle.TypeOfEngine == input)
      {
        System.Console.WriteLine(vehicle.Make + " " + vehicle.Model);
      }
    }
  }

  private void DisplayAllVehicles()
  {
    List<Car> carList = _carRepo.GetCarList();

    System.Console.WriteLine("All vehicles in repository:");
    foreach (Car vehicle in carList)
    {
      System.Console.WriteLine(vehicle.Make + " " + vehicle.Model);
    }
  }

  private void SeedContentList()
  {
    Car civic = new Car("Honda", "Civic", "gas", "sedan", 32.4, 5);
    Car prius = new Car("Toyota", "Prius", "hybrid", "sedan", 55.3, 5);
    Car suburban = new Car("Chevy", "Suburban", "gas", "SUV", 15.7, 7);
    Car cybertruck = new Car("Tesla", "Cybertruck", "electric", "truck", 150.0, 5);
    Car stingray = new Car("Corvette", "Stingray", "gas", "coup", 25.3, 2);
    Car tacoma = new Car("Toyota", "Tacoma", "gas", "truck", 14.6, 5);
    Car carollaCrossHybrid = new Car("Toyota", "Carolla Cross Hybrid", "hybrid", "sedan", 40.5, 5);

    _carRepo.AddCarToList(civic);
    _carRepo.AddCarToList(prius);
    _carRepo.AddCarToList(suburban);
    _carRepo.AddCarToList(cybertruck);
    _carRepo.AddCarToList(stingray);
    _carRepo.AddCarToList(tacoma);
    _carRepo.AddCarToList(carollaCrossHybrid);
  }
}
