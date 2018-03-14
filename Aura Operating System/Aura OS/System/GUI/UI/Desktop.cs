﻿/*
* PROJECT:          Aura Operating System Development
* CONTENT:          Desktop
* PROGRAMMER(S):    Valentin Charbonnier <valentinbreiz@gmail.com>
*/

using System;
using System.Collections.Generic;
using System.Text;
using Aura_OS.System.GUI;
using Aura_OS.System.GUI.Graphics;
using Aura_OS.System.GUI.Imaging;

using Cosmos.HAL;
using static Cosmos.HAL.Mouse;

namespace Aura_OS.System.GUI.UI
{
    class Desktop
    {

        public static VbeScreen Screen = new VbeScreen();
        public static Canvas Canvas = new Canvas(1024, 768);
        public static SdfFont terminus;

        public static int Width = 1024;
        public static int Height = 768;

        static int _frames = 0;
        static int _fps = 0;
        static int _deltaT = 0;

        private static bool flag = false;

        public static int Main()
        {
            Initialize();

            while(true)
            {
                int ret = Update();
                if (ret == 1)
                {
                    break;
                }
                Render();
            }

            Final();

            return 0;
        }

        static bool windows = false;
        static bool active = false;

        public static int Update()
        {


            while (Console.KeyAvailable)
            {
                 if (Console.ReadKey(true).Key == ConsoleKey.LeftWindows)
                 {
                    windows = true;
                 }
                 else
                 {
                    windows = false;
                 }
            }
            

            switch (Cursor.Mouse.Buttons)
            {
                case MouseState.Left:
                    Cosmos.System.Power.Reboot();
                    break;
                case MouseState.Right:
                    Cosmos.System.Power.Shutdown();
                    break;
                case MouseState.Middle:
                    break;
                case MouseState.None:
                    break;
                default:
                    break;
            }

            return 0;
        }



        //public static Image cursor;

        public static Graphics.Graphics g;

        public static void Render()
        {
            //if (_deltaT != RTC.Second)
            //{
            //    _fps = _frames;
            //    _frames = 0;
            //    _deltaT = RTC.Second;
            //}

            //_frames++;

            g = new Graphics.Graphics(Canvas);

            //if (RTC.Second > 30 && !flag)
            //{
            //flag = true;


            g.Clear(Colors.White);


            //g.DrawString(10, 10, "FPS: " + _fps, 50f, terminus, Colors.Black);
            //g.DrawString(10, 10 + 17, "Frames: " + _frames, 50f, terminus, Colors.Cyan);
            //g.DrawString(10, 10 + 17 + 17, "DeltaT: " + _deltaT, 50f, terminus, Colors.Orange);
            //g.DrawString(10, 10 + 17 + 17 + 17, "RTC.Second: " + RTC.Second, 50f, terminus, Colors.Purple);
            //}

            g.FillRectangle(0 , 738, 1024, 29, Colors.LightBlue);

            if (windows)
            {
                if (!active)
                {
                    g.FillRectangle(0, 588, 1024, 150, Colors.LightBlue);
                    active = true;
                }
                else
                {
                    active = false;
                }
            }

            Cursor.Render();

            Canvas.WriteToScreen();

            //terminus = new SdfFont(Fonts.Terminus.Terminus_fnt,
            //    Image.FromBytes(Fonts.Terminus.Terminus_ppm, "ppm"));


            //g.DrawString(10, 10, "ABCDEFGHIJKLMNOPQRSTUVWXYZ", 14f, terminus, Colors.Black);
            //Canvas.WriteToScreen();
        }

        public static void Initialize()
        {

            Console.Clear();
            Screen.SetMode(VbeScreen.ScreenSize.Size1024X768, VbeScreen.Bpp.Bpp32);
            Screen.Clear(Colors.Blue);

            _deltaT = RTC.Second;

            g = new Graphics.Graphics(Canvas);
            g.Clear(Colors.White);

            Canvas.WriteToScreen();

            Cursor.Init();
            Cursor.Enabled = true;

            //cursor = Image.FromBytes(Images.Cursors.Cif, "cif");
        }

        public static void Final()
        {
            Cosmos.System.Power.Reboot();
        }
    }
}
