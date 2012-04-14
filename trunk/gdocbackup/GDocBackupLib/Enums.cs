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
 

namespace GDocBackupLib
{

    /// <summary>
    /// Proxy authentication modes
    /// </summary>
    public enum ProxyAuthMode
    {
        NotAuthenticated,    // proxy not authenticated
        DefaultCredential,   // current user credential (in Win use logged-in user credentials)
        UsernamePassword     // explicit username and password
    }


    /// <summary>
    /// Proxy address source
    /// </summary>
    public enum ProxyHostPortSource
    {
        Default,   // IE
        HostPort
    }
}
