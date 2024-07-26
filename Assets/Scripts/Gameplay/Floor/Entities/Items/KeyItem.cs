using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Keys for the goal.
    public class KeyItem : Item
    {
        // The list of all key items in the game.
        private static List<KeyItem> keyItems = new List<KeyItem>();

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            // If the item type is not set, set it to key.
            if (item == itemType.none)
                item = itemType.key;

            // Add the key to the list.
            if (!keyItems.Contains(this))
                keyItems.Add(this);
        }

        // This function is called when the object has become enabled and active
        protected override void OnEnable()
        {
            base.OnEnable();

            // The key is enabled, so add it to the list.
            if (!keyItems.Contains(this))
                keyItems.Add(this);
        }

        // This function is called when the behaviour has become disabled or inactive
        protected override void OnDisable()
        {
            base.OnDisable();

            // The key is disabled, so remove it from the list.
            if (keyItems.Contains(this))
                keyItems.Remove(this);
        }

        // Gets the total number of key items.
        public static int GetKeyItemCount()
        {
            return keyItems.Count;
        }

        // Remove from the switch block list.
        protected override void OnDestroy()
        {
            base.OnDestroy();

            // Remove from the key list.
            if (keyItems.Contains(this))
                keyItems.Remove(this);
        }
    }
}