using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Floors 1-10
    public class Floors01_10
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
                    floor = GetFloor01();
                    break;

                case 2:
                    floor = GetFloor02();
                    break;

                case 3:
                    floor = GetFloor03();
                    break;

                case 4:
                    floor = GetFloor04();
                    break;

                case 5:
                    floor = GetFloor05();
                    break;

                case 6:
                    floor = GetFloor06();
                    break;

                case 7:
                    floor = GetFloor07();
                    break;
                
                case 8:
                    floor = GetFloor08();
                    break;
                
                case 9:
                    floor = GetFloor09();
                    break;
                
                case 10:
                    floor = GetFloor10();
                    break;
            }

            return floor;
        }

        // Floor 01
        public static Floor GetFloor01()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 1;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "02A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A", "03A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "03A", "03A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "03A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.geometry = geometry;

            // Enemies
            string[,] enemies = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS] {
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.enemies = enemies;

            // Items
            string[,] items = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS] {
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0;

            // Skybox
            floor.skyboxId = 0;

            // BGM
            floor.bgmId = 0;

            return floor;
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