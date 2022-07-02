using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Harmony;
using UnityEngine;

namespace SnowWhiteWitheredVine_MOD
{
    public class Harmony_Patch
    {
        public Harmony_Patch()
        {
            try
            {
                var harmony = HarmonyInstance.Create("Nonnonstop.SnowWhiteWitheredVine");
                var assembly = Assembly.GetExecutingAssembly();
                harmony.PatchAll(assembly);
            }
            catch (Exception ex)
            {
                File.WriteAllText(Application.dataPath + "/BaseMods/error_SnowWhiteWitheredVine.txt", ex.Message);
            }
        }

        [HarmonyPatch(typeof(SnowWhite), "OnSuppressed")]
        private class SnowWhite_OnSuppressed_Patch
        {
            public static void Postfix(SnowWhite __instance)
            {
                __instance.infectInfos = new Dictionary<PassageObjectModel, SnowWhite.PassageInfectInfo>();
                foreach (var vine in __instance.vineList)
                {
                    Notice.instance.Send(NoticeName.RemoveEtcUnit, new object[] { vine });
                }
                __instance.vineList = new List<SnowWhite.VineArea>();
            }
        }
    }
}
