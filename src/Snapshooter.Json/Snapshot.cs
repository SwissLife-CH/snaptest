﻿using System;
using Snapshooter.Core;

namespace Snapshooter.Json
{
    /// <summary>
    /// The snapshot class creates and compares snapshots of object.
    /// It creates a json snapshot of the given object and compares it with the 
    /// already existing snapshot of the test. If no snapshot exists already for this
    /// test, then a new snapshot will be created from the current result and saved
    /// in the folder __snapshots__ in the bin directory.
    /// </summary>
    public static class Snapshot
    {
        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname will be evaluated automatically.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match<T>(T currentResult,
                                    string snapshotName,
                                    Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            Match((object)currentResult, snapshotName, matchOptions);
        }

        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        ///  /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically.
        /// </param> 
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the generated snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific. 
        /// Example: 
        /// Generated Snapshotname = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match<T>(T currentResult,
                                    string snapshotName,
                                    SnapshotNameExtension snapshotNameExtension,
                                    Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            Match((object)currentResult, snapshotName, snapshotNameExtension, matchOptions);
        }
                
        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <param name="currentResult">The object to match.</param>
        /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically.
        /// </param> 
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match(object currentResult,
                                 string snapshotName,
                                 Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            SnapshotFullName snapshotFullName = FullName(snapshotName);
            Snapshooter.AssertSnapshot(currentResult, snapshotFullName, matchOptions);
        }

        /// <summary>
        /// Matches the current result/object with the actual snapshot of the test. If 
        /// no snapshot exists, a new snapshot will be created from the current result
        /// and saved under a certain file path, which will shown in the assert exception.
        /// </summary>
        /// <typeparam name="T">The type of the result/object to match.</typeparam>
        /// <param name="currentResult">The object to match.</param>
        ///  /// <param name="snapshotName">
        /// The name of the snapshot. If not set, then the snapshotname 
        /// will be evaluated automatically.
        /// </param> 
        /// <param name="snapshotNameExtension">
        /// The snapshot name extension will extend the generated snapshot name with
        /// this given extensions. It can be used to make a snapshot name even more
        /// specific. 
        /// Example: 
        /// Generated Snapshotname = 'NumberAdditionTest'
        /// Snapshot name extension = '5', '6', 'Result', '11'
        /// Result: 'NumberAdditionTest_5_6_Result_11'
        /// </param>
        /// <param name="matchOptions">
        /// Additional compare actions, which can be applied during the comparison
        /// </param>
        public static void Match(object currentResult,
                                 string snapshotName,
                                 SnapshotNameExtension snapshotNameExtension,
                                 Func<MatchOptions, MatchOptions> matchOptions = null)
        {
            SnapshotFullName snapshotFullName = FullName(snapshotName, snapshotNameExtension);
            Snapshooter.AssertSnapshot(currentResult, snapshotFullName, matchOptions);
        }

        public static SnapshotFullName FullName(string snapshotName)
        {
            return Snapshooter.ResolveSnapshotFullName(snapshotName);
        }
        
        public static SnapshotFullName FullName(
            string snapshotName, SnapshotNameExtension snapshotNameExtension)
        {
            return Snapshooter.ResolveSnapshotFullName(snapshotName, snapshotNameExtension);
        }

        private static Snapshooter Snapshooter
        {
            get
            {
                return 
                    new Snapshooter(
                        new SnapshotAssert(
                            new SnapshotSerializer(),
                            new SnapshotFileHandler(),
                            new SnapshotEnvironmentCleaner(
                                new SnapshotFileHandler()),
                            new JsonSnapshotComparer(
                                new JsonAssert(),
                                new SnapshotSerializer())),
                        new SnapshotFileInfoResolver(
                            new JsonSnapshotFileInfoReader(),
                            new SnapshotFileNameBuilder()));
            }
        }
    }
}
