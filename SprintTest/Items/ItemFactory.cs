using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace sprint0Test.Items
{

    public class ItemFactory
    {
        private Dictionary<string, Func<Vector2, IItem>> itemRegistry;
        private Dictionary<string, Texture2D> textureRegistry;

        public ItemFactory()
        {
            itemRegistry = new Dictionary<string, Func<Vector2, IItem>>();
            textureRegistry = new Dictionary<string, Texture2D>();
        }

        // Register textures separately
        public void RegisterTexture(string itemName, Texture2D texture)
        {
            if (!textureRegistry.ContainsKey(itemName))
            {
                textureRegistry[itemName] = texture;
            }
        }

        // Register item creation logic
        public void RegisterItem(string itemName, Func<Vector2, IItem> createItem)
        {
            if (!itemRegistry.ContainsKey(itemName))
            {
                itemRegistry[itemName] = createItem;
            }
        }

        // Get texture for item creation
        public Texture2D GetTexture(string itemName)
        {
            if (textureRegistry.ContainsKey(itemName))
            {
                return textureRegistry[itemName];
            }
            throw new Exception($"Texture for '{itemName}' not found.");
        }

        public bool HasItem(string itemName)
        {
            return itemRegistry.ContainsKey(itemName);
        }

        // Create item dynamically
        public IItem CreateItem(string itemName, Vector2 position)
        {
            if (itemRegistry.ContainsKey(itemName))
            {
                return itemRegistry[itemName](position);
            }
            throw new Exception($"Item '{itemName}' not registered.");
        }
    }

}
