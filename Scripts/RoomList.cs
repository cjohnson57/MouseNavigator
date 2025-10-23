using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct RoomInfo
{
    public string Name { get; set; }
    public string NameFrench { get; set; }
    public uint VerticalHoles { get; set; }
    public bool DoubleWide { get; set; } = true;

    public RoomInfo(string name, string nameFrench, uint verticalHoles, bool doubleWide = true)
    {
        Name = name;
        NameFrench = nameFrench;
        VerticalHoles = verticalHoles;
        DoubleWide = doubleWide;
    }
}

public partial class Rooms
{
    public List<RoomInfo> RoomList = new List<RoomInfo>()
    {
        new RoomInfo("Baby Room", "Chambre de bébé", 0b11111110),
        new RoomInfo("Bedroom", "Chambre à coucher", 0b10111111),
        new RoomInfo("Office", "Bureau", 0b01100110, false),
        new RoomInfo("Toilet", "Toilettes", 0b01100110, false),
        new RoomInfo("Bathroom", "Salle de bain", 0b10111110),
        new RoomInfo("Kitchen", "Cuisine", 0b11110111),
        new RoomInfo("Dining Room", "Salle à manger", 0b1110110),
        new RoomInfo("Main Stairs", "Escalier principal", 0b00100110, false),
        new RoomInfo("Living Room", "Salon", 0b11111110),
        new RoomInfo("Entrance", "Entrée", 0b00000110, false),
        new RoomInfo("Garage", "Garage", 0b11111111),
        new RoomInfo("Workshop", "Atelier", 0b01101111),
        new RoomInfo("Basement Stairs", "Escalier du sous-sol", 0b01000110, false),
        new RoomInfo("Laundry Room", "Buanderie", 0b11111111),
    };
}


