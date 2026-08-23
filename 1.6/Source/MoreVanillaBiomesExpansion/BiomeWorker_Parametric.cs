using System;
using System.Reflection;
using RimWorld;
using RimWorld.Planet;
using Verse;

namespace MoreVanillaBiomesExpansion
{
    // One generic worker for all expansion biomes. Placement thresholds come from
    // each biome's BiomeExtension. Per-tile climate values are read via cached
    // reflection so the code is immune to field/property or value/reference-type
    // differences in the game's Tile type across 1.6 builds.
    public class BiomeWorker_Parametric : BiomeWorker
    {
        private static bool resolved;
        private static MemberInfo mTemp, mRain, mElev, mSwamp, mHill, mWater;

        private static void Resolve(Type t)
        {
            mTemp  = Member(t, "temperature");
            mRain  = Member(t, "rainfall");
            mElev  = Member(t, "elevation");
            mSwamp = Member(t, "swampiness");
            mHill  = Member(t, "hilliness");
            mWater = Member(t, "WaterCovered");
            resolved = true;
        }

        private static MemberInfo Member(Type t, string name)
        {
            MemberInfo m = t.GetField(name, BindingFlags.Public | BindingFlags.Instance);
            if (m != null) return m;
            return t.GetProperty(name, BindingFlags.Public | BindingFlags.Instance);
        }

        private static object Read(MemberInfo m, object o)
        {
            if (m == null) return null;
            FieldInfo f = m as FieldInfo;
            if (f != null) return f.GetValue(o);
            return ((PropertyInfo)m).GetValue(o, null);
        }

        public override float GetScore(BiomeDef biome, Tile tile, PlanetTile planetTile)
        {
            BiomeExtension ext = biome.GetModExtension<BiomeExtension>();
            if (ext == null) return 0f;
            if (!resolved) Resolve(tile.GetType());

            object wv = Read(mWater, tile);
            bool water = wv != null && Convert.ToBoolean(wv);
            if (ext.needsWater) { if (!water) return -100f; }
            else { if (water) return -100f; }

            float temp  = ToF(Read(mTemp, tile));
            float rain  = ToF(Read(mRain, tile));
            float elev  = ToF(Read(mElev, tile));
            float swamp = ToF(Read(mSwamp, tile));
            int   hill  = ToI(Read(mHill, tile));

            if (temp  < ext.minTemp        || temp  > ext.maxTemp)        return 0f;
            if (rain  < ext.minRainfall    || rain  > ext.maxRainfall)    return 0f;
            if (elev  < ext.minElevation   || elev  > ext.maxElevation)   return 0f;
            if (swamp < ext.minSwampiness  || swamp > ext.maxSwampiness)  return 0f;
            if (hill  < ext.minHilliness   || hill  > ext.maxHilliness)   return 0f;
            if (ext.rejectChance > 0f && Rand.Value < ext.rejectChance)   return 0f;

            float s = ext.score;
            if (ext.tempWeight  != 0f) s += ext.tempWeight  * (temp - ext.tempPivot);
            if (ext.rainWeight  != 0f) s += ext.rainWeight  * (rain - ext.rainPivot);
            if (ext.elevWeight  != 0f) s += ext.elevWeight  * (elev - ext.elevPivot);
            if (ext.swampWeight != 0f) s += ext.swampWeight * swamp;
            if (ext.flatBonus   != 0f && hill == 1) s += ext.flatBonus;
            if (ext.hillsBonus  != 0f && (hill == 3 || hill == 4)) s += ext.hillsBonus;
            return s;
        }

        private static float ToF(object o) { return o == null ? 0f : Convert.ToSingle(o); }
        private static int   ToI(object o) { return o == null ? 0  : Convert.ToInt32(o); }
    }
}
