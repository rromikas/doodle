using System;
using GameServer.Constants;
using System.Collections.Generic;
using System.Text.Json;
using System.Linq;

namespace GameServer.Models
{
    public class Composite: BaseUnit
    {
        public Composite(Coordinate coordinate, ColorTypes color) : base(coordinate, color)
        {
            Items = new List<BaseUnit>();
        }

        public void AddItem(BaseUnit item)
        {
            Items.Add(item);
        }

        public void RemoveItem(string itemId)
        {
            var item = Items.Find(x => x.Id.CompareTo(itemId) == 0);
            if (item != null)
            {
                Items.Remove(item);
            }
        }
    }
}
