namespace MTG.Importer.Models;

public record RelatedCard
{
    // A field explaining what role this card plays in this relationship, one of token, meld_part, meld_result, or combo_piece.
    public string Component { get; }
    public string Name { get; }
    public string TypeLine { get; }

    public RelatedCard(string component, string name, string typeLine)
    {
        Component = component;
        Name = name;
        TypeLine = typeLine;
    }
}
