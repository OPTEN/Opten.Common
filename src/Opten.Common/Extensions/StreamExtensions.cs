using System;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Opten.Common.Extensions
{
	/// <summary>
	/// Provides extension methods for copying streams
	/// </summary>
	public static class StreamExtensions
	{

		/// <summary>
		/// Copies the remaining data in the current stream to a new MemoryStream instance.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static MemoryStream CopyToMemoryStream(this Stream stream)
		{
			return CopyToMemoryStream(stream, false);
		}
		
		/// <summary>
		/// [async] Copies the current stream into a new MemoryStream instance.
		/// </summary>
		/// <param name="stream">The stream.</param>
		/// <param name="bufferSize">Size of the buffer.</param>
		/// <returns></returns>
		public static async Task<MemoryStream> CopyToMemoryStreamAsync(this Stream stream, int bufferSize = 4096)
		{
			MemoryStream ms = new MemoryStream(stream.CanSeek ? ((int)stream.Length + 8 - (int)stream.Position) : bufferSize);
			await stream.CopyToAsync(ms, bufferSize);
			ms.Position = 0;
			return ms;
		}

		/// <summary>
		/// Copies the current stream into a new MemoryStream instance.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data</param>
		/// <returns></returns>
		public static MemoryStream CopyToMemoryStream(this Stream stream, bool entireStream)
		{
			return CopyToMemoryStream(stream, entireStream, 0x1000);
		}

		/// <summary>
		/// Copies the current stream into a new MemoryStream instance.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data</param>
		/// <param name="chunkSize">The buffer size to use (in bytes) if a buffer is required. Default: 4KiB</param>
		/// <returns></returns>
		public static MemoryStream CopyToMemoryStream(this Stream stream, bool entireStream, int chunkSize)
		{
			MemoryStream ms = new MemoryStream(stream.CanSeek ? ((int)stream.Length + 8 - (entireStream ? 0 : (int)stream.Position)) : chunkSize);
			CopyToStream(stream, ms, entireStream, chunkSize);
			ms.Position = 0;
			return ms;
		}

		/// <summary>
		/// Copies the remaining data in the current stream to a byte[] array of exact size.
		/// </summary>
		/// <param name="stream"></param>
		/// <returns></returns>
		public static byte[] CopyToBytes(this Stream stream)
		{
			return CopyToBytes(stream, false);
		}

		/// <summary>
		/// Copies the current stream into a byte[] array of exact size.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data</param>
		/// <returns></returns>
		public static byte[] CopyToBytes(this Stream stream, bool entireStream)
		{
			return CopyToBytes(stream, entireStream, 0x1000);
		}

		/// <summary>
		/// Copies the remaining data from the this stream into the given stream.
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="other">The stream to write to</param>
		public static void CopyToStream(this Stream stream, Stream other)
		{
			CopyToStream(stream, other, false);
		}

		/// <summary>
		/// Copies this stream into the given stream
		/// </summary>
		/// <param name="stream"></param>
		/// <param name="other">The stream to write to</param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data</param>
		public static void CopyToStream(this Stream stream, Stream other, bool entireStream)
		{
			CopyToStream(stream, other, entireStream, 0x1000);
		}

		/// <summary>
		/// Copies this stream into the given stream
		/// </summary>
		/// <param name="src"></param>
		/// <param name="dest">The stream to write to</param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data</param>
		/// <param name="chunkSize">True to copy entire stream if seekable, false to only copy remaining data</param>
		public static void CopyToStream(this Stream src, Stream dest, bool entireStream, int chunkSize)
		{
			if (entireStream && src.CanSeek) src.Seek(0, SeekOrigin.Begin);

			if (src is MemoryStream && src.CanSeek)
			{
				try
				{
					int pos = (int)src.Position;
					dest.Write(((MemoryStream)src).GetBuffer(), pos, (int)(src.Length - pos));
					return;
				}
				catch (UnauthorizedAccessException) //If we can't slice it, then we read it like a normal stream
				{ }
			}
			if (dest is MemoryStream && src.CanSeek)
			{
				try
				{
					int srcPos = (int)src.Position;
					int pos = (int)dest.Position;
					int length = (int)(src.Length - srcPos) + pos;
					dest.SetLength(length);

					byte[] data = ((MemoryStream)dest).GetBuffer();
					while (pos < length)
					{
						pos += src.Read(data, pos, length - pos);
					}
					return;
				}
				catch (UnauthorizedAccessException) //If we can't write directly, fall back
				{ }
			}
			int size = (src.CanSeek) ? Math.Min((int)(src.Length - src.Position), chunkSize) : chunkSize;
			byte[] buffer = new byte[size];
			int n;
			do
			{
				n = src.Read(buffer, 0, buffer.Length);
				dest.Write(buffer, 0, n);
			} while (n != 0);
		}

		/// <summary>
		/// Can return null if MemoryStream doesn't permit access to internal buffer. 
		/// </summary>
		/// <param name="src"></param>
		/// <param name="entireStream"></param>
		/// <returns></returns>
		private static byte[] CopyMemoryStreamToBytes(this MemoryStream src, bool entireStream)
		{
			byte[] bytes;

			//Slice from 
			MemoryStream ms = src as MemoryStream;
			try
			{
				byte[] buffer = ms.GetBuffer();
				long pos = entireStream ? 0 : src.Position;
				long count = src.Length - pos;
				bytes = new byte[count];
				Array.Copy(buffer, pos, bytes, 0, count);
				return bytes;
			}
			catch (UnauthorizedAccessException) //If we can't slice it, then we read it like a normal stream
			{ }
			if (entireStream || src.Position == 0) return ms.ToArray(); //Uses InternalBlockCopy, quite fast...
			else
			{
				return null;
			}
		}

		/// <summary>
		/// Copies the current stream into a byte[] array of exact size
		/// </summary>
		/// <param name="src"></param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data.</param>
		/// <param name="chunkSize">The buffer size to use (in bytes) if a buffer is required. Default: 4KiB</param>
		/// <returns></returns>
		public static byte[] CopyToBytes(this Stream src, bool entireStream, int chunkSize)
		{

			byte[] bytes = src is MemoryStream ? CopyMemoryStreamToBytes(src as MemoryStream, entireStream) : null;
			if (bytes != null) return bytes;

			if (src.CanSeek)
			{
				long pos = entireStream ? 0 : src.Position;
				if (entireStream) src.Seek(0, SeekOrigin.Begin);

				// Read the source file into a byte array.
				int numBytesToRead = (int)(src.Length - pos);
				bytes = new byte[numBytesToRead];
				int numBytesRead = 0;
				while (numBytesToRead > 0)
				{
					// Read may return anything from 0 to numBytesToRead.
					int n = src.Read(bytes, numBytesRead, numBytesToRead);

					// Break when the end of the file is reached.
					if (n == 0)
						break;

					numBytesRead += n;
					numBytesToRead -= n;
				}

				Debug.Assert(numBytesRead == bytes.Length);

				return bytes;
			}
			else
			{
				//No seeking, so we have to buffer to an intermediate memory stream
				MemoryStream ms = new MemoryStream(chunkSize);
				CopyToStream(src, ms, entireStream, chunkSize);
				return CopyToBytes(ms, true, chunkSize);
			}
		}

		/// <summary>
		/// [async] Copies the current stream into a byte[] array of exact size.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="entireStream">True to copy entire stream if seekable, false to only copy remaining data.</param>
		/// <param name="chunkSize">The buffer size to use (in bytes) if a buffer is required. Default: 4KiB</param>
		/// <returns></returns>
		public static async Task<byte[]> CopyToBytesAsync(this Stream src, bool entireStream, int chunkSize)
		{
			byte[] bytes = src is MemoryStream ? CopyMemoryStreamToBytes(src as MemoryStream, entireStream) : null;
			if (bytes != null) return bytes;


			if (src.CanSeek)
			{
				long pos = entireStream ? 0 : src.Position;
				if (entireStream) src.Seek(0, SeekOrigin.Begin);

				// Read the source file into a byte array.
				int numBytesToRead = (int)(src.Length - pos);
				bytes = new byte[numBytesToRead];
				int numBytesRead = 0;
				while (numBytesToRead > 0)
				{
					// Read may return anything from 0 to numBytesToRead.
					int n = await src.ReadAsync(bytes, numBytesRead, numBytesToRead);

					// Break when the end of the file is reached.
					if (n == 0)
						break;

					numBytesRead += n;
					numBytesToRead -= n;
				}

				Debug.Assert(numBytesRead == bytes.Length);

				return bytes;
			}
			else
			{
				//No seeking, so we have to buffer to an intermediate memory stream
				MemoryStream ms = new MemoryStream(chunkSize);
				await src.CopyToAsync(ms, chunkSize);
				return CopyToBytes(ms, true, chunkSize);
			}
		}

		/// <summary>
		/// Attempts to return a byte[] array containing the remaining portion of the stream.
		/// Unlike CopyToBytes(), does not return a byte[] array of exact length, and may re-use the actual Stream's byte array, making it unsafe to write to in the future.
		/// </summary>
		/// <param name="src"></param>
		/// <param name="length"></param>
		/// <param name="chunkSize"></param>
		/// <param name="entireStream"></param>
		/// <returns></returns>
		public static byte[] CopyOrReturnBuffer(this Stream src, out long length, bool entireStream, int chunkSize)
		{
			if (src is MemoryStream)
			{
				if (entireStream || src.Position == 0)
				{
					length = src.Length;
					//Slice from 
					MemoryStream ms = src as MemoryStream;
					try
					{
						return ms.GetBuffer();
					}
					catch (UnauthorizedAccessException) //If we can't slice it, then we read it like a normal stream
					{
						return ms.ToArray();
					}
				}
				else
				{
					byte[] buf = CopyToBytes(src, entireStream, chunkSize);
					length = buf.Length;
					return buf;
				}
			}
			else
			{
				MemoryStream ms = CopyToMemoryStream(src, entireStream, chunkSize);
				return CopyOrReturnBuffer(ms, out length, true, chunkSize);
			}
		}

	}
}