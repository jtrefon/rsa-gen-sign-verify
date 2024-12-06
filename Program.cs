using Blik.Cli.Libs;

class Program
{
    static void Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: dotnet run <command> [options]");
            return;
        }

        string command = args[0].ToLower();

        switch (command)
        {
            case "keygen":
                if (args.Length != 2 || !int.TryParse(args[1], out int keySize))
                {
                    Console.WriteLine("Usage: dotnet run keygen <keySize>");
                    return;
                }
                RsaKeyGenerator.GenerateKeys(keySize);
                break;

            case "sign":
                if (args.Length != 3)
                {
                    Console.WriteLine("Usage: dotnet run sign <privateKeyPath> <data>");
                    return;
                }
                RsaKeyGenerator.SignData(args[1], args[2]);
                break;

            case "verify":
                if (args.Length != 4)
                {
                    Console.WriteLine("Usage: dotnet run verify <publicKeyPath> <data> <signature>");
                    return;
                }
                RsaKeyGenerator.VerifySignature(args[1], args[2], args[3]);
                break;

            default:
                Console.WriteLine("Unknown command. Available commands: keygen, sign, verify");
                break;
        }

    }
}