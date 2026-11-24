// Human-created

using System;
using System.Runtime.InteropServices;

#if VA_DOUBLE_PRECISION
using VAFloat = System.Double;
#else
using VAFloat = System.Single;
#endif

using GainFormulaFunc = System.Func<bool, int, int, int, float, float, float>;

namespace vaudionativewrapper.managed
{
    /// <summary>Emitters cast rays and can be discovered by other emitters</summary>
    public unsafe class Emitter
    {
        public IntPtr native;

#if DEBUG
        private readonly bool owns;
        string stackTrace;
#endif

#region Functions
        public Emitter(IntPtr native)
        {
            this.native = native;

#if DEBUG
            stackTrace = Environment.StackTrace;
#endif
        }

        /// <summary>Create a new Emitter with default settings</summary>
        public Emitter()
        {
            native = EmitterBindings.Create();

#if DEBUG
            owns = true;
            stackTrace = Environment.StackTrace;
#endif
        }

        /// <summary>Free the emitter. Throws if the emitter is still added to a world.</summary>
        public void Destroy()
        {
            EmitterBindings.Destroy(native).ThrowIfError();
            RemoveCallbacks(native);
            native = IntPtr.Zero;
        }

#if DEBUG
        ~Emitter()
        {
            if (owns && native != IntPtr.Zero)
            {
                string name;
                try { name = Name; } catch { name = "<unknown>"; }

                LogSettings.Warn($"Emitter '{name}' was garbage collected without calling Destroy() first. Stack trace: {stackTrace}");
            }
        }
#endif

        /// <summary>Add a target emitter. This emitter will calculate how muffled the target emitter is.</summary>
        public void AddTarget(Emitter target) => EmitterBindings.AddTarget(native, target.native);

        /// <summary>Remove a target emitter</summary>
        public void RemoveTarget(Emitter target) => EmitterBindings.RemoveTarget(native, target.native).ThrowIfError();

        /// <summary>Returns whether the provided emitter is a target of this emitter</summary>
        public bool HasTarget(Emitter target) => EmitterBindings.HasTarget(native, target.native).ToBool();

        /// <summary>Returns whether a target emitter has been raytraced. If true, it is safe to check the target's filter via GetTargetFilter</summary>
        public bool HasRaytracedTarget(Emitter target) => EmitterBindings.HasRaytracedTarget(native, target.native).ToBool();

        /// <summary>
        /// Get the low- and high-frequency gains for the target emitter.<br/>
        /// Only access when HasRaytracedTarget(Emitter) returns true.
        /// </summary>
        public LowPassFilter* GetTargetFilter(Emitter target)
        {
            EmitterBindings.GetTargetFilter(native, target.native, out var filter).ThrowIfError();
            return filter;
        }

        /// <summary>Clear the ray cache, causing all rays to be re-cast.</summary>
        public void ResetTrails() => EmitterBindings.ResetTrails(native).ThrowIfError();
#endregion

#region Properties
        /// <summary>World-space position. Can be a Vector, FuncPosition, or any class that inherits from IPosition</summary>
        public Vector Position
        {
            get => EmitterBindings.GetPosition(native);
            set => EmitterBindings.SetPosition(native, value).ThrowIfError();
        }

        /// <summary>True if this emitter hasn't cast its own reverb and ambient rays yet.</summary>
        public bool Initialising
        {
            get => EmitterBindings.GetInitialising(native);
        }

        /// <summary>Whether this emitter will be removed from the world when its reverb tail finishes playing. While true, changing this emitter's properties (excluding Name, UserData, LogCallback, LogErrorCallback, OnRemoved and KeepReverbTailAlive) or adding targets throws an InvalidOperationException.</summary>
        public bool PendingRemoval
        {
            get => EmitterBindings.GetPendingRemoval(native);
        }

        /// <summary>
        /// Suspended emitters will not re-cast their rays, but will still be raytraced by other emitters as muffling targets.<br/>
        /// When un-suspended, all of its rays are re-cast, and it can join a cluster again.
        /// </summary>
        public bool Suspended
        {
            get => EmitterBindings.GetSuspended(native);
            set => EmitterBindings.SetSuspended(native, value).ThrowIfError();
        }

        /// <summary>Whether this emitter's EAX is blended into the World.GroupedEAX list. Set this to false for listener emitters</summary>
        public bool AffectsGroupedEAX
        {
            get => EmitterBindings.GetAffectsGroupedEAX(native);
            set => EmitterBindings.SetAffectsGroupedEAX(native, value).ThrowIfError();
        }

