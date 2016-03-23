using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Brass9.IO.FileSystemInfoExtension;

namespace Brass9.IO
{
	public static class FileHelper
	{
		public static string ReadFile(FileInfo file)
		{
			using (var reader = file.OpenText())
			{
				return reader.ReadToEnd();
			}
		}

		public static IEnumerable<string> ReadLines(string path)
		{
			using (var stream = File.OpenText(path))
			{
				// Holy fancy enumerators! http://msdn.microsoft.com/en-us/library/vstudio/9k7k7cf0.aspx
				string line = stream.ReadLine();
				while (line != null)
				{
					yield return line;
					line = stream.ReadLine();
				}

			}

			yield break;
		}

		/// <summary>
		/// Processes the lines of 
		/// </summary>
		/// <param name="path"></param>
		/// <param name="processHeader"></param>
		/// <returns></returns>
		public static string ReadLinesUntil(string path, Func<string, bool> where)
		{
			foreach (var line in ReadLines(path))
			{
				if (where(line))
					return line;
			}

			return null;
		}


		public static void WriteFile(FileInfo file, string s)
		{
			WriteFile(file, s, Encoding.UTF8);
		}
		public static void WriteFile(FileInfo file, string s, Encoding enc)
		{
			using (var stream = file.OpenWrite())
			{
				var bytes = enc.GetBytes(s);
				stream.Write(bytes, 0, bytes.Length);
			}
		}

		/// <summary>
		/// Reads all text in a file, asynchronously. Doesn't throw for missing files - returns null.
		/// </summary>
		/// <param name="path">Full file path</param>
		/// <returns>Contents of the file, or null if the file doesn't exist.</returns>
		public static async Task<string> ReadAllTextAsync(string path)
		{
			// If the path is enclosed in double quotes for DOS etc we'll screw up in the OpenText call below - strip if present.
			if (path.StartsWith("\"") && path.EndsWith("\""))
				path = path.Substring(1, path.Length - 2);

			// http://stackoverflow.com/a/13168006/176877
			string s;

			// Avoid throwing if not exists, especially in async
			if (!File.Exists(path))
				return null;

			using(var reader = File.OpenText(path))
			{
				s = await reader.ReadToEndAsync();
			}
			return s;
		}

		public static async Task WriteFileAsync(string path, string s)
		{
			await WriteFileAsync(new FileInfo(path), s);
		}

		public static async Task WriteFileAsync(FileInfo file, string s)
		{
			await WriteFileAsync(file, s, Encoding.UTF8);
		}
		public static async Task WriteFileAsync(FileInfo file, string s, Encoding enc)
		{
			using (var stream = file.OpenWrite())
			{
				var bytes = enc.GetBytes(s);
				await stream.WriteAsync(bytes, 0, bytes.Length);
			}
		}

		/// <summary>
		/// Checks for the directory and its parent directories to ensure they exist; creates it and as many parents as necessary
		/// if they don't.
		/// </summary>
		/// <param name="path">Path for directory to ensure exists.</param>
		/// <returns>True if the entire path already existed. False if any directories were created.</returns>
		public static bool EnsureDirectory(string path)
		{
			var dir = new DirectoryInfo(path);
			bool existed = dir.EnsureDirectory();
			return existed;
		}

		/// <summary>
		/// Exactly like File.Move(), but overwrites if the file exists.
		/// </summary>
		/// <param name="from"></param>
		/// <param name="to"></param>
		public static void Move(string from, string to)
		{
			if (File.Exists(to))
				File.Delete(to);

			File.Move(from, to);
		}

		public static void Copy(string from, string to)
		{
			if (from == null)
				throw new ArgumentNullException("from");

			if (from.Equals(to))
				return;

			if (File.Exists(to))
				File.Delete(to);

			File.Copy(from, to);
		}
	}
}
