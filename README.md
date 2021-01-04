[<img src="icon.png">](https://github.com/fikret0/Antares.Crypter/)
# Antares.Crypter
 A basic library for asymmetric and symmetric key encryption/decryption or hashing.

## Basic Usage:

```cs
using Antares.Crypter;

...

Crypter crypter = new Crypter();
crypter.Mode = HashMode.MD5;

string test = crypter.Hash("hello!");
```

## Modes:

There is 4 modes to crypt/encrypt/hash.

- AES
- RSA
- MD5
- SHA256
