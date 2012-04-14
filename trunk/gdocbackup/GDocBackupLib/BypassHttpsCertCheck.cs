/*
   Copyright 2009-2012  Fabrizio Accatino

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
   See the License for the specific language governing permissions and
   limitations under the License.
*/

using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;


namespace GDocBackupLib
{

    // This is a better solution. But Mono does not support it.  ARGHH !
    // -------------------------------------------------------
    // ServicePointManager.ServerCertificateValidationCallback = delegate(
    //    Object sndr,
    //    System.Security.Cryptography.X509Certificates.X509Certificate certificate,
    //    System.Security.Cryptography.X509Certificates.X509Chain chain,
    //    System.Net.Security.SslPolicyErrors sslPolicyErrors)
    //    {
    //        return true;
    //    };
    // -------------------------------------------------------


    /// <summary>
    /// A very indulgent certificate policy...  :-)
    /// </summary>
    internal class BypassHttpsCertCheck : ICertificatePolicy
    {
        public bool CheckValidationResult(ServicePoint srvPoint, X509Certificate certificate, WebRequest request, int certificateProblem)
        {
            return true;
        }
    }
}
