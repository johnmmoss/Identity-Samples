using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CreateSelfSignedCert
{
    /// <summary>
    /// Creates a self-signed X.509 certificate.
    /// 
    /// For maximum compatibility, the "Microsoft Enhanced RSA and AES Cryptographic Provider" cryptographic service provider (CSP)
    /// is used to create the private key. This is supported in all versions of the .NET framework.
    /// 
    /// The alternative of a Cryptography API: Next Generation (CNG) provider such as the default
    /// "Microsoft Software Key Storage Provider" requires .NET framework v4.6 or higher.
    /// 
    /// Attempting to use a CNG provider private key in earlier versions of the .NET framework results in a 
    /// "CryptographicException: Invalid provider type specified".
    /// 
    /// Usage: CreateSelfSignedCert.exe
    /// </summary>
    class Program
    {
        private const int provideType = 24;
        private const string providerName = "Microsoft Enhanced RSA and AES Cryptographic Provider";

        static void Main(string[] args)
        {
            try
            {
                Console.Write("Subject distinguished name (eg CN=test): ");
                var subjectName = Console.ReadLine();

                if (string.IsNullOrEmpty(subjectName))
                {
                    throw new ArgumentException("A subject distinguished name must be specified.");
                }

                var keySizeInBits = 2048;
                Console.Write($"Key Size in bits [{keySizeInBits}]: ");
                var input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && !int.TryParse(Console.ReadLine(), out keySizeInBits))
                {
                    throw new ArgumentException("The key size must be an integer.");
                }

                var yearsBeforeExpiring = 5;
                Console.Write($"Number of years before expiring [{yearsBeforeExpiring}]: ");
                input = Console.ReadLine();

                if (!string.IsNullOrEmpty(input) && !int.TryParse(Console.ReadLine(), out yearsBeforeExpiring))
                {
                    throw new ArgumentException("The number of years must be an integer.");
                }

                using (var privateKey = new RSACryptoServiceProvider(keySizeInBits, new CspParameters(provideType, providerName, Guid.NewGuid().ToString())))
                {
                    var certificateRequest = new CertificateRequest(subjectName, privateKey, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);

                    certificateRequest.CertificateExtensions.Add(
                        new X509KeyUsageExtension(X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.DataEncipherment | X509KeyUsageFlags.KeyEncipherment, false));

                    var notBefore = DateTimeOffset.UtcNow;
                    var notAfter = notBefore.AddYears(yearsBeforeExpiring);

                    using (var x509Certificate = certificateRequest.CreateSelfSigned(notBefore, notAfter))
                    {
                        Console.Write("Certificate file name (eg test.cer): ");
                        var fileName = Console.ReadLine();

                        if (string.IsNullOrEmpty(fileName))
                        {
                            throw new ArgumentException("A file name must be specified.");
                        }

                        File.WriteAllText(fileName, Convert.ToBase64String(x509Certificate.Export(X509ContentType.Cert)));
                        Console.WriteLine($"The certificate has been saved to {fileName}.");

                        Console.Write("Private key file name (eg test.pfx): ");
                        fileName = Console.ReadLine();

                        if (string.IsNullOrEmpty(fileName))
                        {
                            throw new ArgumentException("A file name must be specified.");
                        }

                        Console.Write("Private key password: ");
                        var password = Console.ReadLine();

                        if (string.IsNullOrEmpty(password))
                        {
                            throw new ArgumentException("A password must be specified.");
                        }

                        File.WriteAllBytes(fileName, x509Certificate.Export(X509ContentType.Pfx, password));
                        Console.WriteLine($"The private key has been saved to {fileName}.");
                    }
                }
            }

            catch (Exception exception)
            {
                Console.WriteLine(exception.ToString());
            }
        }
    }
}
