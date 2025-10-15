using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct RoomInfo
{
    public string Name { get; set; }
    public string NameFrench { get; set; }
    public bool DoubleWide { get; set; } = true;

    public RoomInfo(string name, string nameFrench, bool doubleWide = true)
    {
        Name = name;
        NameFrench = nameFrench;
        DoubleWide = doubleWide;
    }
}

public partial class Rooms
{
    public List<RoomInfo> RoomList = new List<RoomInfo>()
    {
        new RoomInfo("Baby Room", "Chambre de bébé"),
        new RoomInfo("Bedroom", "Chambre à coucher"),
        new RoomInfo("Office", "Bureau", false),
        new RoomInfo("Toilet", "Toilettes", false),
        new RoomInfo("Bathroom", "Salle de bain"),
        new RoomInfo("Kitchen", "Cuisine"),
        new RoomInfo("Dining Room", "Salle à manger"),
        new RoomInfo("Main Stairs", "Escalier principal", false),
        new RoomInfo("Living Room", "Salon"),
        new RoomInfo("Entrance", "Entrée", false),
        new RoomInfo("Garage", "Garage"),
        new RoomInfo("Workshop", "Atelier"),
        new RoomInfo("Basement Stairs", "Escalier du sous-sol", false),
        new RoomInfo("Laundry Room", "Buanderie"),
    };
}


