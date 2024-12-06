# CLI Tool

This CLI tool provides functionalities for RSA key generation, data signing, and signature verification.

## Usage

```sh
dotnet run <command> [options]
```

### Commands

#### `keygen`

Generates RSA keys of the specified size.

```sh
dotnet run keygen <keySize>
```

- `<keySize>`: The size of the RSA key to generate (e.g., 2048, 4096).

#### `sign`

Signs the provided data using the specified private key.

```sh
dotnet run sign <privateKeyPath> <data>
```

- `<privateKeyPath>`: The path to the private key file.
- `<data>`: The data to sign.

#### `verify`

Verifies the provided signature using the specified public key.

```sh
dotnet run verify <publicKeyPath> <data> <signature>
```

- `<publicKeyPath>`: The path to the public key file.
- `<data>`: The data that was signed.
- `<signature>`: The signature to verify.

## Examples

### Generate RSA Keys

```sh
dotnet run keygen 2048
```

### Sign Data

```sh
dotnet run sign /path/to/privateKey.pem "data to sign"
```

### Verify Signature

```sh
dotnet run verify /path/to/publicKey.pem "data to verify" "signature"
```

## License

This project is licensed under the Apache 2 License.
