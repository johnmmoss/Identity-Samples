using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.IdentityModel.Protocols.WSFederation;
using Microsoft.IdentityModel.SecurityTokenService;
using Microsoft.IdentityModel.Web;
using WifStsApplication.sts.Core;

namespace WifStsApplication.sts
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_PreRender(object sender, EventArgs e)
        {
            string action = Request.QueryString[WSFederationConstants.Parameters.Action];

            try
            {
                if (action == WSFederationConstants.Actions.SignIn)
                {
                    // Process signin request.
                    SignInRequestMessage requestMessage = (SignInRequestMessage)WSFederationMessage.CreateFromUri(Request.Url);
                    if (User != null && User.Identity != null && User.Identity.IsAuthenticated)
                    {
                        SecurityTokenService sts = new CustomSecurityTokenService(CustomSecurityTokenServiceConfiguration.Current);
                        SignInResponseMessage responseMessage = FederatedPassiveSecurityTokenServiceOperations.ProcessSignInRequest(requestMessage, User, sts);
                        FederatedPassiveSecurityTokenServiceOperations.ProcessSignInResponse(responseMessage, Response);
                    }
                    else
                    {
                        throw new UnauthorizedAccessException();
                    }
                }
                else if (action == WSFederationConstants.Actions.SignOut)
                {
                    // Process signout request.
                    SignOutRequestMessage requestMessage = (SignOutRequestMessage)WSFederationMessage.CreateFromUri(Request.Url);
                    FederatedPassiveSecurityTokenServiceOperations.ProcessSignOutRequest(requestMessage, User, requestMessage.Reply, Response);
                }
                else
                {
                    throw new InvalidOperationException(
                        String.Format(CultureInfo.InvariantCulture,
                            "The action '{0}' (Request.QueryString['{1}']) is unexpected. Expected actions are: '{2}' or '{3}'.",
                            String.IsNullOrEmpty(action) ? "<EMPTY>" : action,
                            WSFederationConstants.Parameters.Action,
                            WSFederationConstants.Actions.SignIn,
                            WSFederationConstants.Actions.SignOut));
                }
            }
            catch (Exception exception)
            {
                throw new Exception("An unexpected error occurred when processing the request. See inner exception for details.", exception);
            }
        }
    }
}