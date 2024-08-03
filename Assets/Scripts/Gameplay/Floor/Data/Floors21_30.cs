using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VLG
{
    // Floors 21-30
    public class Floors21_30
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
                case 21:
                    floor = GetFloor01();
                    break;

                case 2:
                case 22:
                    floor = GetFloor02();
                    break;

                case 3:
                case 23:
                    floor = GetFloor03();
                    break;

                case 4:
                case 24:
                    floor = GetFloor04();
                    break;

                case 5:
                case 25:
                    floor = GetFloor05();
                    break;

                case 6:
                case 26:
                    floor = GetFloor06();
                    break;

                case 7:
                case 27:
                    floor = GetFloor07();
                    break;

                case 8:
                case 28:
                    floor = GetFloor08();
                    break;

                case 9:
                case 29:
                    floor = GetFloor09();
                    break;

                case 10:
                case 30:
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
            floor.id = 21;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "02B", "03A", "03A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
                { "00A", "00A", "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A"},
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
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 02
        public static Floor GetFloor02()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 22;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "01A", "02B", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "05A", "05A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "03A", "03A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "03A", "03A", "00A", "00A", "03A", "03A", "00A", "00A"},
                { "00A", "03A", "03A", "00A", "00A", "00A", "00A", "03A", "03A", "00A"},
                { "05A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "05A"},
                { "03A", "05A", "00A", "00A", "00A", "00A", "00A", "00A", "05A", "03A"}};

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
                { "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "01A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 03
        public static Floor GetFloor03()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 23;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "02B", "00A", "00A", "03A", "00A", "00A", "00A", "03A", "03A", "03A"},
                { "03A", "03A", "07B", "03A", "00A", "00A", "00A", "00A", "00A", "03A"},
                { "00A", "03A", "03A", "00A", "00A", "00A", "00A", "00A", "03A", "03A"},
                { "03A", "03A", "03A", "07A", "03A", "00A", "00A", "03A", "07A", "00A"},
                { "03A", "00A", "00A", "00A", "03A", "00A", "03A", "03A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "03A", "03A", "03A", "03A", "03A"},
                { "00A", "00A", "00A", "00A", "00A", "07B", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A", "00A"}};

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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "01A", "00A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 04
        public static Floor GetFloor04()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 24;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "09E", "03A", "01A", "03A", "03A", "03A", "07A", "03A", "03A", "09C"},
                { "00A", "00A", "03A", "08A", "04A", "06B", "08B", "00A", "00A", "00A"},
                { "00A", "00A", "03A", "08A", "04A", "06B", "08B", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "08A", "04A", "06B", "08B", "03A", "00A", "00A"},
                { "00A", "00A", "00A", "08A", "04A", "06B", "08B", "03A", "00A", "00A"},
                { "09D", "03A", "03A", "07A", "03A", "03A", "03A", "02B", "03A", "09E"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "00A", "00A"}};

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
                { "00A", "00A", "00A", "01A", "00A", "01A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "01A", "00A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 05
        public static Floor GetFloor05()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 25;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "03A", "03A", "01A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "05B", "00A", "00A", "05A", "00A", "00A", "00A"},
                { "03A", "05A", "03A", "03A", "00A", "00A", "03A", "03A", "05A", "03A"},
                { "03A", "00A", "00A", "08A", "00A", "00A", "08B", "00A", "00A", "03A"},
                { "03A", "00A", "00A", "08A", "00A", "00A", "08B", "00A", "00A", "03A"},
                { "03A", "00A", "00A", "08A", "00A", "00A", "08B", "00A", "00A", "03A"},
                { "03A", "00A", "00A", "08A", "00A", "00A", "08B", "00A", "00A", "03A"},
                { "03A", "05A", "03A", "03A", "00A", "00A", "03A", "03A", "05A", "03A"},
                { "00A", "00A", "00A", "05A", "00A", "00A", "05A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "02B", "03A", "03A", "00A", "00A", "00A"}};

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
                { "00A", "00A", "00A", "01A", "00A", "00A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "00A", "00A", "01A", "00A", "00A", "01A", "00A", "00A", "01A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "00A", "00A", "01A", "00A", "00A", "01A", "00A", "00A", "01A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "01A", "00A", "00A", "01A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 06
        public static Floor GetFloor06()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 26;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "01A", "03A", "03A", "03A", "07A", "03A", "03A", "03A", "03A", "07B"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "07C", "03A", "03A", "03A", "07A", "03A", "03A", "03A", "03A", "07B"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "07C", "03A", "03A", "03A", "07D", "03A", "03A", "03A", "03A", "07E"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "02B", "03A", "03A", "03A", "07D", "03A", "03A", "03A", "03A", "07E"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
                { "00A", "00A", "01A", "00A", "00A", "00A", "00A", "01A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "01A", "00A", "00A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "01A", "00A", "00A", "00A", "00A", "00A", "01A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 38;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 07
        public static Floor GetFloor07()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 27;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "07A", "09C", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "02B"},
                { "00A", "03A", "03A", "00A", "00A", "00A", "00A", "03A", "03A", "00A"},
                { "00A", "00A", "03A", "03A", "00A", "00A", "03A", "03A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "03A", "03A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "04A", "06B", "04B", "06A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "04A", "06B", "04B", "06A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "03A", "03A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "03A", "03A", "00A", "00A", "03A", "03A", "00A", "00A"},
                { "00A", "03A", "03A", "00A", "00A", "00A", "00A", "03A", "03A", "00A"},
                { "01A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "09D", "07A"}};

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
                { "00A", "00A", "00A", "01A", "01A", "01A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 45;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 08
        public static Floor GetFloor08()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 28;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "01A", "00A", "07D", "00A", "03A", "00A", "03A", "00A", "07E"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "06B"},
                { "00A", "07B", "08A", "03A", "00A", "07D", "08B", "07E", "08A", "06B"},
                { "00A", "00A", "03A", "08B", "03A", "08A", "03A", "00A", "07B", "00A"},
                { "00A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A"},
                { "07A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A"},
                { "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A"},
                { "02B", "00A", "07C", "00A", "07A", "00A", "09D", "00A", "07C", "00A"}};

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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "01A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 28;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 09
        public static Floor GetFloor09()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 29;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "01A", "03A", "03A", "00A", "00A", "07B", "05B", "05A", "05A"},
                { "03A", "03A", "00A", "03A", "03A", "00A", "00A", "05A", "05A", "05A"},
                { "03A", "00A", "00A", "00A", "03A", "00A", "00A", "05A", "05A", "05A"},
                { "03A", "03A", "00A", "07B", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "07A", "07C", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "05A", "03A", "03A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "05A", "07C", "00A", "03A", "03A"},
                { "05A", "05A", "05B", "07A", "00A", "03A", "00A", "00A", "00A", "03A"},
                { "05A", "05A", "05A", "00A", "00A", "03A", "03A", "00A", "03A", "03A"},
                { "05A", "05A", "05A", "00A", "00A", "00A", "03A", "03A", "02B", "00A"}};

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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "01A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 40;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }

        // Floor 10
        public static Floor GetFloor10()
        {
            Floor floor = new Floor();

            // ID and Code
            floor.id = 30;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "03A", "03A", "03A", "00A", "00A", "00A", "05A", "05A", "05A", "00A"},
                { "01A", "09C", "03A", "00A", "00A", "00A", "03A", "05A", "05A", "00A"},
                { "03A", "09D", "07B", "00A", "00A", "00A", "07C", "05A", "05A", "00A"},
                { "00A", "00A", "00A", "07B", "09C", "07C", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "09D", "07A", "09D", "00A", "07A", "03A", "02B"},
                { "00A", "00A", "00A", "07D", "09C", "07E", "00A", "00A", "00A", "00A"},
                { "06B", "06B", "07D", "00A", "00A", "00A", "07E", "04A", "04A", "00A"},
                { "06B", "06B", "03A", "00A", "00A", "00A", "03A", "04A", "04A", "00A"},
                { "06B", "06B", "06B", "00A", "00A", "00A", "04A", "04A", "04A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "01A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "01A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 42;

            // Skybox
            floor.skyboxId = 3;

            // BGM
            floor.bgmId = 3;

            return floor;
        }
    }
}
