// Minimal compile-time stub of the RimWorld/Verse API surface we bind to.
// This is NOT shipped. At runtime these type references resolve to the real
// Assembly-CSharp loaded by RimWorld. Only ultra-stable members are declared;
// volatile per-tile fields are read via reflection in the worker.
namespace Verse {
    public class DefModExtension { }
    public class Def {
        public T GetModExtension<T>() where T : DefModExtension { return null; }
    }
    public static class Rand {
        public static float Value { get { return 0f; } }
    }
}
namespace RimWorld {
    public class BiomeDef : Verse.Def { }
}
namespace RimWorld.Planet {
    public class Tile { }
    public struct PlanetTile { }
    public abstract class BiomeWorker {
        public virtual float GetScore(RimWorld.BiomeDef biome, Tile tile, PlanetTile planetTile) { return 0f; }
    }
}