        /// <summary>Whether this emitter is kept alive while its reverb tail continues to play. Setting this to false while PendingRemoval is true removes the emitter on the next World.Update.</summary>
        public bool KeepReverbTailAlive
        {
            get => EmitterBindings.GetKeepReverbTailAlive(native);
            set => EmitterBindings.SetKeepReverbTailAlive(native, value).ThrowIfError();
        }

        /// <summary>Whether reverb gain and direction will be calculated relative to this emitter. Set to true for your main listener emitter.</summary>
        public bool HasRelativeReverb
        {
            get => EmitterBindings.GetHasRelativeReverb(native);
            set => EmitterBindings.SetHasRelativeReverb(native, value).ThrowIfError();
        }

        /// <summary>
        /// Reverb directionality is calculated in two stages:<br/>
        /// - First the average direction from this emitter to all of another emitter's reverb bounce positions is calculated. This direction is called the 'directional reverb vector'.<br/>
        /// - Second this vector is normalised using the RelativeReverbInnerThreshold and RelativeReverbOuterThreshold bounds.<br/>
        /// For reference, a vector of (0, 0, 0) has no direction, so you'll hear the reverb all around you. A vector of (0.5f, 0, 0) has reverb mostly coming from the right.<br/>
        /// If the magnitude of the directional reverb vector is lower than RelativeReverbInnerThreshold, reverb will not be directional (sounds like the reverb is all around you).<br/>
        /// Reduce this value to make it easier to hear directional reverb.<br/>
        /// Increase this value to make it harder to hear directional reverb.
        /// </summary>
        public float RelativeReverbInnerThreshold
        {
            get => EmitterBindings.GetRelativeReverbInnerThreshold(native);
            set => EmitterBindings.SetRelativeReverbInnerThreshold(native, value).ThrowIfError();
        }

        /// <summary>
        /// Reverb directionality is calculated in two stages:<br/>
        /// - First the average direction from this emitter to all of another emitter's reverb bounce positions is calculated. This direction is called the 'directional reverb vector'.<br/>
        /// - Second this vector is normalised using the RelativeReverbInnerThreshold and RelativeReverbOuterThreshold bounds.<br/>
        /// For reference, a vector of (0, 0, 0) has no direction, so you'll hear the reverb all around you. A vector of (0.5f, 0, 0) has reverb mostly coming from the right.<br/>
        /// If the magnitude of the directional reverb vector is higher than RelativeReverbOuterThreshold, reverb will be fully directional.<br/>
        /// Reduce this value to make it easier to hear directional reverb.<br/>
        /// Increase this value to make it harder to hear directional reverb.
        /// </summary>
        public float RelativeReverbOuterThreshold
        {
            get => EmitterBindings.GetRelativeReverbOuterThreshold(native);
            set => EmitterBindings.SetRelativeReverbOuterThreshold(native, value).ThrowIfError();
        }

        /// <summary>
        /// Whether this emitter can share its rays with other nearby emitters that also have clustering enabled.<br/>
        /// A cluster casts its reverb, ambient occlusion, ambient permeation, occlusion and permeation rays outwards from each emitter in the cluster in round robin, and each emitter shares the same reverb, EAX and ambience results.<br/>
        /// The cluster uses the highest ray count, bounce count and MaxEchogramTime of its emitters. Its occlusion and permeation rays are cast towards the targets of every emitter in the cluster, and each emitter only gets low pass filters for its own targets.<br/>
        /// Emitters only cluster with others that are within line of sight and have the same EchogramGranularity, and never with their own targets. Emitters with OverridePositions or OverrideRayDirections set never cluster.
        /// </summary>
        public bool ClusteringEnabled
        {
            get => EmitterBindings.GetClusteringEnabled(native);
            set => EmitterBindings.SetClusteringEnabled(native, value).ThrowIfError();
        }

        /// <summary>The maximum distance between this emitter and the centre of a cluster, for it to share that cluster's rays. If two emitters have different distances, the smaller one is used. Only used if ClusteringEnabled is true.</summary>
        public VAFloat ClusteringDistance
        {
            get => EmitterBindings.GetClusteringDistance(native);
            set => EmitterBindings.SetClusteringDistance(native, value).ThrowIfError();
        }

