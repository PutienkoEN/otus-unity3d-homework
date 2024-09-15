using System;
using Homeworks.SaveLoad.Scripts.Security;
using Zenject;

namespace Homeworks.SaveLoad.Scripts.Persistance
{
    public class AesPersistingStrategy : IPersistingStrategy
    {
        private readonly IPersistingStrategy persistingStrategy;
        private readonly AesEncryptor aesEncryptor;

        [Inject]
        public AesPersistingStrategy(IPersistingStrategy persistingStrategy, AesEncryptor aesEncryptor)
        {
            this.persistingStrategy = persistingStrategy;
            this.aesEncryptor = aesEncryptor;
        }

        public void Save(string valueToSave)
        {
            var encryptedValue = aesEncryptor.Encrypt(valueToSave);
            persistingStrategy.Save(encryptedValue);
        }

        public bool TryLoad(out string value)
        {
            if (!persistingStrategy.TryLoad(out var valueToLoad))
            {
                value = default;
                return false;
            }

            value = aesEncryptor.Decrypt(valueToLoad);
            return true;
        }
    }
}