using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CryptedData<T> : Object
{
	#region VALUE

	/// <summary>
	/// Gets or sets the crypted value.
	/// </summary>
	/// <value>The value.</value>
	public T value
	{
		get
		{
			if(Data == null)
				return default(T);
			else
				return (T) ByteArrayToObject(XOR(this.Data, this.Key));
		}

		set
		{
			if(value == null)
				value = default(T);

			GetNewKey(value);

			this.Data = XOR(ObjectToByteArray(value), this.Key);
		}
	}

	#endregion

	#region PRIVATE_PROPERTIES

	private byte[] Data;
	private byte[] Key;

	#endregion

	#region CONSTRUCTOR

	/// <summary>
	/// Initializes a new instance of the <see cref="CryptedData"/> class with the default value.
	/// </summary>
	public CryptedData()
	{
		value = default(T);
	}

	/// <summary>
	/// Initializes a new instance of the <see cref="CryptedData"/> class with a value.
	/// </summary>
	/// <param name="data">T</param>
	public CryptedData(T data)
	{
		if(data == null)
			value = default(T);
		else
			value = data;
	}

	#endregion

	#region OPERATOR_OVERRIDE

	public static bool operator ==(CryptedData<T> v1, T v2) 
	{
		return v1.value.Equals(v2);
	}

	public static bool operator !=(CryptedData<T> v1, T v2) 
	{
		return !v1.value.Equals(v2);
	}

	#endregion

	#region OBJECT_OVERRIDE

	public override string ToString()
	{
		return value.ToString();
	}

	public override bool Equals(object obj)
	{
		return value.Equals((T) obj);
	}

	public new Type GetType()
	{
		return typeof(T);
	}

	public override int GetHashCode ()
	{
		return value.GetHashCode ();
	}

	#endregion

	#region private methods

	/// <summary>
	/// Object to byte array.
	/// </summary>
	/// <returns>The byte array</returns>
	/// <param name="obj">Object</param>
	private byte[] ObjectToByteArray(Object obj)
	{
		if(obj == null)
			return null;

		BinaryFormatter bf = new BinaryFormatter();
		using(MemoryStream ms = new MemoryStream())
		{
			bf.Serialize(ms, obj);
			return ms.ToArray();
		}
	}

	/// <summary>
	/// Byte array to object.
	/// </summary>
	/// <returns>The object conversion.</returns>
	/// <param name="arrBytes">Byte Array.</param>
	private Object ByteArrayToObject(byte[] bytes)
	{
		if(bytes == null)
			return null;

		BinaryFormatter bf = new BinaryFormatter();
		using(MemoryStream ms = new MemoryStream())
		{
			ms.Write(bytes, 0, bytes.Length);
			ms.Seek(0, SeekOrigin.Begin);
			return (Object) bf.Deserialize(ms);
		}
	}

	/// <summary>
	/// XOR Crypt
	/// </summary>
	/// <param name="data">Data.</param>
	/// <param name="key">Key.</param>
	private byte[] XOR(byte[] data, byte[] key)
	{
		byte[] xor = new byte[data.Length];

		for(int i = 0; i < data.Length; i++)
			xor[i] = (byte) (data[i] ^ key[i]);

		return xor;
	}

	/// <summary>
	/// Gets the new key.
	/// </summary>
	/// <param name="value">Value.</param>
	private void GetNewKey(object value)
	{
		this.Key = new byte[ObjectToByteArray(value).Length];
		new Random().NextBytes(Key);
	}

	#endregion
}