import "bootstrap/dist/css/bootstrap.css";
import React from "react";
import ReactDOM from "react-dom";
import { Router } from "react-router-dom";
import history from "./utils/history";
import App from "./App";
import registerServiceWorker from "./registerServiceWorker";
import { Auth0Provider } from "./react-auth0-spa";
import { CookiesProvider } from 'react-cookie';

import authConfig from "./auth_config.json";

const baseUrl = document.getElementsByTagName('base')[0].getAttribute('href');
const rootElement = document.getElementById('root');

const onRedirectCallback = (appState) => {
    history.push(
        appState && appState.targetUrl
            ? appState.targetUrl
            : window.location.pathname
    );
};

ReactDOM.render(
    <Auth0Provider
        domain={authConfig.domain}
        client_id={authConfig.clientId}
        audience={authConfig.audience}
        redirect_uri={window.location.origin}
        onRedirectCallback={onRedirectCallback}
    >
        <CookiesProvider>
            <Router basename={baseUrl} history={history}>
                <App />
            </Router>
        </CookiesProvider>
    </Auth0Provider>,
    rootElement);

registerServiceWorker();

