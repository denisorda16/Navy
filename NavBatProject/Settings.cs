using System;
using System.Collections.Generic;
using System.Text;

namespace NavBatProject
{
    struct ShipSettings
    {
        int lengthShip;
        int shipAmount;
        public override string ToString()
        {
            return "";
        }
        public bool IsValid()
        {
            return true;
        }
        public void LoadSettings(string text)
        {

        }
    }
    struct BoardSettings
    {
        int shipDistance;
        int verticalSize;
        int horizontalSize;
        public override string ToString()
        {
            return "";
        }
        public bool IsValid()
        {
            return true;
        }
        public void LoadSettings(string text)
        {

        }
    }
    class Settings
    {
        protected List<ShipSettings> shipSettings;
        protected BoardSettings boardSettings;
        public override string ToString()
        {
            return "";
        }
        public bool IsValid()
        {
            return true;
        }
        public void LoadSettings(string text)
        {

        }
    }
}
