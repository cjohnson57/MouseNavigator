using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public struct GachaInfo
{
    public string Name { get; set; }
    public string NameFrench { get; set; }

    public GachaInfo(string name, string nameFrench)
    {
        Name = name;
        NameFrench = nameFrench;
    }
}

public partial class Gachas
{
    public List<GachaInfo> GachaList = new List<GachaInfo>()
    {
        new GachaInfo("Dollar", "Dollar"),
        new GachaInfo("Euro", "Euro"),
        new GachaInfo("Yen", "Yen"),
    };
}


