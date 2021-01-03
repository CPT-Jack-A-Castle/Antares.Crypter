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
