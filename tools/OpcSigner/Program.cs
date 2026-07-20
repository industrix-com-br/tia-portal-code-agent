using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

class Program
{
    static int Main(string[] args)
    {
        if (args.Length < 1)
        {
            Console.WriteLine("Usage: OpcSigner <addin-file> [certificate-thumbprint]");
            return 1;
        }

        string addinPath = args[0];
        string thumbprint = args.Length > 1 ? args[1] : null;

        if (!File.Exists(addinPath))
        {
            Console.Error.WriteLine("File not found: " + addinPath);
            return 1;
        }

        X509Certificate2 cert = thumbprint != null
            ? FindCertificate(thumbprint)
            : FindOrCreateSelfSignedCert();

        if (cert == null)
        {
            Console.Error.WriteLine("Certificate not found.");
            return 1;
        }

        Console.WriteLine("Certificate: " + cert.Subject + " [" + cert.Thumbprint + "]");
        Console.WriteLine("Signing: " + addinPath);

        Package package = Package.Open(addinPath, FileMode.Open, FileAccess.ReadWrite);
        try
        {
            PackageDigitalSignatureManager sigManager =
                new PackageDigitalSignatureManager(package);

            // Collect all part URIs to sign
            List<Uri> partUris = new List<Uri>();
            foreach (PackagePart part in package.GetParts())
            {
                partUris.Add(part.Uri);
            }

            Console.WriteLine("  Signing " + partUris.Count + " parts...");

            // Sign all parts
            sigManager.Sign(partUris, cert as X509Certificate);

            Console.WriteLine("Package signed successfully!");
        }
        finally
        {
            package.Close();
        }

        return 0;
    }

    static X509Certificate2 FindCertificate(string thumbprint)
    {
        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);
        X509Certificate2Collection found = store.Certificates.Find(
            X509FindType.FindByThumbprint, thumbprint, false);
        store.Close();
        return found.Count > 0 ? found[0] : null;
    }

    static X509Certificate2 FindOrCreateSelfSignedCert()
    {
        X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        store.Open(OpenFlags.ReadOnly);

        X509Certificate2Collection existing = store.Certificates.Find(
            X509FindType.FindBySubjectName, "TIA Portal Code Agent", false);
        if (existing.Count > 0)
        {
            X509Certificate2 cert = existing[0];
            store.Close();
            return cert;
        }
        store.Close();

        Console.WriteLine("Creating self-signed code signing certificate...");

        RSA rsa = RSA.Create(2048);
        CertificateRequest request = new CertificateRequest(
            new X500DistinguishedName("CN=TIA Portal Code Agent"),
            rsa,
            HashAlgorithmName.SHA256,
            RSASignaturePadding.Pkcs1);

        request.CertificateExtensions.Add(
            new X509KeyUsageExtension(
                X509KeyUsageFlags.DigitalSignature | X509KeyUsageFlags.KeyEncipherment,
                false));

        X509Certificate2 newCert = request.CreateSelfSigned(
            DateTimeOffset.Now,
            DateTimeOffset.Now.AddYears(5));

        X509Store writableStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
        writableStore.Open(OpenFlags.ReadWrite);
        writableStore.Add(newCert);
        writableStore.Close();

        Console.WriteLine("  Created: " + newCert.Thumbprint);
        return newCert;
    }
}
