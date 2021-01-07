[<img src="icon.png" width="100">](https://github.com/fikret0/Antares.Crypter/)

[![NuGet Version](https://img.shields.io/nuget/v/Antares.Crypter.svg?style=flat)](https://www.nuget.org/packages/Antares.Crypter/) [![NuGet](https://img.shields.io/nuget/dt/Antares.Crypter.svg)](https://www.nuget.org/packages/Antares.Crypter)
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
