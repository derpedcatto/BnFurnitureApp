﻿using System.Text;

namespace BnFurniture.Shared.Utilities.Hash
{
    public class Sha1HashService : IHashServices
    {
        public string HashString(string source)
        {
           
            using var hasher = System.Security.Cryptography.SHA256.Create();
            return Convert.ToHexString(hasher.ComputeHash(System.Text.Encoding.UTF8.GetBytes(source)));
           
        }
    }
}
