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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "02A", "03A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "03A", "03A", "03A", "00A", "00A"},
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
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 02
        public static Floor GetFloor02()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 2;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "03A", "03A", "03A", "03A", "03A", "02A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "04A", "00A", "04B", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "00A"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "00A"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "00A"},
                { "00A", "03A", "03A", "03A", "03A", "03A", "03A", "03A", "00A", "00A"},
                { "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 03
        public static Floor GetFloor03()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 3;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "03A", "09B", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "03A", "03A", "03A", "05A", "00A", "00A", "00A"},
                { "00A", "00A", "03A", "03A", "05B", "00A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "05C", "00A", "03A", "00A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "03A", "03A", "03A", "03A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "02D", "00A", "00A", "00A", "00A", "00A"}};

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
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 04
        public static Floor GetFloor04()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 4;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "03A", "03A", "03A", "06B", "06B", "06B", "06B", "09D"},
                { "00A", "00A", "03A", "09D", "03A", "00A", "00A", "00A", "00A", "06A"},
                { "00A", "00A", "03A", "03A", "03A", "00A", "00A", "00A", "00A", "06A"},
                { "00A", "00A", "04A", "00A", "00A", "00A", "00A", "00A", "00A", "06A"},
                { "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "06A"},
                { "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "06A"},
                { "09C", "04B", "03A", "00A", "00A", "00A", "00A", "00A", "00A", "06A"},
                { "03A", "04B", "06A", "00A", "00A", "00A", "00A", "00A", "00A", "06A"},
                { "03A", "03A", "06A", "00A", "00A", "00A", "00A", "00A", "00A", "06A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "02A"}};

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
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 05
        public static Floor GetFloor05()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 5;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "07D", "00A", "00A", "00A", "00A", "00A", "00A", "03A", "07D", "00A"},
                { "03A", "00A", "00A", "07B", "00A", "07B", "03A", "03A", "00A", "00A"},
                { "03A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "09D", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "06B", "07A", "00A", "07A", "00A", "00A", "07E", "03A", "03A", "02A"},
                { "00A", "03A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "00A", "03A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "00A", "07C", "00A", "07C", "03A", "03A", "00A", "00A"},
                { "00A", "03A", "00A", "00A", "00A", "00A", "00A", "03A", "07E", "00A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 06
        public static Floor GetFloor06()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 6;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "08A", "08A", "09D", "03A", "00A", "00A", "00A", "00A"},
                { "00A", "08A", "00A", "00A", "06A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "08A", "00A", "00A", "06A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "00A", "00A", "06A", "08B", "08B", "08B", "02A", "00A"},
                { "00A", "08B", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "08B", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "01A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 07
        public static Floor GetFloor07()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 7;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "09C", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "07A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "03A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "03A", "00A", "00A", "00A"},
                { "09E", "05A", "06B", "03A", "05C", "04A", "03A", "09D", "00A", "00A"},
                { "03A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "00A", "00A"},
                { "03A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "07A", "00A"},
                { "03A", "03A", "03A", "06B", "03A", "00A", "00A", "00A", "03A", "00A"},
                { "00A", "00A", "00A", "00A", "03A", "00A", "00A", "00A", "03A", "00A"},
                { "00A", "00A", "00A", "00A", "01A", "00A", "00A", "00A", "02A", "00A"}};

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
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 08
        public static Floor GetFloor08()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 8;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "09A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "05B", "05B", "05B", "09C", "05A", "05A", "05C", "00A", "00A"},
                { "00A", "05B", "00A", "00A", "05A", "00A", "00A", "05C", "00A", "00A"},
                { "00A", "05B", "00A", "00A", "05A", "00A", "00A", "05C", "00A", "00A"},
                { "00A", "05B", "00A", "00A", "05A", "00A", "00A", "05C", "00A", "00A"},
                { "00A", "05B", "00A", "00A", "05A", "00A", "00A", "05C", "00A", "00A"},
                { "00A", "05B", "00A", "00A", "05A", "00A", "00A", "05C", "00A", "00A"},
                { "00A", "05B", "00A", "00A", "01A", "00A", "00A", "05C", "00A", "00A"},
                { "00A", "05A", "05C", "05C", "04A", "05C", "05C", "05A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "02A", "00A", "00A", "00A", "00A", "00A"}};

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
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 09
        public static Floor GetFloor09()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 9;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "00A"},
                { "00A", "09D", "09D", "09D", "09D", "09D", "09D", "09D", "00A", "00A"},
                { "01A", "09D", "09D", "09D", "09D", "09D", "09D", "09D", "06A", "02A"},
                { "00A", "09D", "09D", "09D", "09D", "09D", "09D", "09D", "00A", "00A"},
                { "00A", "03A", "00A", "03A", "00A", "03A", "00A", "03A", "00A", "00A"},
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
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

            floor.items = items;

            // Turns Max
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }

        // Floor 10
        public static Floor GetFloor10()
        {
            // Floor
            Floor floor = new Floor();

            // ID and Code
            floor.id = 10;
            floor.code = FloorData.Instance.GetFloorCode(floor.id);

            // Geometry
            string[,] geometry = new string[FloorData.FLOOR_COLS, FloorData.FLOOR_ROWS]{
                { "00A", "09D", "09B", "00A", "00A", "00A", "00A", "08B", "00A", "00A"},
                { "00A", "00A", "08A", "00A", "00A", "00A", "00A", "08B", "00A", "00A"},
                { "07A", "08A", "03A", "00A", "08B", "08B", "08B", "09E", "08B", "08B"},
                { "00A", "00A", "06B", "06B", "02D", "00A", "00A", "08B", "00A", "00A"},
                { "00A", "00A", "00A", "00A", "01A", "00A", "00A", "08B", "00A", "00A"},
                { "00A", "00A", "08A", "00A", "08A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "08A", "00A", "08A", "00A", "00A", "00A", "00A", "00A"},
                { "08A", "08A", "07A", "08A", "08A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "08A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"},
                { "00A", "00A", "08A", "00A", "00A", "00A", "00A", "00A", "00A", "00A"}};

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
            floor.turnsMax = 0; // Max

            // Skybox
            floor.skyboxId = 1;

            // BGM
            floor.bgmId = 1;

            return floor;
        }
    }
}