# MoreVanillaBiomesExpansion — placement assembly

This small assembly (`MoreVanillaBiomesExpansion.dll`, shipped in `1.6/Assemblies/`)
provides ONE data-driven world-generation worker used by all 24 expansion biomes,
plus the `BiomeExtension` DefModExtension that feeds it per-biome placement
parameters from XML. It does not touch the original `VanillaBiomes.dll`.

Files:
- `BiomeExtension.cs`        – placement parameters (temp/rain/elevation/hilliness/etc.)
- `BiomeWorker_Parametric.cs`– generic BiomeWorker; reads a tile's climate via cached
                               reflection (immune to field/property differences) and
                               scores it against the biome's BiomeExtension.
- `AssemblyCsharpStub.cs`    – compile-time-only stub of the handful of RimWorld/Verse
                               API members used. NOT shipped. At runtime the references
                               resolve to the real Assembly-CSharp loaded by RimWorld.

## Rebuild (no game files or NuGet needed)
```
mcs -target:library -out:ref/Assembly-CSharp.dll AssemblyCsharpStub.cs
mcs -target:library -langversion:6 -r:ref/Assembly-CSharp.dll \
    -out:MoreVanillaBiomesExpansion.dll BiomeExtension.cs BiomeWorker_Parametric.cs
```
Or, in a normal modding setup, build against `Krafs.Rimworld.Ref` (net472) instead of
the stub — the source is unchanged.