        /// <summary>
        /// Whether this emitter groups its targets into clusters, to reduce the number of occlusion and permeation rays it casts.<br/>
        /// Within each cluster, this emitter casts occlusion and permeation rays towards the targets in the cluster in round robin, and each target gets the same low pass filter.<br/>
        /// This is set on the emitter that casts the rays (e.g. the listener), not on its targets. For sharing rays between emitters, see ClusteringEnabled.<br/>
        /// If this emitter is in a cluster with other emitters that have targets, its targets are only clustered if all of them have this enabled, using the smallest TargetClusteringDistance.
        /// </summary>
        public bool TargetClusteringEnabled
        {
            get => EmitterBindings.GetTargetClusteringEnabled(native);
            set => EmitterBindings.SetTargetClusteringEnabled(native, value).ThrowIfError();
        }

        /// <summary>The maximum distance between two of this emitter's targets for them to be in the same cluster. Only used if TargetClusteringEnabled is true.</summary>
        public VAFloat TargetClusteringDistance
        {
            get => EmitterBindings.GetTargetClusteringDistance(native);
            set => EmitterBindings.SetTargetClusteringDistance(native, value).ThrowIfError();
        }

        /// <summary>Whether to clamp this emitter's position within the world bounds.</summary>
        public bool ClampPosition
        {
            get => EmitterBindings.GetClampPosition(native);
            set => EmitterBindings.SetClampPosition(native, value).ThrowIfError();
        }

        /// <summary>This name is displayed in the debug window. Setting null stores an empty string.</summary>
        public string Name
        {
            get => EmitterBindings.GetName(native);
            set => EmitterBindings.SetName(native, value).ThrowIfError();
        }

        /// <summary>User-defined type for this emitter</summary>
        public int Type
        {
            get => EmitterBindings.GetType(native);
            set => EmitterBindings.SetType(native, value).ThrowIfError();
        }

        /// <summary>Custom user data.</summary>
        public IntPtr UserData
        {
            get => EmitterBindings.GetUserData(native);
            set => EmitterBindings.SetUserData(native, value).ThrowIfError();
        }

