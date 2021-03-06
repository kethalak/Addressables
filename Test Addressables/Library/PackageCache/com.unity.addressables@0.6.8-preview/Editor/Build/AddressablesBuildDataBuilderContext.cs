﻿using System;
using System.Collections.Generic;
using UnityEditor.AddressableAssets.Settings;
using UnityEngine;

namespace UnityEditor.AddressableAssets.Build
{
    /// <summary>
    /// Data builder context object for Addressables.
    /// </summary>
    public class AddressablesBuildDataBuilderContext : IDataBuilderContext
    {
        /// <summary>
        /// Names of common context data objects.
        /// </summary>
        public static class BuildScriptContextConstants
        {
            public const string kAddressableAssetSettings = "AddressableAssetSettings";
            public const string kCurrentAssetGroup = "AddressableAssetGroup";
            public const string kAssetCatalog = "AssetCatalog";
            public const string kCatalogCatalog = "CatalogCatalog";
            public const string kDevMode = "DevMode";
            public const string kProfile = "Profile";
            public const string kBuildTargetGroup = "BuildTargetGroup";
            public const string kBuildTarget = "BuildTarget";
            public const string kPlayerBuildVersion = "PlayerBuildVersion";
            public const string kRuntimeSettingsFilename = "RuntimeSettingsFilename";
            public const string kRuntimeCatalogFilename = "RuntimeCatalogFilename";
        }

        Dictionary<string, object> m_Values = new Dictionary<string, object>();
        /// <summary>
        /// Collection of context keys.
        /// </summary>
        public ICollection<string> Keys { get { return m_Values.Keys; } }

        /// <summary>
        /// Construct a new AddressablesBuildDataBuilderContext object.
        /// </summary>
        public AddressablesBuildDataBuilderContext() { }

        /// <summary>
        /// Construct a new AddressablesBuildDataBuilderContext object, copying values from the one passed in.
        /// </summary>
        /// <param name="toCopy">The context to copy values from.</param>
        public AddressablesBuildDataBuilderContext(IDataBuilderContext toCopy)
        {
            foreach (var v in toCopy.Keys)
                m_Values.Add(v, toCopy.GetValue<object>(v));
        }

        /// <summary>
        /// Creates a default context object with values taken from the AddressableAssetSettings parameter.
        /// </summary>
        /// <param name="settings">The settings object to pull values from.</param>
        public AddressablesBuildDataBuilderContext(AddressableAssetSettings settings)
        {
            SetValue(BuildScriptContextConstants.kAddressableAssetSettings, settings);
            SetValue(BuildScriptContextConstants.kDevMode, true);
            SetValue(BuildScriptContextConstants.kProfile, ProjectConfigData.postProfilerEvents);
            SetValue(BuildScriptContextConstants.kBuildTargetGroup, BuildPipeline.GetBuildTargetGroup(EditorUserBuildSettings.activeBuildTarget));
            SetValue(BuildScriptContextConstants.kBuildTarget, EditorUserBuildSettings.activeBuildTarget);
            SetValue(BuildScriptContextConstants.kPlayerBuildVersion, settings.ActivePlayModeDataBuilder.GetType().Name);
        }

        /// <summary>
        /// Creates a default context object with values taken from the AddressableAssetSettings parameter.
        /// </summary>
        /// <param name="settings">The settings object to pull values from.</param>
        /// <param name="buildTargetGroup">The BuildTargetGroup to set.</param>
        /// <param name="buildTarget">The BuildTarget to set.</param>
        /// <param name="developerMode">Is develepor mode.</param>
        /// <param name="sendProfilerEvents">Send profiler events.</param>
        /// <param name="playerBuildVersion">The player build version.</param>
        public AddressablesBuildDataBuilderContext(AddressableAssetSettings settings, BuildTargetGroup buildTargetGroup, BuildTarget buildTarget, bool developerMode, bool sendProfilerEvents, string playerBuildVersion)
        {
            SetValue(BuildScriptContextConstants.kAddressableAssetSettings, settings);
            SetValue(BuildScriptContextConstants.kDevMode, developerMode);
            SetValue(BuildScriptContextConstants.kProfile, sendProfilerEvents);
            SetValue(BuildScriptContextConstants.kBuildTargetGroup, buildTargetGroup);
            SetValue(BuildScriptContextConstants.kBuildTarget, buildTarget);
            SetValue(BuildScriptContextConstants.kPlayerBuildVersion, playerBuildVersion);
        }

        /// <summary>
        /// Get a context value.
        /// </summary>
        /// <typeparam name="T">The type of the context value to retrieve.</typeparam>
        /// <param name="key">The key for the value.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value cast to the type T.</returns>
        public T GetValue<T>(string key, T defaultValue)
        {
            object res = null;
            if (!m_Values.TryGetValue(key, out res))
                return defaultValue;
            return (T)res;
        }

        /// <summary>
        /// Sets a context value.
        /// </summary>
        /// <param name="key">The key for the value.</param>
        /// <param name="value">The value to set.</param>
        public void SetValue(string key, object value)
        {
            m_Values[key] = value;
        }
    }
}