using System;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using UnityEngine.XR;
using System.Reflection;
using System.Collections.Generic;

namespace Mod
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Class1 : BaseUnityPlugin
    {
        private const string modGUID = "fly";
        private const string modName = "fly";
        private const string modVersion = "0.2.9";

        public void Awake()
        {
            var harmony = new Harmony(modGUID);
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    class MainPath
    {
        static InputDevice input;

        static bool grip;

        static GameObject menu;

        static void Prefix(GorillaLocomotion.Player __instance)
        {
            try
            {
                List<InputDevice> devices = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller, devices);

                if (devices.Count > 0) input = devices[0];

                input.TryGetFeatureValue(CommonUsages.primaryButton, out grip);

                if (grip)
                {
                     __instance.headCollider.transform.position = __instance.headCollider.transform.forward;
                }
         
            }
            catch
            {

            }
        }
    }
}
