using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Brass9.IO.FileSystemInfoExtension
{
	public static class FileSystemInfoExtensions
	{
		/// <summary>
		/// Checks for the directory and its parent directories to ensure they exist; creates it and as many parents as necessary
		/// if they don't.
		/// </summary>
		/// <param name="this_"></param>
		/// <returns>True if the entire path already existed. False if any directories were created.</returns>
		public static bool EnsureDirectory(this FileSystemInfo this_)
		{
			string dirPath;
			if (this_ is FileInfo)
				dirPath = ((FileInfo)this_).Directory.FullName;
			else if (this_ is DirectoryInfo)
				dirPath = ((DirectoryInfo)this_).FullName;
			else
				throw new Exception("Subclass of FileSystemInfo lacks a clear way to get the current full directory path");

			// Shortcut if the whole thing's already there
			if (Directory.Exists(dirPath))
				return true;

			// It's not, 
			string[] chain = dirPath.Split('\\');
			var verify = new StringBuilder(chain[0]);

			for (int i = 1; i < chain.Length; i++)
			{
				verify.Append("\\").Append(chain[i]);
				string dirToVerify = verify.ToString();
				if (!Directory.Exists(dirToVerify))
					Directory.CreateDirectory(dirToVerify);
			}

			return false;
		}
	}
}
