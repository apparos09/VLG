using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Floors 31-40
    public class Floors31_40
    {
        // Gets the floor
        public static Floor GetFloor(int id)
        {
            // The floor object.
            Floor floor = null;

            // Gets the floor based on the ID.
            switch (id)
            {
                default:
                case 1:
                case 31:
                    floor = GetFloor01();
                    break;

                case 2:
                case 32:
                    floor = GetFloor02();
                    break;

                case 3:
                case 33:
                    floor = GetFloor03();
                    break;

                case 4:
                case 34:
                    floor = GetFloor04();
                    break;

                case 5:
                case 35:
                    floor = GetFloor05();
                    break;

                case 6:
                case 36:
                    floor = GetFloor06();
                    break;

                case 7:
                case 37:
                    floor = GetFloor07();
                    break;

                case 8:
                case 38:
                    floor = GetFloor08();
                    break;

                case 9:
                case 39:
                    floor = GetFloor09();
                    break;

                case 10:
                case 40:
                    floor = GetFloor10();
                    break;
            }

            return floor;
        }

        // Floor 01
        public static Floor GetFloor01()
        {
            return new Floor();
        }

        // Floor 02
        public static Floor GetFloor02()
        {
            return new Floor();
        }

        // Floor 03
        public static Floor GetFloor03()
        {
            return new Floor();
        }

        // Floor 04
        public static Floor GetFloor04()
        {
            return new Floor();
        }

        // Floor 05
        public static Floor GetFloor05()
        {
            return new Floor();
        }

        // Floor 06
        public static Floor GetFloor06()
        {
            return new Floor();
        }

        // Floor 07
        public static Floor GetFloor07()
        {
            return new Floor();
        }

        // Floor 08
        public static Floor GetFloor08()
        {
            return new Floor();
        }

        // Floor 09
        public static Floor GetFloor09()
        {
            return new Floor();
        }

        // Floor 10
        public static Floor GetFloor10()
        {
            return new Floor();
        }
    }
}