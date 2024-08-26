using System.ComponentModel;

namespace MTG.Importer.Models;

[Flags]
public enum CardColor
{
    [Description("Color not defined")]
    Unknown = 0,
    [Description("No color")]
    None = 1,
    [Description("White")]
    W = 2,
    [Description("Blue")]
    U = 4,
    [Description("Black")]
    B = 8,
    [Description("Red")]
    R = 16,
    [Description("Green")]
    G = 32
}
