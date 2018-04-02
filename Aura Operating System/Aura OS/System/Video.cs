﻿/*
* PROJECT:          Aura Operating System Development
* CONTENT:          Video card detection
* PROGRAMMERS:      Valentin Charbonnier <valentinbreiz@gmail.com>
*/

namespace Aura_OS.System
{
    public class Video
    {
        public static string GetVideo()
        {
            // TO DO: Detect video card
            if (Cosmos.HAL.PCI.GetDevice(Cosmos.HAL.VendorID.VMWare, Cosmos.HAL.DeviceID.SVGAIIAdapter) != null)
            {
                return "SVGAII";
            }
            else
            {
                return "VGATextmode";
            }
        }
    }
}
