using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using sprint0Test.Enemy;
using sprint0Test.Items;

namespace sprint0Test.Managers
{
    public class ItemManager
    {
        private static ItemManager instance;
        public static ItemManager Instance
        {
            get
            {
                if (instance == null)
                    throw new Exception("ItemManager not initialized. Call Initialize(ItemFactory) first.");
                return instance;
            }
        }

        private readonly Dictionary<string, List<IItem>> roomItems = new();
        private readonly ItemFactory itemFactory;

        private ItemManager(ItemFactory factory)
        {
            this.itemFactory = factory;
        }

        public static void Initialize(ItemFactory factory)
        {
            if (instance == null)
                instance = new ItemManager(factory);
        }

        public void LoadFromCSV(string filePath)
        {
            roomItems.Clear();

            if (!File.Exists(filePath)) {
                return;
            }
                

            string[] lines = File.ReadAllLines(filePath);

            foreach (string line in lines.Skip(1))
            {
                string[] parts = line.Split(',');

                if (parts.Length < 4) continue;

                string roomID = parts[0].Trim();
                string itemType = parts[1].Trim();
                if (!float.TryParse(parts[2], out float x) || !float.TryParse(parts[3], out float y))
                    continue;

                Vector2 position = new Vector2(x, y);
                IItem item = itemFactory.CreateItem(itemType, position);

                if (!roomItems.ContainsKey(roomID))
                    roomItems[roomID] = new List<IItem>();

                roomItems[roomID].Add(item);
            }
        }

        public List<IItem> GetItemsForRoom(string roomId)
        {
            return roomItems.TryGetValue(roomId, out var list) ? new List<IItem>(list) : new List<IItem>();
        }

        public void AddItem(string roomID, IItem item)
        {
            if (!roomItems.ContainsKey(roomID))
                roomItems[roomID] = new List<IItem>();
            roomItems[roomID].Add(item);
        }
    }
}
