import {UserManagerSettings, WebStorageStateStore} from "oidc-client";

export function ClientSetting(): UserManagerSettings {
  return {
    authority: 'http://localhost:3685/',
    client_id: 'angular_spa',
    redirect_uri: 'http://localhost:44330/auth-callback',
    post_logout_redirect_uri: 'http://localhost:44330/',
    response_type:"id_token token",
    scope:"openid profile customAPI.read",
    filterProtocolClaims: true,
    loadUserInfo: true,
    userStore: new WebStorageStateStore({ store: window.localStorage })
  };
}