        /// <summary>Number of reverb rays to cast</summary>
        public int ReverbRayCount
        {
            get => EmitterBindings.GetReverbRayCount(native);
            set => EmitterBindings.SetReverbRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per reverb ray</summary>
        public int ReverbBounceCount
        {
            get => EmitterBindings.GetReverbBounceCount(native);
            set => EmitterBindings.SetReverbBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of occlusion rays to cast</summary>
        public int OcclusionRayCount
        {
            get => EmitterBindings.GetOcclusionRayCount(native);
            set => EmitterBindings.SetOcclusionRayCount(native, value).ThrowIfError();
        }

        /// <summary>Maximum number of bounces per occlusion ray</summary>
        public int OcclusionBounceCount
        {
            get => EmitterBindings.GetOcclusionBounceCount(native);
            set => EmitterBindings.SetOcclusionBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of permeation rays to cast</summary>
        public int PermeationRayCount
        {
            get => EmitterBindings.GetPermeationRayCount(native);
            set => EmitterBindings.SetPermeationRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per permeation ray</summary>
        public int PermeationBounceCount
        {
            get => EmitterBindings.GetPermeationBounceCount(native);
            set => EmitterBindings.SetPermeationBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of ambient occlusion rays to cast</summary>
        public int AmbientOcclusionRayCount
        {
            get => EmitterBindings.GetAmbientOcclusionRayCount(native);
            set => EmitterBindings.SetAmbientOcclusionRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per ambient occlusion ray</summary>
        public int AmbientOcclusionBounceCount
        {
            get => EmitterBindings.GetAmbientOcclusionBounceCount(native);
            set => EmitterBindings.SetAmbientOcclusionBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of ambient permeation rays to cast</summary>
        public int AmbientPermeationRayCount
        {
            get => EmitterBindings.GetAmbientPermeationRayCount(native);
            set => EmitterBindings.SetAmbientPermeationRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per ambient permeation ray</summary>
        public int AmbientPermeationBounceCount
        {
            get => EmitterBindings.GetAmbientPermeationBounceCount(native);
            set => EmitterBindings.SetAmbientPermeationBounceCount(native, value).ThrowIfError();
        }

        /// <summary>Number of visualisation rays to cast</summary>
        public int VisualisationRayCount
        {
            get => EmitterBindings.GetVisualisationRayCount(native);
            set => EmitterBindings.SetVisualisationRayCount(native, value).ThrowIfError();
        }

        /// <summary>Number of bounces per visualisation ray</summary>
        public int VisualisationBounceCount
        {
            get => EmitterBindings.GetVisualisationBounceCount(native);
            set => EmitterBindings.SetVisualisationBounceCount(native, value).ThrowIfError();
        }

        /// <summary>How often (in milliseconds) to cast visualisation rays</summary>
        public int VisualisationUpdateFrequency
        {
            get => EmitterBindings.GetVisualisationUpdateFrequency(native);
            set => EmitterBindings.SetVisualisationUpdateFrequency(native, value).ThrowIfError();
        }

        /// <summary>How long (in milliseconds) the echogram records data for. Returning reverb rays after this period will be ignored</summary>
        public int MaxEchogramTime
        {
            get => EmitterBindings.GetMaxEchogramTime(native);
            set => EmitterBindings.SetMaxEchogramTime(native, value).ThrowIfError();
        }

        /// <summary>The length (in milliseconds) of each entry in the echogram</summary>
        public int EchogramGranularity
        {
            get => EmitterBindings.GetEchogramGranularity(native);
            set => EmitterBindings.SetEchogramGranularity(native, value).ThrowIfError();
        }

        /// <summary>
        /// Controls the number of trails that are 'refreshed' each frame.<br/>
        /// Refreshing a trail involves re-casting the first ray, and if it hits a different position than last time, the entire trail will be trimmed and recalculated.<br/>
        /// RefreshDistanceThreshold controls the allowed distance between new and old bounce positions.
        /// </summary>
        public int TrailRefreshCount
        {
            get => EmitterBindings.GetTrailRefreshCount(native);
            set => EmitterBindings.SetTrailRefreshCount(native, value).ThrowIfError();
        }

        /// <summary>The allowed distance between new and old bounce positions when refreshing trails. See TrailRefreshCount for more information.</summary>
        public VAFloat RefreshDistanceThreshold
        {
            get => EmitterBindings.GetRefreshDistanceThreshold(native);
            set => EmitterBindings.SetRefreshDistanceThreshold(native, value).ThrowIfError();
        }

        /// <summary>The percentage of returning energy required for reverb to be at full volume.</summary>
        public float ReverbEnergyCap
        {
            get => EmitterBindings.GetReverbEnergyCap(native);
            set => EmitterBindings.SetReverbEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of occlusion energy required for this emitter to be at full volume. Defaults to 15% of the other emitter's OcclusionRayCount. This field does nothing if a custom GainFormula is provided.</summary>
        public float OcclusionEnergyCap
        {
            get => EmitterBindings.GetOcclusionEnergyCap(native);
            set => EmitterBindings.SetOcclusionEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>
        /// The percentage of permeation energy required for this emitter to be at full volume. Defaults to 15% of the other emitter's PermeationRayCount * PermeationBounceCount.<br/>
        /// This field does nothing if a custom GainFormula is provided.
        /// </summary>
        public float PermeationEnergyCap
        {
            get => EmitterBindings.GetPermeationEnergyCap(native);
            set => EmitterBindings.SetPermeationEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of occlusion energy required for ambience to be at full volume.</summary>
        public float AmbientOcclusionEnergyCap
        {
            get => EmitterBindings.GetAmbientOcclusionEnergyCap(native);
            set => EmitterBindings.SetAmbientOcclusionEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The percentage of permeation energy required for ambience to be at full volume.</summary>
        public float AmbientPermeationEnergyCap
        {
            get => EmitterBindings.GetAmbientPermeationEnergyCap(native);
            set => EmitterBindings.SetAmbientPermeationEnergyCap(native, value).ThrowIfError();
        }

        /// <summary>The loudest linear volume (0–1) this emitter's dry source will ever be played at by the consuming application. Used to estimate how long the emitter's reverb tail stays audible in EAXUtils.GetEffectiveTailSeconds - a quieter source reaches an inaudible reverb tail sooner.</summary>
        public float MaxVolume
        {
            get => EmitterBindings.GetMaxVolume(native);
            set => EmitterBindings.SetMaxVolume(native, value).ThrowIfError();
        }

        /// <summary>Optimisation field - permeation rays will stop bouncing when their energy drops below this threshold</summary>
        public float MinimumPermeationEnergy
        {
            get => EmitterBindings.GetMinimumPermeationEnergy(native);
            set => EmitterBindings.SetMinimumPermeationEnergy(native, value).ThrowIfError();
        }

        /// <summary>Optimisation field - reverb rays will stop bouncing when their energy drops below this threshold</summary>
        public float MinimumReverbEnergy
        {
            get => EmitterBindings.GetMinimumReverbEnergy(native);
            set => EmitterBindings.SetMinimumReverbEnergy(native, value).ThrowIfError();
        }

        /// <summary>Optimisation field - occlusion rays will stop bouncing when their low-frequency energy drops below this threshold</summary>
        public float MinimumOcclusionEnergy
        {
            get => EmitterBindings.GetMinimumOcclusionEnergy(native);
            set => EmitterBindings.SetMinimumOcclusionEnergy(native, value).ThrowIfError();
        }

        /// <summary>Optimisation field - ambient permeation rays will stop permeating when their energy drops below this threshold</summary>
        public float MinimumAmbientPermeationEnergy
        {
            get => EmitterBindings.GetMinimumAmbientPermeationEnergy(native);
            set => EmitterBindings.SetMinimumAmbientPermeationEnergy(native, value).ThrowIfError();
        }

        /// <summary>Optimisation field - ambient occlusion rays will stop bouncing when their low-frequency energy drops below this threshold</summary>
        public float MinimumAmbientOcclusionEnergy
        {
            get => EmitterBindings.GetMinimumAmbientOcclusionEnergy(native);
            set => EmitterBindings.SetMinimumAmbientOcclusionEnergy(native, value).ThrowIfError();
        }

        /// <summary>
        /// Seed used to randomise scattering vectors.<br/>
        /// Warning: changing this at runtime will clear the ray cache.
        /// </summary>
        public int ScatteringSeed
        {
            get => EmitterBindings.GetScatteringSeed(native);
            set => EmitterBindings.SetScatteringSeed(native, value).ThrowIfError();
        }

        /// <summary>Seed used to randomise visualisation rays. If null, a random visualisation seed is generated each time</summary>
        public int? VisualisationSeed
        {
            get => EmitterBindings.GetVisualisationSeed(native, out var value) ? value : (int?)null;
            set => (value.HasValue ? EmitterBindings.SetVisualisationSeed(native, value.Value) : EmitterBindings.ClearVisualisationSeed(native)).ThrowIfError();
        }

        /// <summary>Whether to render each trail a different color in the debug window (dev build only)</summary>
        public bool RandomTrailColor
        {
            get => EmitterBindings.GetRandomTrailColor(native);
            set => EmitterBindings.SetRandomTrailColor(native, value).ThrowIfError();
        }

        /// <summary>The color of ray trails in the debug window (dev build only)</summary>
        public Color TrailColor
        {
            get => EmitterBindings.GetTrailColor(native);
            set => EmitterBindings.SetTrailColor(native, value).ThrowIfError();
        }

        /// <summary>The color of reverb rays in the debug window (dev build only)</summary>
        public Color ReverbColor
        {
            get => EmitterBindings.GetReverbColor(native);
            set => EmitterBindings.SetReverbColor(native, value).ThrowIfError();
        }

        /// <summary>The color of occlusion rays in the debug window (dev build only)</summary>
        public Color OcclusionColor
        {
            get => EmitterBindings.GetOcclusionColor(native);
            set => EmitterBindings.SetOcclusionColor(native, value).ThrowIfError();
        }

        /// <summary>The color of permeation rays in the debug window (dev build only)</summary>
        public Color PermeationColor
        {
            get => EmitterBindings.GetPermeationColor(native);
            set => EmitterBindings.SetPermeationColor(native, value).ThrowIfError();
        }

        /// <summary>The color of ambient permeation rays in the debug window (dev build only)</summary>
        public Color AmbientPermeationColor
        {
            get => EmitterBindings.GetAmbientPermeationColor(native);
            set => EmitterBindings.SetAmbientPermeationColor(native, value).ThrowIfError();
        }
#endregion

#region ReadOnly
        /// <summary>
        /// Contains EAX reverb properties. This object is null until this emitter casts its reverb rays for the first time.<br/>
        /// See Initialising and OnRaytracingComplete
        /// </summary>
        public EAXReverb EAX => new EAXReverb(EmitterBindings.GetEAX(native));
        /// <summary>
        /// Contains data gathered by reverb rays. This object is null until this emitter casts its reverb rays for the first time.<br/>
        /// See Initialising and OnRaytracingComplete
        /// </summary>
        public ProcessedReverb ProcessedReverb => new ProcessedReverb(EmitterBindings.GetProcessedReverb(native));
        /// <summary>
        /// Contains the low- and high-frequency volume of ambient sounds. This object is null until this emitter casts its ambient rays for the first time, and null if neither ambient occlusion nor ambient permeation is enabled.<br/>
        /// See Initialising and OnRaytracingComplete
        /// </summary>
        public LowPassFilter* AmbientFilter => EmitterBindings.GetAmbientFilter(native);

        /// <summary>Whether this emitter casts any rays (excluding visualisation rays)</summary>
        public bool CreatesTrails => EmitterBindings.CreatesTrails(native);
        /// <summary>
        /// Whether this emitter is within World.Position and World.MaxBounds.<br/>
        /// Emitters outside the world bounds will not be raytraced. Set ClampPosition to true to keep this emitter within the world bounds.<br/>
        /// If ClampPosition is false and any of the OverridePositions are outside the world bounds, this emitter is also outside the world bounds.
        /// </summary>
        public bool WithinWorldBounds => EmitterBindings.WithinWorldBounds(native);
        /// <summary>The index of this emitter's EAX object in World.GroupedEAX</summary>
        public int GroupedEAXIndex => EmitterBindings.GetGroupedEAXIndex(native);
        /// <summary>The percentage of ambient occlusion energy that reached the edge of the world.</summary>
        public float OutsidePercent => EmitterBindings.GetOutsidePercent(native);

        /// <summary>The number of trails that will be created for this emitter</summary>
        public int TrailCount => EmitterBindings.GetTrailCount(native);
        /// <summary>The number of bounces per trail</summary>
        public int TrailBounceCount => EmitterBindings.GetTrailBounceCount(native);

        /// <summary>True if both ReverbRayCount and ReverbBounceCount are greater than zero</summary>
        public bool ReverbEnabled => EmitterBindings.ReverbEnabled(native);
        /// <summary>True if both OcclusionRayCount and OcclusionBounceCount are greater than zero</summary>
        public bool OcclusionEnabled => EmitterBindings.OcclusionEnabled(native);
        /// <summary>True if both PermeationRayCount and PermeationBounceCount are greater than zero</summary>
        public bool PermeationEnabled => EmitterBindings.PermeationEnabled(native);
        /// <summary>True if both AmbientOcclusionRayCount and AmbientOcclusionBounceCount are greater than zero</summary>
        public bool AmbientOcclusionEnabled => EmitterBindings.AmbientOcclusionEnabled(native);
        /// <summary>True if both AmbientPermeationRayCount and AmbientPermeationBounceCount are greater than zero</summary>
        public bool AmbientPermeationEnabled => EmitterBindings.AmbientPermeationEnabled(native);
        /// <summary>True if both VisualisationRayCount and VisualisationBounceCount are greater than zero, and VisualisationCallback is assigned</summary>
        public bool VisualisationEnabled => EmitterBindings.VisualisationEnabled(native);
#endregion

#region WriteOnly
        /// <summary>
        /// When defined, rays will be cast in round-robin from these positions rather than the default Position.<br/>
        /// If any position is outside the world bounds and ClampPosition is true, they are clamped within the world bounds. If ClampPosition is false, a warning is logged and this emitter is treated as outside the world, see WithinWorldBounds.<br/>
        /// Set to null or an empty array to revert back to using the default Position.<br/>
        /// A copy of the provided array will be stored.<br/>
        /// Warning: changing this at runtime will clear the ray cache.
        /// </summary>
        public Vector[] OverridePositions
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverridePositions(native, ptr, value?.Length ?? 0).ThrowIfError();
                }
            }
        }

        /// <summary>
        /// When defined, rays will be cast in these directions rather than the default ray directions.<br/>
        /// Set to null or an empty array to revert back to using the default directions.<br/>
        /// A copy of the provided array will be stored.<br/>
        /// Warning: changing this at runtime will clear the ray cache.
        /// </summary>
        public Vector[] OverrideRayDirections
        {
            set
            {
                fixed (Vector* ptr = value)
                {
                    EmitterBindings.SetOverrideRayDirections(native, ptr, value?.Length ?? 0).ThrowIfError();
                }
            }
        }
#endregion

#region Callbacks
        // Handlers are keyed by the emitter's native pointer, see CallbackTable
        static readonly CallbackTable<Action> onRaytracingCompleteHandlers = new CallbackTable<Action>();
        static readonly CallbackTable<Action<Emitter>> onRaytracedByAnotherEmitterHandlers = new CallbackTable<Action<Emitter>>();
        static readonly CallbackTable<Action> onRemovedHandlers = new CallbackTable<Action>();
        static readonly CallbackTable<Action<VisualisationData[]>> visualisationHandlers = new CallbackTable<Action<VisualisationData[]>>();
        static readonly CallbackTable<Action<string>> logHandlers = new CallbackTable<Action<string>>();
        static readonly CallbackTable<Action<string>> logErrorHandlers = new CallbackTable<Action<string>>();
        static readonly CallbackTable<GainFormulaFunc> gainFormulaHandlers = new CallbackTable<GainFormulaFunc>();
        static readonly CallbackTable<GainFormulaFunc> ambientGainFormulaHandlers = new CallbackTable<GainFormulaFunc>();

        static readonly OnRaytracingCompleteFn onRaytracingCompleteNative = OnRaytracingCompleteNative;
        static readonly OnRaytracedByAnotherEmitterFn onRaytracedByAnotherEmitterNative = OnRaytracedByAnotherEmitterNative;
        static readonly OnRemovedFn onRemovedNative = OnRemovedNative;
        static readonly VisualisationCallbackFn visualisationNative = VisualisationNative;
        static readonly LogCallbackFn logNative = LogNative;
        static readonly LogCallbackFn logErrorNative = LogErrorNative;
        static readonly GainFormulaDelegate gainFormulaNative = GainFormulaNative;
        static readonly GainFormulaDelegate ambientGainFormulaNative = AmbientGainFormulaNative;

        [AOT.MonoPInvokeCallback(typeof(OnRaytracingCompleteFn))]
        static void OnRaytracingCompleteNative(IntPtr emitter) => onRaytracingCompleteHandlers.Get(emitter)?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(OnRaytracedByAnotherEmitterFn))]
        static void OnRaytracedByAnotherEmitterNative(IntPtr source, IntPtr target) => onRaytracedByAnotherEmitterHandlers.Get(target)?.Invoke(source == IntPtr.Zero ? null : new Emitter(source));

        // Native clears its callback once invoked, and the user may destroy the emitter inside it
        [AOT.MonoPInvokeCallback(typeof(OnRemovedFn))]
        static void OnRemovedNative(IntPtr emitter) => onRemovedHandlers.Take(emitter)?.Invoke();

        [AOT.MonoPInvokeCallback(typeof(VisualisationCallbackFn))]
        static void VisualisationNative(IntPtr emitter, VisualisationData* data, int count)
        {
            var handler = visualisationHandlers.Get(emitter);

            if (handler == null)
                return;

            var arr = new VisualisationData[count];

            for (int i = 0; i < count; i++)
                arr[i] = new VisualisationData { position = data[i].position, normal = data[i].normal };

            handler.Invoke(arr);
        }

        [AOT.MonoPInvokeCallback(typeof(LogCallbackFn))]
        static void LogNative(IntPtr emitter, string message) => logHandlers.Get(emitter)?.Invoke(message);

        [AOT.MonoPInvokeCallback(typeof(LogCallbackFn))]
        static void LogErrorNative(IntPtr emitter, string message) => logErrorHandlers.Get(emitter)?.Invoke(message);

        // Invoked on the raytracing threads. The handler is only missing if native raced a clear, so fall back to full volume
        [AOT.MonoPInvokeCallback(typeof(GainFormulaDelegate))]
        static float GainFormulaNative(IntPtr emitter, bool lowFrequency, int occlusionRayCount, int permeationRayCount, int permeationBounceCount, float occlusionEnergy, float permeationEnergy) =>
            gainFormulaHandlers.Get(emitter)?.Invoke(lowFrequency, occlusionRayCount, permeationRayCount, permeationBounceCount, occlusionEnergy, permeationEnergy) ?? 1;

        [AOT.MonoPInvokeCallback(typeof(GainFormulaDelegate))]
        static float AmbientGainFormulaNative(IntPtr emitter, bool lowFrequency, int occlusionRayCount, int permeationRayCount, int permeationBounceCount, float occlusionEnergy, float permeationEnergy) =>
            ambientGainFormulaHandlers.Get(emitter)?.Invoke(lowFrequency, occlusionRayCount, permeationRayCount, permeationBounceCount, occlusionEnergy, permeationEnergy) ?? 1;

        // Native can no longer invoke this emitter's callbacks
        static void RemoveCallbacks(IntPtr emitter)
        {
            onRaytracingCompleteHandlers.Remove(emitter);
            onRaytracedByAnotherEmitterHandlers.Remove(emitter);
            onRemovedHandlers.Remove(emitter);
            visualisationHandlers.Remove(emitter);
            logHandlers.Remove(emitter);
            logErrorHandlers.Remove(emitter);
            gainFormulaHandlers.Remove(emitter);
            ambientGainFormulaHandlers.Remove(emitter);
        }

        // Native validates a new formula by invoking it, so its handler is registered first and the previous one restored if native rejects it. A cleared formula keeps its handler until the emitter is destroyed, as the raytracing threads may still be invoking it
        void SetGainFormula(CallbackTable<GainFormulaFunc> handlers, GainFormulaFunc value, Func<IntPtr, GainFormulaDelegate, VAResult> set, GainFormulaDelegate trampoline)
        {
            if (value == null)
            {
                set(native, null).ThrowIfError();
                return;
            }

            var previous = handlers.Get(native);
            handlers.Set(native, value);

            try
            {
                set(native, trampoline).ThrowIfError();
            }
            catch
            {
                handlers.Set(native, previous);
                throw;
            }
        }

        /// <summary>This callback is invoked when this emitter has cast its reverb and ambient rays for the first time.</summary>
        public Action OnRaytracingComplete
        {
            set => EmitterBindings.SetOnRaytracingCompleteCallback(native, onRaytracingCompleteHandlers.Set(native, value) ? onRaytracingCompleteNative : null).ThrowIfError();
        }

        /// <summary>
        /// This callback is invoked when another emitter raytraces this emitter for the first time.<br/>
        /// The first argument is the other emitter that raytraced this emitter.
        /// </summary>
        public Action<Emitter> OnRaytracedByAnotherEmitter
        {
            set => EmitterBindings.SetOnRaytracedByAnotherEmitterCallback(native, onRaytracedByAnotherEmitterHandlers.Set(native, value) ? onRaytracedByAnotherEmitterNative : null).ThrowIfError();
        }

        /// <summary>
        /// This callback is invoked once this emitter has been removed from the World and the raytracing threads no longer read it.<br/>
        /// It's invoked inside World.Update or World.Wait once the next raytracing results are handled, inside World.RemoveEmitter if no raytracing results are pending, or inside World.Dispose. If it casts reverb rays and AffectsGroupedEAX is true, removal itself waits until its reverb tail finishes playing, and during that time PendingRemoval is set to true.<br/>
        /// The callback is cleared once invoked. If the emitter is re-added to the same world before then, it isn't invoked, and is kept for the next removal.
        /// </summary>
        public Action OnRemoved
        {
            set => EmitterBindings.SetOnRemovedCallback(native, onRemovedHandlers.Set(native, value) ? onRemovedNative : null).ThrowIfError();
        }

        /// <summary>This callback is invoked with the position and normal of each bounce of each visualisation ray. Do not modify the array or access it outside this callback.</summary>
        public Action<VisualisationData[]> VisualisationCallback
        {
            set => EmitterBindings.SetVisualisationCallback(native, visualisationHandlers.Set(native, value) ? visualisationNative : null).ThrowIfError();
        }

        /// <summary>
        /// A custom formula that calculates how muffled this emitter is, relative to another emitter.<br/>
        /// This is invoked for both low-frequency and high-frequency energy and the values it returns are stored in the other emitter's low pass filter, accessed via GetTargetFilter.<br/>
        /// Set to null to use the default formula.<br/>
        /// This formula is invoked on background threads. Do not attempt to access data from the main thread within this function.<br/>
        /// Parameters: (bool lowFrequency, int occlusionRayCount, int permeationRayCount, int permeationBounceCount, float occlusionEnergy, float permeationEnergy)<br/>
        /// Returns: float between 0.0f and 1.0f (inclusive)
        /// </summary>
        public GainFormulaFunc GainFormula
        {
            set => SetGainFormula(gainFormulaHandlers, value, EmitterBindings.SetGainFormula, gainFormulaNative);
        }

        /// <summary>
        /// A custom formula that calculates how muffled ambience is relative to this emitter.<br/>
        /// This is invoked for both low-frequency and high-frequency energy and the values it returns are stored in this emitter's AmbientFilter.<br/>
        /// Set to null to use the default formula.<br/>
        /// This formula is invoked on the main thread.<br/>
        /// Parameters: (bool lowFrequency, int ambientOcclusionRayCount, int ambientPermeationRayCount, int ambientPermeationBounceCount, float ambientOcclusionEnergy, float ambientPermeationEnergy)<br/>
        /// Returns: float between 0.0f and 1.0f (inclusive)
        /// </summary>
        public GainFormulaFunc AmbientGainFormula
        {
            set => SetGainFormula(ambientGainFormulaHandlers, value, EmitterBindings.SetAmbientGainFormula, ambientGainFormulaNative);
        }

        /// <summary>A custom log callback. Defaults to Console.WriteLine()</summary>
        public Action<string> LogCallback
        {
            set => EmitterBindings.SetLogCallback(native, logHandlers.Set(native, value) ? logNative : null).ThrowIfError();
        }

        /// <summary>A custom error log callback. Defaults to Console.Error.WriteLine()</summary>
        public Action<string> LogErrorCallback
        {
            set => EmitterBindings.SetLogErrorCallback(native, logErrorHandlers.Set(native, value) ? logErrorNative : null).ThrowIfError();
        }
#endregion
    }
}