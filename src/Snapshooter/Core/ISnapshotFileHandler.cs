﻿namespace Snapshooter.Core
{
	/// <summary>
	/// The <see cref="SnapshotFileHandler"/> is responsible to handle all snapshots  
	/// interactions on the file system. It can read, save and delete snapshots or 
	/// their folders.
	/// </summary>
	public interface ISnapshotFileHandler
    {
		/// <summary>
		/// Saves a new snapshot as a snapshot *.snap file in the __snapshots__ folder.
		/// </summary>
		/// <param name="snapshotFileInfo">The snapshot name and location.</param>
		/// <param name="snapshotData">The current snapshot data to store.</param>
		/// <returns>The file path of the stored snapshot.</returns>
		string SaveNewSnapshot(
			ISnapshotFileInfo snapshotFileInfo, string snapshotData);

		/// <summary>
		/// Saves a mismatching snapshot as a snapshot *.snap file 
		/// in the __snapshots__/__mismatch__ folder.
		/// </summary>
		/// <param name="snapshotFileInfo">The snapshot name and location.</param>
		/// <param name="snapshotData">The current snapshot data to store.</param>
		/// <returns>The file path of the stored snapshot.</returns>
		string SaveMismatchSnapshot(
			ISnapshotFileInfo snapshotFileInfo, string snapshotData);

		/// <summary>
		/// Reads the current snapshot from the __snapshots__ folder.
		/// </summary>
		/// <param name="snapshotFileInfo">The file info of the snapshot.</param> 
		/// <returns>The expected snapshot.</returns>
		string ReadSnapshot(ISnapshotFileInfo snapshotFileInfo);

		/// <summary>
		/// Deletes the given subfolder of the __snapshots__ folder of the current snapshot test.
		/// </summary>
		/// <param name="snapshotFileInfo">The location of the running snapshot test.</param>
		/// <param name="subfolderName">The subfolder to delete.</param>
		void DeleteSnapshotSubfolder(
			ISnapshotFileInfo snapshotFileInfo, string subfolderName);

	}
}