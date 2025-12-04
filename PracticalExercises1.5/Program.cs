using PracticalExercises1._5.Models;
using PracticalExercises1._5.Models.Interfaces;
using PracticalExercises1._5.Services;

List<Drone> fleet = new();

while (true)
{
    Console.WriteLine("\n=== DRONE FLEET ===");
    Console.WriteLine("1. List drones");
    Console.WriteLine("2. Add drone");
    Console.WriteLine("3. Pre-flight check (all)");
    Console.WriteLine("4. Take off / Land");
    Console.WriteLine("5. Set waypoint");
    Console.WriteLine("6. Capability actions");
    Console.WriteLine("7. Charge battery");
    Console.WriteLine("8. Exit");
    Console.Write("Select: ");

    string? choice = Console.ReadLine();
    Console.WriteLine();

    switch (choice)
    {
        case "1":
            foreach (var d in fleet) Console.WriteLine(d);
            break;

        case "2":
            AddDrone(fleet);
            break;

        case "3":
            foreach (var d in fleet)
                Console.WriteLine($"{d.Name}: {(d.RunSelfTest() ? "PASS" : "FAIL")}");
            break;

        case "4":
            FlightMenu(fleet);
            break;

        case "5":
            WaypointMenu(fleet);
            break;

        case "6":
            CapabilityMenu(fleet);
            break;

        case "7":
            ChargeMenu(fleet);
            break;

        case "8":
            return;

        default:
            Console.WriteLine("Invalid option.");
            break;
    }
}


// ---------- Menu Functions ----------

static Drone? ChooseDrone(List<Drone> fleet)
{
    Console.Write("Enter drone id: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid id.");
        return null;
    }

    return fleet.FirstOrDefault(d => d.Id == id);
}

static void AddDrone(List<Drone> fleet)
{
    Console.Write("Type (Survey/Delivery/Racing): ");
    string? type = Console.ReadLine();

    Console.Write("Name: ");
    string? name = Console.ReadLine();

    Drone? drone = DroneFactory.Create(type ?? "", name ?? "");
    if (drone == null)
    {
        Console.WriteLine("Unknown drone type.");
        return;
    }

    fleet.Add(drone);
    Console.WriteLine($"Added #{drone.Id} {drone.GetType().Name} \"{drone.Name}\"");
}

static void FlightMenu(List<Drone> fleet)
{
    Drone? d = ChooseDrone(fleet);
    if (d == null) return;

    Console.WriteLine("1. Take off");
    Console.WriteLine("2. Land");
    Console.Write("Select: ");

    string? c = Console.ReadLine();
    if (c == "1") d.TakeOff();
    else if (c == "2") d.Land();
    else Console.WriteLine("Invalid.");
}

static void WaypointMenu(List<Drone> fleet)
{
    Drone? d = ChooseDrone(fleet);
    if (d == null) return;

    if (d is not INavigable nav)
    {
        Console.WriteLine("This drone cannot navigate.");
        return;
    }

    Console.Write("Latitude: ");
    double lat = double.Parse(Console.ReadLine() ?? "0");

    Console.Write("Longitude: ");
    double lon = double.Parse(Console.ReadLine() ?? "0");

    nav.SetWaypoint(lat, lon);
}

static void CapabilityMenu(List<Drone> fleet)
{
    Drone? d = ChooseDrone(fleet);
    if (d == null) return;

    if (d is IPhotoCapture pc)
    {
        Console.WriteLine("1. Take Photo");
        if (Console.ReadLine() == "1") pc.TakePhoto();
        return;
    }

    if (d is ICargoCarrier cargo)
    {
        Console.WriteLine("1. Load cargo");
        Console.WriteLine("2. Unload all");
        Console.Write("Select: ");
        string? c = Console.ReadLine();

        if (c == "1")
        {
            Console.Write("Weight kg: ");
            if (double.TryParse(Console.ReadLine(), out double kg))
            {
                cargo.Load(kg);
            }
            else Console.WriteLine("Invalid weight.");
        }
        else if (c == "2")
        {
            cargo.UnloadAll();
        }
        return;
    }

    Console.WriteLine("No capabilities available for this drone.");
}

static void ChargeMenu(List<Drone> fleet)
{
    Drone? d = ChooseDrone(fleet);
    if (d == null) return;

    Console.Write("Charge amount (%): ");
    if (double.TryParse(Console.ReadLine(), out double amount))
        d.Charge(amount);
    else
        Console.WriteLine("Invalid number.");
}