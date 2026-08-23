// Compile-time-only stub of the RimWorld/Verse API surface we bind to. NOT shipped.
// At runtime these references resolve to the real Assembly-CSharp loaded by RimWorld.
// NOTE: BiomeWorker lives in namespace RimWorld (not RimWorld.Planet); only Tile and
// PlanetTile are in RimWorld.Planet.
namespace Verse {
    public class DefModExtension { }
    public class Def {
        public T GetModExtension<T>() where T : DefModExtension { return null; }
    }
    public static class Rand {
        public static float Value { get { return 0f; } }
    }
}
namespace RimWorld.Planet {
    public class Tile { }
    public struct PlanetTile { }
}
namespace RimWorld {
    public class BiomeDef : Verse.Def { }
    public abstract class BiomeWorker {
        public virtual float GetScore(BiomeDef biome, RimWorld.Planet.Tile tile, RimWorld.Planet.PlanetTile planetTile) { return 0f; }
    }
}
