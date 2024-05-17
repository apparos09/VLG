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

            if (!keyItems.Contains(this))
                keyItems.Add(this);
        }

        // Gets the total number of key items.
        public static int GetKeyItemCount()
        {
            return keyItems.Count;
        }

        // Update is called once per frame
        protected override void Update()
        {
            base.Update();
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