//-----------------------------------------------------------------------------
//
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY OF
// ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO
// THE IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
// PARTICULAR PURPOSE.
//
// Copyright (c) Microsoft Corporation. All rights reserved.
//
//
//-----------------------------------------------------------------------------

using System.Threading;
using Microsoft.IdentityModel.Claims;

namespace ClaimsService
{
    // NOTE: If you change the class name "Service" here, you must also update the reference to "Service" in Web.config and in the associated .svc file.
    public class Service : IService
    {
        public string GetData( int value )
        {
            //TODO: Change the code below to handle your claims usage.
            IClaimsPrincipal principal = ( IClaimsPrincipal )Thread.CurrentPrincipal;
            IClaimsIdentity identity = ( IClaimsIdentity )principal.Identity;

            return string.Format( "You entered: {0} and you are {1}", value, identity.Name );
        }

        public CompositeType GetDataUsingDataContract( CompositeType composite )
        {
            if ( composite.BoolValue )
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
