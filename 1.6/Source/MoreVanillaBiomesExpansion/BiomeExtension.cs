using Verse;

namespace MoreVanillaBiomesExpansion
{
    // Data-driven placement parameters, attached to a BiomeDef via <modExtensions>.
    // Read by BiomeWorker_Parametric to score world tiles, mirroring the tuning
    // approach of the base mod's hand-written BiomeWorker_* classes.
    public class BiomeExtension : DefModExtension
    {
        public bool needsWater = false;      // true = ocean/lake tiles only (like Sandbar/IceFloes)

        public float minTemp = -9999f;       // average temperature window (Celsius)
        public float maxTemp = 9999f;
        public float minRainfall = 0f;       // annual rainfall window (mm)
        public float maxRainfall = 1000000f;
        public float minElevation = -1000000f; // metres (negative = below sea level / water)
        public float maxElevation = 1000000f;
        public float minSwampiness = -1f;    // 0..1
        public float maxSwampiness = 9999f;
        public int minHilliness = 1;         // 1 Flat, 2 SmallHills, 3 LargeHills, 4 Mountainous, 5 Impassable
        public int maxHilliness = 5;

        public float rejectChance = 0f;      // 0..1 per-tile chance to skip (for rare "pocket" biomes)

        public float score = 13f;            // base score when in-range (higher wins the tile)

        // Optional linear tie-breakers so a biome peaks in the sweet spot of its niche.
        public float tempWeight = 0f;  public float tempPivot = 0f;
        public float rainWeight = 0f;  public float rainPivot = 0f;
        public float elevWeight = 0f;  public float elevPivot = 0f;
        public float swampWeight = 0f;
        public float flatBonus = 0f;         // added on Flat tiles
        public float hillsBonus = 0f;        // added on LargeHills/Mountainous tiles
    }
}
