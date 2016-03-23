using System;
using System.Collections.Generic;
using System.IO;
using System.Text;


namespace Brass9.IO
{
	/// <summary>
	/// A merge of the FileStream and StreamWriter classes.
	/// 
	/// Disposable. Implements TextWriter, and wraps it in a StreamWriter so you can write straight out to files.
	/// Allows you to use a single using statement with just the path of the file instead of 2 using statements and a bunch of
	/// options.
	/// 
	/// Calls FileStream with Create, Write, and shared Read.
	/// Encodes in UTF8 by default, or you can pass an alternative encoding in the constructor.
	/// </summary>
	public class FileStreamWriter : TextWriter
	{
		protected FileStream stream;
		protected StreamWriter writer;

		public FileStreamWriter(string path, Encoding encoding)
		{
			stream = File.Open(path, FileMode.Create, FileAccess.Write, FileShare.Read);
			writer = new StreamWriter(stream, encoding);
		}
		public FileStreamWriter(string path)
			: this(path, Encoding.UTF8)
		{
		}

		public FileStreamWriter Write(params string[] text)
		{
			foreach (var s in text)
				writer.Write(s);

			return this;
		}

		public FileStreamWriter WriteLine(params string[] text)
		{
			Write(text);
			writer.WriteLine();

			return this;
		}

		public new void Dispose()
		{
			writer.Dispose();
			stream.Dispose();
		}

		public override Encoding Encoding
		{
			get { return writer.Encoding; }
		}
	}
}
