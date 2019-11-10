using System;
using System.Security.Cryptography;

namespace BoardGameDatabase.Operations
{
	// https://stackoverflow.com/questions/4181198/how-to-hash-a-password/10402129#10402129

    internal static class HashEncryption
    {
	    private const int SaltLenght = 16;
	    private const int HashLenght = 20;
	    private const int Interaction = 10000;

	    public static string Hash(string password)
	    {
		    byte[] salt;
			new RNGCryptoServiceProvider().GetBytes(salt = new byte[SaltLenght]);
			Rfc2898DeriveBytes pbkdf2 = new Rfc2898DeriveBytes(password,salt, Interaction);
		    byte[] hash = pbkdf2.GetBytes(HashLenght);
			byte[] hashByte = new byte[SaltLenght + HashLenght];
			Array.Copy(salt, 0, hashByte, 0, SaltLenght);
			Array.Copy(hash, 0, hashByte, SaltLenght, HashLenght);
		    return Convert.ToBase64String(hashByte);
	    }

	    public static bool Verification(string password, string hashPassword)
	    {
		    byte[] hashByte = Convert.FromBase64String(hashPassword);
			byte[] salt = new byte[SaltLenght];
			Array.Copy(hashByte,0,salt,0,SaltLenght);
			var pbkdf2 = new Rfc2898DeriveBytes(password,salt,Interaction);
		    byte[] hash = pbkdf2.GetBytes(HashLenght);

		    for (int i = 0; i < HashLenght; i++)
		    {
			    if (hashByte[i + SaltLenght] != hash[i])
			    {
				    return false;
			    }
		    }

		    return true;
	    }
    }
}
