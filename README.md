# CryptedData
C# Class to encrypt your objects or fields in memory

## How it's works
It make a random Key and generate a encrypted byte array in memory and It return the decrypted data, It generate a new key in all value sets!

## How to use
Make a CryptedData object with the type of your object/field

    CryptedData<int> cryptedInt = new CryptedData<int>(0);

Use the value property

    cryptedInt.value = 10;
    if(cryptedInt.value == 10)
    {
        //To do something...
    }

Welldone! The hackers can not find the cryptedInt data! (Or it is much difficult!)
