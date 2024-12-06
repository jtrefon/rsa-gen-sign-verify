using System.Security.Cryptography;

namespace Blik.Cli.Libs;

public class RsaKeyGenerator
{
    public static void GenerateKeys(int keySize = 2048)
    {
        using RSA rsa = RSA.Create();
        rsa.KeySize = keySize;

        // Export the private key
        var privateKey = rsa.ExportPkcs8PrivateKey();
        var privateKeyPem = PemEncoding.Write("PRIVATE KEY", privateKey);
        File.WriteAllText("private_key.pem", privateKeyPem);

        // Export the public key
        var publicKey = rsa.ExportSubjectPublicKeyInfo();
        var publicKeyPem = PemEncoding.Write("PUBLIC KEY", publicKey);
        File.WriteAllText("public_key.pem", publicKeyPem);

        Console.WriteLine("Keys generated and saved to 'private_key.pem' and 'public_key.pem'.");
    }

    public static void SignData(string privateKeyPath, string dataFile)
    {
        string data = File.ReadAllText(dataFile);
        string privateKeyPem = File.ReadAllText(privateKeyPath);
        string privateKeyBase64 = privateKeyPem.Replace("-----BEGIN PRIVATE KEY-----", "").Replace("-----END PRIVATE KEY-----", "").Replace("\n", "").Replace("\r", "");
        byte[] privateKey = Convert.FromBase64String(privateKeyBase64);

        using RSA rsa = RSA.Create();
        rsa.ImportPkcs8PrivateKey(privateKey, out _);

        byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
        byte[] signature = rsa.SignData(dataBytes, HashAlgorithmName.SHA3_384, RSASignaturePadding.Pkcs1);
        File.WriteAllBytes("signature.sig", signature);

        Console.WriteLine("Data signed and signature saved to 'signature.sig'.");
    }

    public static void VerifySignature(string publicKeyPath, string dataFile, string signatureFile)
    {
        string data = File.ReadAllText(dataFile);
        string publicKeyPem = File.ReadAllText(publicKeyPath);
        string publicKeyBase64 = publicKeyPem.Replace("-----BEGIN PUBLIC KEY-----", "").Replace("-----END PUBLIC KEY-----", "").Replace("\n", "").Replace("\r", "");
        byte[] publicKey = Convert.FromBase64String(publicKeyBase64);

        byte[] signature = File.ReadAllBytes(signatureFile);

        using RSA rsa = RSA.Create();
        rsa.ImportSubjectPublicKeyInfo(publicKey, out _);

        byte[] dataBytes = System.Text.Encoding.UTF8.GetBytes(data);
        bool verified = rsa.VerifyData(dataBytes, signature, HashAlgorithmName.SHA3_384, RSASignaturePadding.Pkcs1);

        Console.WriteLine(verified ? "Signature verified." : "Signature not verified.");
    }
}