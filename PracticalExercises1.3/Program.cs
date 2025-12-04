using PracticalExercises1._3.Model;
using PracticalExercises1._3.Models;
using PracticalExercises1._3.Models.Interfaces;
using PracticalExercises1._3.Services;

var registry = new DeviceRegistry();

while (true)
{
    Console.WriteLine("\n=== SMART HOME CONSOLE REMOTE ===");
    Console.WriteLine("1. List devices");
    Console.WriteLine("2. Add device");
    Console.WriteLine("3. Toggle power");
    Console.WriteLine("4. Device actions");
    Console.WriteLine("5. Self-test all");
    Console.WriteLine("6. Exit");
    Console.Write("Select: ");

    var input = Console.ReadLine();
    Console.WriteLine();

    switch (input)
    {
        case "1": ListDevices(); break;
        case "2": AddDevice(); break;
        case "3": TogglePower(); break;
        case "4": DeviceActions(); break;
        case "5": SelfTestAll(); break;
        case "6": return;
        default: Console.WriteLine("Invalid option."); break;
    }
}

void ListDevices()
{
    if (registry.Devices.Count == 0)
    {
        Console.WriteLine("No devices added.");
        return;
    }

    foreach (var d in registry.Devices)
        Console.WriteLine($"{d.Id}: {d.GetStatus()}");
}

void AddDevice()
{
    Console.Write("Choose type (LightBulb / Thermostat / SmartPlug): ");
    string? type = Console.ReadLine()?.Trim().ToLower();

    Console.Write("Enter name: ");
    string name = Console.ReadLine() ?? "Unnamed";

    SmartDevice? device = type switch
    {
        "lightbulb" => new LightBulb(name),
        "thermostat" => new Thermostat(name),
        "smartplug" => new SmartPlug(name),
        _ => null
    };

    if (device == null)
    {
        Console.WriteLine("Invalid device type.");
        return;
    }

    registry.Add(device);
    Console.WriteLine($"Added: {device.GetStatus()}");
}

void TogglePower()
{
    var d = SelectDevice();
    if (d == null) return;

    if (d.IsOn) d.PowerOff();
    else d.PowerOn();

    Console.WriteLine($"Power toggled: {d.GetStatus()}");
}

void DeviceActions()
{
    var d = SelectDevice();
    if (d == null) return;

    Console.WriteLine("\nAvailable actions:");

    if (d is IDimmable dim)
    {
        Console.Write("Set brightness (0-100): ");
        if (int.TryParse(Console.ReadLine(), out int b))
        {
            dim.SetBrightness(b);
            Console.WriteLine("Brightness updated.");
        }
    }

    if (d is ITemperatureControl t)
    {
        Console.Write("Set temperature (10-30 °C): ");
        if (double.TryParse(Console.ReadLine(), out double temp))
        {
            t.SetTarget(temp);
            Console.WriteLine("Temperature updated.");
        }
    }

    if (d is IMeasurableLoad load)
    {
        Console.WriteLine($"Current load: {load.CurrentWatts:F1} W");
        Console.WriteLine($"Total energy: {load.TotalWh:F2} Wh");

        Console.Write("Reset energy counter? (y/n): ");
        if (Console.ReadLine()?.Trim().ToLower() == "y")
        {
            load.ResetEnergy();
            Console.WriteLine("Energy counter reset.");
        }
    }
}

void SelfTestAll()
{
    if (registry.Devices.Count == 0)
    {
        Console.WriteLine("No devices to test.");
        return;
    }

    foreach (var d in registry.Devices)
    {
        bool ok = d.SelfTest();
        Console.WriteLine($"{d.Name}: {(ok ? "PASS" : "FAIL")}");
    }
}

SmartDevice? SelectDevice()
{
    if (registry.Devices.Count == 0)
    {
        Console.WriteLine("No devices available.");
        return null;
    }

    Console.Write("Enter device ID: ");
    if (!int.TryParse(Console.ReadLine(), out int id))
    {
        Console.WriteLine("Invalid number.");
        return null;
    }

    var device = registry.GetById(id);

    if (device == null)
        Console.WriteLine("Device not found.");

    return device;
}