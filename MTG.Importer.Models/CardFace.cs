namespace MTG.Importer.Models;

public record CardFace(int FaceId, CardCost Cost, CardName Name, CardTypeline TypeLine, CardText Text, CardColor Colors, CardColor ColorsIndicator, CardCreature? Creature, CardPlaneswalker? Planeswalker, CardBattle? Battle);
