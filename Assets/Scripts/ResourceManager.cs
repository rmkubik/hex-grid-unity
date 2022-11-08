using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
  Dictionary<string, Texture2D> textures = new Dictionary<string, Texture2D>();

  // Start is called before the first frame update
  void Start()
  {
    loadTextures("forestHex", "Hexes/Tiles Forests");
    loadTextures("tileLocations", "Tiles/Locations 134x134");
  }

  void loadTextures(string prekey, string path)
  {
    var newTextures = Resources.LoadAll(path, typeof(Texture2D));
    foreach (var newTexture in newTextures)
    {
      var key = prekey + '/' + newTexture.name;

      if (textures.ContainsKey(key))
      {
        Debug.LogError("Duplicate resource key added! " + key);
        break;
      }

      textures.Add(key, (Texture2D)newTexture);
    }
  }

  Texture2D GetTexture(string key)
  {
    var texture = textures[key];

    // TODO: If texture does not exist, then
    // we should log an error and return a 
    // default texture...
    // Appropriate default texture would probably
    // depend on the TYPE of texture being requested...
    // Maybe we want an underlying GetTexture function
    // But we can also have some specialized getters
    // as well?

    // If we set all textures into ONE Dictionary...
    // A - we should probably not have EVERY SINGLE texture
    // loaded into memory at a time...
    // B - How do we ensure we've prevented conflicts on texture
    // keys/ids.
    // We could have ids include their directories somehow?
    // Then each path would be unique.
    // We'd still need to allow the app to differentiate
    // SIMILAR paths though, we don't want to type full paths
    // every time we require an asset...

    return texture;
  }

}
