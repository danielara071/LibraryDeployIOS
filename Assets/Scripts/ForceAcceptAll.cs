using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Networking;
public class ForceAcceptAll : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}
