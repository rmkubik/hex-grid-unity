using System.Collections.Generic;

class GameState
{
  List<string> deck;

  public GameState()
  {
    deck = new List<string>();
  }

  public void AddTileToDeck(string[] tileKeys)
  {
    foreach (var tileKey in tileKeys)
    {
      AddTileToDeck(tileKey);
    }
  }

  public void AddTileToDeck(string tileKey)
  {
    deck.Add(tileKey);
  }

  // TODO: This function should look up the card
  // DATA (not resources) for the rest of the
  // app eventually.
  public string GetCurrentTile()
  {
    return deck[0];
  }
}