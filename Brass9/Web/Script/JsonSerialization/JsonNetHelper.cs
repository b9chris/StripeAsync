using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Brass9.IO;


namespace Brass9.Web.Script.JsonSerialization
{
	public class JsonNetHelper
	{
		public static JsonNetHelper Create()
		{
			return new JsonNetHelper();
		}



		/// <summary>
		/// Serialize to a TextWriter, using the fixed Iso DateTime converter, instead of Json.Net's built-in.
		/// </summary>
		/// <param name="writer"></param>
		/// <param name="o"></param>
		public void Serialize(TextWriter writer, object o)
		{
			var jsonSerializer = getFixedSerializer();
			jsonSerializer.Serialize(writer, o);
		}
		/// <param name="contractResolver">Used to whitelist properties, or otherwise limit what's serialized</param>
		public void Serialize(TextWriter writer, object o, IContractResolver contractResolver)
		{
			var jsonSerializer = getFixedSerializer();
			jsonSerializer.Serialize(writer, o);
		}

		public string SerializeObject(object o)
		{
			var sb = new StringBuilder();
			using(var writer = new StringWriter(sb))
			{
				Serialize(writer, o);
			}
			var s = sb.ToString();
			return s;
		}
		/// <param name="contractResolver">Used to whitelist properties, or otherwise limit what's serialized</param>
		public string SerializeObject(object o, IContractResolver contractResolver)
		{
			var sb = new StringBuilder();
			using(var writer = new StringWriter(sb))
			{
				Serialize(writer, o, contractResolver);
			}
			var s = sb.ToString();
			return s;
		}

		protected JsonConverter getFixedIsoDateTimeConverter()
		{
			return new IsoDateTimeConverter
			{
				DateTimeFormat = Brass9.DateHelpers.DateTimeHelper.IsoFormat
			};
		}
		protected JsonSerializer getFixedSerializer()
		{
			var settings = new JsonSerializerSettings
			{
				Converters = new[] { getFixedIsoDateTimeConverter() },
#if MIN
				Formatting = Formatting.None
#endif
			};
			var jsonSerializer = JsonSerializer.Create(settings);
			return jsonSerializer;
		}
		/// <param name="contractResolver">Used to whitelist properties, or otherwise limit what's serialized</param>
		protected JsonSerializer getFixedSerializer(IContractResolver contractResolver)
		{
			var settings = new JsonSerializerSettings
			{
				Converters = new[] { getFixedIsoDateTimeConverter() },
				ContractResolver = contractResolver,
#if MIN
				Formatting = Formatting.None
#endif
			};
			var jsonSerializer = JsonSerializer.Create(settings);
			return jsonSerializer;
		}



		/// <summary>
		/// Write any object out to a file as JSON, UTF-8 encoded
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="fullFilePath"></param>
		public void WriteFile(object obj, string fullFilePath)
		{
			using (var writer = new FileStreamWriter(fullFilePath))
			{
				var jsonEncoder = getFixedSerializer();
				jsonEncoder.Serialize(writer, obj);
			}
		}

		public async Task WriteFileAsync(object obj, string fullFilePath)
		{
			string json = JsonConvert.SerializeObject(obj);
			await FileHelper.WriteFileAsync(fullFilePath, json);
		}
		


		/// <summary>
		/// Read a UTF-8 encoded JSON file in as an object
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="fullFilePath"></param>
		/// <returns></returns>
		public T ReadFile<T>(string fullFilePath)
		{
			using (var reader = File.OpenText(fullFilePath))
			{
				using (var jsonReader = new JsonTextReader(reader))
				{
					var jsonEncoder = new JsonSerializer();
					return jsonEncoder.Deserialize<T>(jsonReader);
				}
			}
		}

		public async Task<T> ReadFileAsync<T>(string fullFilePath)
		{
			string json = await FileHelper.ReadAllTextAsync(fullFilePath);
			T obj = JsonConvert.DeserializeObject<T>(json);
			return obj;
		}



		public T ReadAndDisposeStream<T>(Stream stream)
		{
			// http://stackoverflow.com/a/17788118/176877
			var jsonSerializer = new JsonSerializer();

			using(var sr = new StreamReader(stream))
			{
				using(var jsonReader = new JsonTextReader(sr))
				{
					var result = jsonSerializer.Deserialize<T>(jsonReader);
					return result;
				}
			}
		}

		/// <summary>
		/// Consumes a WebResponse's content as JSON, and returns the resulting object
		/// </summary>
		/// <param name="re">Any WebResponse, often the result of await request.GetResponseAsync()</param>
		public T ReadResponse<T>(System.Net.WebResponse re)
		{
			return ReadAndDisposeStream<T>(re.GetResponseStream());
		}
	}
}
